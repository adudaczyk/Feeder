using FeederSokML.EntityFramework.Models;
using FeederSokML.EntityFramework.Repositories;
using FeederSokML.Models;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using System;
using System.Net;

namespace FeederSokML.Services
{
    public class ClassificationService : IClassificationService
    {
        private readonly IProcesRezultMLRepository _procesRezultMLRepository;
        private readonly ILogger _logger;

        public ClassificationService(IProcesRezultMLRepository procesRezultMLRepository, ILogger logger)
        {
            _procesRezultMLRepository = procesRezultMLRepository;
            _logger = logger;
        }

        public ClassificationResult SendToClassification(string endpoint, string filePath, int procesId)
        {
            var clientOptions = new RestClientOptions()
            {
                UseDefaultCredentials = true,
                MaxTimeout = 60000
            };

            using (var client = new RestClient(clientOptions))
            {
                var request = new RestRequest(endpoint, Method.Post);
                request.AddHeader("Content-Type", "multipart/form-data");
                request.AddFile("file", filePath);

                try
                {
                    _logger.Info($"[ProcesID: {procesId}] Rozpoczęcie wysyłki do klasyfikacji. (Endpoint: {endpoint} | FilePath: {filePath}).");

                    var response = client.Post(request);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception($"[ProcesID: {procesId}] Response status code: {response.StatusCode}");
                    }

                    _logger.Info($"[ProcesID: {procesId}] Otrzymano dane z klasyfikacji");

                    return JsonConvert.DeserializeObject<ClassificationResult>(response.Content);
                }
                catch (TimeoutException ex)
                {
                    _logger.Error(ex, $"[ProcesID: {procesId}] Przekroczono czas oczekiwania na odpowiedź (60000 ms) z {endpoint}.");
                    return null;
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, $"[ProcesID: {procesId}] Błąd podczas wysyłki do klasyfikacji");
                    return null;
                }
            }
        }

        public int? SaveClassificationResult(ClassificationResult classification, DocumentForClassification doc)
        {
            try
            {
                _logger.Info($"[ProcesID: {doc.IdProces}] Zapis wyniku klasyfikacji do tabeli [dbo].[proces_RezultML]");
        
                var entity = new ProcesRezultML()
                {
                    IdProces = doc.IdProces,
                    IddictProcesClassificationML = doc.IdDictProcesClassificationML,
                    IdpdfMail = doc.IdPdfMail,
                    IddefProces = doc.IdDefProces,
                    Predicted = classification.PredictedLabel,
                    PredictedScore = (double?)classification.PredictedScore,
                    Scores = JsonConvert.SerializeObject(classification.Scores),
                    UseDate = null,
                };
        
                _procesRezultMLRepository.Add(entity);
                _procesRezultMLRepository.SaveChanges();

                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[ProcesID: {doc.IdProces}] Błąd podczas zapisu wyniku klasyfikacji do tabeli [dbo].[proces_RezultML]");
                return null;
            }
        }
        
    }
}
