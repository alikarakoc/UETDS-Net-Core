using System.ComponentModel;

namespace UetdsEntegrasyon.Models.Uetds
{
   public class SeferYolcuBilgileri
   {
      [Description("SeferGrupEkleme işleminden sonra dönen id uetdsGrupRefNo")]
      public int grupId { get; set; }
      public string adi { get; set; }
      public string soyadi { get; set; }
      public string cinsiyet { get; set; }
      public string koltukNo { get; set; }
      public string tcKimlikPasaportNo { get; set; }
      public string telefonNo { get; set; }
      public string uyrukUlke { get; set; }
   }
}
