using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeederSokML.EntityFramework.Models
{
    [Table("proces_RezultML")]
    public partial class ProcesRezultML
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdProces { get; set; }
        public int IddictProcesClassificationML { get; set; }
        public short IddefProces { get; set; }
        public int? IdpdfMail { get; set; }
        public string Predicted { get; set; }
        public double? PredictedScore { get; set; }
        public string Scores { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? UseDate { get; set; }
    }
}
