using System.ServiceModel;
using uetdsServis;

namespace UetdsEntegrasyon.Yardimci
{
   public class servis
   {
      public static string kullanici = "gecerliKullanici";
      public static string kullaniciTest = "999999";
      public static string sifre = "gecerliSifre";
      public static string sifreTest = "999999testtest";
      public static string servisUrl = "https://servis.turkiye.gov.tr/services/g2g/kdgm/uetdsarizi?wsdl";
      public static string servisTest = "https://servis.turkiye.gov.tr/services/g2g/kdgm/test/uetdsarizi?wsdl";

      public static UdhbUetdsAriziServiceClient baglan()
      {
         BasicHttpBinding binding = new(BasicHttpSecurityMode.Transport);
         binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
         UdhbUetdsAriziServiceClient c = new(binding, new EndpointAddress(servisUrl));
         c.ClientCredentials.UserName.UserName = kullanici;
         c.ClientCredentials.UserName.Password = sifre;
         return c;
      }
      public static UdhbUetdsAriziServiceClient baglanTest()
      {
         BasicHttpBinding binding = new(BasicHttpSecurityMode.Transport);
         binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
         UdhbUetdsAriziServiceClient c = new(binding, new EndpointAddress(servisUrl));
         c.ClientCredentials.UserName.UserName = kullanici;
         c.ClientCredentials.UserName.Password = sifre;
         return c;
      }
   }
}
