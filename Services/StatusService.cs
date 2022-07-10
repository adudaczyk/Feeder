using FeederSokML.EntityFramework.Repositories;
using NLog;
using System;

namespace FeederSokML.Services
{
    public class StatusService : IStatusService
    {
        private readonly IProcesyRepository _procesyRepository;
        private readonly ILogger _logger;

        public StatusService(IProcesyRepository procesyRepository, ILogger logger)
        {
            _procesyRepository = procesyRepository;
            _logger = logger;
        }

        public short? GetStatus(int procesId)
        {
            return _procesyRepository.GetById(procesId)?.Status;
        }

        public void ChangeStatus(int procesId, short newStatus)
        {
            _logger.Info($"[ProcesID: {procesId}] Zmiana statusu na {newStatus}");

            var entity = _procesyRepository.GetById(procesId);

            if (entity == null)
            {
                throw new NullReferenceException("Nie znaleziono rekordu w tabeli [dbo].[Procesy] o ID: {procesId}");
            }

            entity.Status = newStatus;
            entity.Host = "SokProcesML";
            entity.Modifdate = DateTime.Now;
            _procesyRepository.Update(entity);
            _procesyRepository.SaveChanges();

            _procesyRepository.InvokeProcesyHistSP(entity.Id);
        }
    }
}
