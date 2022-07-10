using FeederSokML.EntityFramework.Models;
using FeederSokML.Models;

namespace FeederSokML.Services
{
    public interface IClassificationService
    {
        ClassificationResult SendToClassification(string endpoint, string filePath, int procesId);
        int? SaveClassificationResult(ClassificationResult classification, DocumentForClassification doc);
    }
}