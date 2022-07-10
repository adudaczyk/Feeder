using System;

namespace FeederSokML.EntityFramework.Models
{
    /// <summary>
    /// Dane dla rejestru SOK Proces -Procesowanie spraw z dokumentów.
    /// </summary>
    public partial class Procesy
    {
        public int Id { get; set; }
        public short? IddefProces { get; set; }
        public byte? IdTypKlient { get; set; }
        public short? Status { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? DataOdlozone { get; set; }
        public DateTime? Modifdate { get; set; }
        public string NrWniosku { get; set; }
        public string KodDokumentu { get; set; }
        public string Klient { get; set; }
        public string Pesel { get; set; }
        public string Regon { get; set; }
        public string Login { get; set; }
        public string Host { get; set; }
        public string Nadawca { get; set; }
        public string Prowadzacy { get; set; }
        public string Val1 { get; set; }
        public string Val2 { get; set; }
        public string Val3 { get; set; }
        public string Val4 { get; set; }
        public string Val5 { get; set; }
        public string Val6 { get; set; }
        public string Uwagi { get; set; }
        public string Zalaczniki { get; set; }
        public DateTime InsertDate { get; set; }
        public int? IdMaster { get; set; }
        public short? BrakAutoGrupowania { get; set; }
        public string Val7 { get; set; }
        public string Val8 { get; set; }
        public string Val9 { get; set; }
        public string Val10 { get; set; }
        public string Val11 { get; set; }
        public string Val12 { get; set; }
        public string Val13 { get; set; }
        public string Val14 { get; set; }
        public int? IdObrazu { get; set; }
        public int? Ecmid { get; set; }
        public string Val15 { get; set; }
        public string Val16 { get; set; }
        public string Val17 { get; set; }
        public string Val18 { get; set; }
        public string Val19 { get; set; }
        public string Val20 { get; set; }
        public string Val21 { get; set; }
        public string Val22 { get; set; }
        public string Val23 { get; set; }
        public string Val24 { get; set; }
        public string Val25 { get; set; }
        public short? IdStatusObraz { get; set; }
        public byte? ObrazmCo { get; set; }
        public short IdTypDokOddzial { get; set; }
        public DateTime InsertDateTime { get; set; }
        public short IloscDniWprocesie { get; set; }
        public string NrKontraktu { get; set; }
    }
}
