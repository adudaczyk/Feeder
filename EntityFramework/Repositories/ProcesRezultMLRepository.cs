using FeederSokML.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace FeederSokML.EntityFramework.Repositories
{
    public class ProcesRezultMLRepository : IProcesRezultMLRepository
    {
        protected readonly DbSet<ProcesRezultML> _dbSet;
        protected readonly SokDbContext _dbContext;

        public ProcesRezultMLRepository(SokDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<ProcesRezultML>();
        }

        public void Add(ProcesRezultML entity)
        {
            _dbSet.Add(entity);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
            _dbContext.ChangeTracker.Clear();
        }
    }
}
