using FeederSokML.EntityFramework.Models;
using FeederSokML.EntityFramework.Repositories;
using FeederSokML.Services;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FeederSokML
{
    public class Executor
    {
        private readonly IStoredProceduresRepository _spRepository;
        private readonly IServiceProvider _services;
        private readonly ILogger _logger;

        public Executor(IStoredProceduresRepository spRepository, IServiceProvider services, ILogger logger)
        {
            _spRepository = spRepository;
            _services = services;
            _logger = logger;
        }

        public void Execute(int maxThreadCount)
        {
            _logger.Info("Uruchomienie FeederSokML");

            var docsForClassification = _spRepository.GetDocumentsForClassification()
                .Where(x => x.Status == 56 || x.Status == 57)
                .OrderByDescending(x => x.Status);

            Parallel.ForEach(
                docsForClassification, 
                new ParallelOptions { MaxDegreeOfParallelism = maxThreadCount },
                doc =>
            {
                using (var scope = _services.CreateScope())
                {
                    var statusService = scope.ServiceProvider.GetRequiredService<IStatusService>();
                    var classificationService = scope.ServiceProvider.GetRequiredService<IClassificationService>();
                    var spRepository = scope.ServiceProvider.GetRequiredService<IStoredProceduresRepository>();

                    _logger.Info($"[ProcesID: {doc.IdProces}] Rozpoczęcie pracy z rekordem.");

                    if (doc.Status != statusService.GetStatus(doc.IdProces))
                    {
                        _logger.Info($"[ProcesID: {doc.IdProces}] Nastąpiła zmiana statusu poza aplikacją przed wysłaniem do klasyfikacji. Pomijam rekord.");
                        return;
                    }

                    try
                    {
                        statusService.ChangeStatus(doc.IdProces, 57);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"[ProcesID: {doc.IdProces}] Nie znaleziono rekordu w tabeli [dbo].[Procesy] o ID: {doc.IdProces}");
                        return;
                    }
                    
                    var filePath = ValidateFilePath(doc);

                    if (string.IsNullOrEmpty(filePath))
                    {
                        _logger.Info($"[ProcesID: {doc.IdProces}] Błąd podczas walidacji. Pomijam rekord.");
                        return;
                    }

                    var classificationResult = classificationService.SendToClassification(doc.UrlML, filePath, doc.IdProces);

                    if (classificationResult == null)
                    {
                        _logger.Info($"[ProcesID: {doc.IdProces}] Brak wyniku klasyfikacji. Pomijam rekord.");
                        return;
                    }

                    if (statusService.GetStatus(doc.IdProces) != 57)
                    {
                        _logger.Info($"[ProcesID: {doc.IdProces}] Nastąpiła zmiana statusu poza aplikacją po odebraniu danych z klasyfikacji. Pomijam rekord.");
                        return;
                    }

                    var classificationId = classificationService.SaveClassificationResult(classificationResult, doc);

                    if (classificationId == null)
                    {
                        _logger.Info($"[ProcesID: {doc.IdProces}] Pusty wynik klasyfikacji. Pomijam rekord.");
                        return;
                    }

                    try
                    {
                        statusService.ChangeStatus(doc.IdProces, 58);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"[ProcesID: {doc.IdProces}] Nie znaleziono rekordu w tabeli [dbo].[Procesy] o ID: {doc.IdProces}");
                        return;
                    }

                    spRepository.InvokeAnalysisML((int)classificationId, doc.IdProces);
                }
            });

            _logger.Info("Zakończenie pracy FeederSokML");
        }

        private string ValidateFilePath(DocumentForClassification doc)
        {
            try
            {
                _logger.Info($"[ProcesID: {doc.IdProces}] Weryfikacja czy plik istnieje w podanej ścieżce.");

                if (File.Exists(doc.LinkTiff))
                {
                    return doc.LinkTiff;
                }
                else if (File.Exists(doc.LinkPdf))
                {
                    return doc.LinkPdf;
                };

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[ProcesID: {doc.IdProces}] Błąd podczas weryfikacji ścieżki pliku.");
                return string.Empty;
            }
        }
    }
}
