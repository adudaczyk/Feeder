using System.Collections.Generic;

namespace FeederSokML.Models
{
    public class ClassificationResult
    {
        public string PredictedLabel { get; set; }
        public decimal PredictedScore { get; set; }
        public Dictionary<string, decimal> Scores { get; set; }
    }
}
