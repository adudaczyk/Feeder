using FeederSokML.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FeederSokML.EntityFramework.Repositories
{
    public class ProcesyRepository : IProcesyRepository
    {
        protected readonly DbSet<Procesy> _dbSet;
        protected readonly SokDbContext _dbContext;

        public ProcesyRepository(SokDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Procesy>();
        }

        public Procesy GetById(int id)
        {
            return _dbSet.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Update(Procesy entity)
        {
            _dbSet.Update(entity);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void InvokeProcesyHistSP(int procesId)
        {
            _dbContext.Database.ExecuteSqlInterpolatedAsync($"EXEC [dbo].[p_SokProces_UtrwalHistorie] @id={procesId}");
        }
    }
}
