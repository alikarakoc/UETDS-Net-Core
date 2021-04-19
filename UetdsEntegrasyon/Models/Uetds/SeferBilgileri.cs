using System;

namespace UetdsEntegrasyon.Models.Uetds
{
   public class SeferBilgileri
   {
      public string aracPlaka { get; set; }
      public DateTime hareketTarihi { get; set; }
      public string hareketSaati { get; set; }
      public string aracTelefonu { get; set; }
      public string firmaSeferNo { get; set; }
      public string seferAciklama { get; set; }
      public string seferBitisSaati { get; set; }
      public DateTime seferBitisTarihi { get; set; }
   }
}
