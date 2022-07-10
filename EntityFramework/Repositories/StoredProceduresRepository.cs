using BlackOps.Config;
using FeederSokML.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeederSokML.EntityFramework.Repositories
{
    public class StoredProceduresRepository : IStoredProceduresRepository
    {
        protected readonly SokDbContext _dbContext;
        protected readonly ILogger _logger;

        public StoredProceduresRepository(SokDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public List<DocumentForClassification> GetDocumentsForClassification()
        {
            try
            {
                _logger.Info($"Wywołanie SP [dbo].[p_proces_GetML].");
                var result = _dbContext.GetMLResults.FromSqlRaw("EXEC [dbo].[p_proces_GetML]").ToList();
                _logger.Info($"Ilość otrzymanych rekordów z SP [dbo].[p_proces_GetML]: {result.Count} (w statusie 57: {result.Count(x => x.Status == 57)}, w statusie 56: {result.Count(x => x.Status == 56)})");

                TokenizeRecords(result);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Wystąpił błąd podczas wywoływania SP [dbo].[p_proces_GetML]");
                throw;
            }
        }

        public Task InvokeAnalysisML(int idProcesRezultML, int procesId)
        {
            _logger.Info($"[ProcesID: {procesId}] Wywołanie SP [dbo].[p_proces_AnalysisML] z parametrem idProcesRezultML: {idProcesRezultML} bez oczekiwania na jej wynik.");
            _dbContext.Database.ExecuteSqlInterpolatedAsync($"EXEC [dbo].[p_proces_AnalysisML] @idProcesRezultML={idProcesRezultML}");

            return Task.CompletedTask;
        }

        private static void TokenizeRecords(List<DocumentForClassification> items)
        {
            IConfigurationProvider config = new AppSettingsJsonConfigurerWithoutDB();

            foreach (var item in items)
            {
                item.LinkTiff = config.TokenizeValueWithFile(item.LinkTiff);
                item.LinkPdf = config.TokenizeValueWithFile(item.LinkPdf);
                item.UrlML = config.TokenizeValueWithFile(item.UrlML);
            }
        }
    }
}
