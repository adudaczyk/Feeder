using FeederSokML.EntityFramework.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeederSokML.EntityFramework.Repositories
{
    public interface IStoredProceduresRepository
    {
        List<DocumentForClassification> GetDocumentsForClassification();
        Task InvokeAnalysisML(int idProcesRezultML, int idProces);
    }
}