using FeederSokML.EntityFramework.Models;

namespace FeederSokML.EntityFramework.Repositories
{
    public interface IProcesRezultMLRepository
    {
        void Add(ProcesRezultML entity);
        void SaveChanges();
    }
}