using FeederSokML.EntityFramework.Models;

namespace FeederSokML.EntityFramework.Repositories
{
    public interface IProcesyRepository
    {
        Procesy GetById(int id);
        void Update(Procesy entity);
        void SaveChanges();
        void InvokeProcesyHistSP(int procesId);
    }
}