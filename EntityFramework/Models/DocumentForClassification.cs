namespace FeederSokML.EntityFramework.Models
{
    public class DocumentForClassification
    {
        public string LinkPdf { get; set; }
        public string LinkTiff { get; set; }
        public string UrlML { get; set; }
        public int IdProces { get; set; }
        public short Status { get; set; }
        public short IdDefProces { get; set; }
        public int IdDictProcesClassificationML { get; set; }
        public int IdPdfMail { get; set; }
    }
}
