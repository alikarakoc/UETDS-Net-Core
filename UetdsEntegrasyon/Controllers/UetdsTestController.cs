using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UetdsEntegrasyon.Models;
using UetdsEntegrasyon.Models.Uetds;
using UetdsEntegrasyon.Yardimci;
using uetdsServis;

namespace UetdsEntegrasyon.Controllers
{
   [Route("api/[controller]/[action]")]
   [ApiController]
   public class UetdsTestController : ControllerBase
   {
      /// <summary>
      /// 
      /// </summary>
      /// <remarks>SeferEkle işleminden sonra dönen uetdsSeferReferansNo bilgisini kendi tarafınızda saklamalısınız. SeferGrupEkleme, Yolcu Ekleme, Personel Ekleme işlemlerinde uetdsSeferReferansNo bilgisini kullanarak işlem yapmalısınız.</remarks>
      [HttpPost]
      public async Task<IActionResult> SeferEkle([FromBody] SeferBilgileri m)
      {
         var client = servis.baglanTest();
         uetdsYtsUser servisUser = new()
         {
            kullaniciAdi = servis.kullaniciTest,
            sifre = servis.sifreTest
         };
         try
         {
            var kullaniciKontrolSonuc = await client.kullaniciKontrolAsync(client.ClientCredentials.UserName.UserName, client.ClientCredentials.UserName.Password);
            uetdsAriziSeferBilgileriInput seferBilgi = new()
            {
               aracPlaka = Convert.ToString(m.aracPlaka),
               hareketTarihi = Convert.ToDateTime(m.hareketTarihi),
               hareketSaati = Convert.ToString(m.hareketSaati),
               aracTelefonu = Convert.ToString(m.aracTelefonu),
               firmaSeferNo = Convert.ToString(m.firmaSeferNo),
               seferAciklama = Convert.ToString(m.seferAciklama),
               seferBitisSaati = m.seferBitisSaati,
               seferBitisTarihi = Convert.ToDateTime(m.seferBitisTarihi)
            };
            var seferEkleSonuc = await client.seferEkleAsync(servisUser, seferBilgi);
            if (seferEkleSonuc.@return.sonucKodu != 0)
            {
               NResult _seferEkleSonuc = new()
               {
                  Message = seferEkleSonuc.@return.sonucMesaji,
                  ResponseType = ResponseType.Error
               };
               return Ok(_seferEkleSonuc);
            }
            else
            {
               NResult<long> _seferEkleSonuc = new()
               {
                  Data = seferEkleSonuc.@return.uetdsSeferReferansNo,
                  Message = seferEkleSonuc.@return.sonucMesaji,
                  ResponseType = ResponseType.Success
               };
               return Ok(_seferEkleSonuc);
            }

         }
         catch (Exception ex)
         {
            NResult result = new()
            {
               Message = "UETDS Bağlantısı başarısız. UETDS Sonuç:  " + ex.Message + "",
               ResponseType = ResponseType.Error
            };
            return Ok(result);
         }
      }
      /// <summary>
      /// 
      /// </summary>
      /// <remarks>SeferEkle işleminden sonra size verilen uetdsSeferReferansNo bilgisi ile işlem yapmalısınız.</remarks>
      [HttpPost]
      public async Task<IActionResult> SeferGrupEkle([FromBody] AriziGrupBilgileri m, int uetdsSeferReferansNo)
      {
         var client = servis.baglanTest();
         uetdsYtsUser servisUser = new()
         {
            kullaniciAdi = servis.kullaniciTest,
            sifre = servis.sifreTest
         };
         try
         {
            var kullaniciKontrolSonuc = await client.kullaniciKontrolAsync(client.ClientCredentials.UserName.UserName, client.ClientCredentials.UserName.Password);
            uetdsAriziGrupBilgileriInput grupBilgi = new()
            {
               baslangicIl = Convert.ToInt32(m.baslangicIl),
               baslangicIlce = Convert.ToInt32(m.baslangicIlce),
               baslangicUlke = Convert.ToString(m.baslangicUlke),
               baslangicYer = Convert.ToString(m.baslangicYer),
               bitisIl = Convert.ToInt32(m.bitisIl),
               bitisIlce = Convert.ToInt32(m.bitisIlce),
               bitisUlke = Convert.ToString(m.bitisUlke),
               bitisYer = Convert.ToString(m.bitisYer),
               grupAdi = Convert.ToString(m.grupAdi),
               grupAciklama = Convert.ToString(m.grupAciklama),
               grupUcret = String.Format("{0:#,###}", m.grupUcret),
            };
            var seferGrupEkleSonuc = await client.seferGrupEkleAsync(servisUser, uetdsSeferReferansNo, grupBilgi);
            if (seferGrupEkleSonuc.@return.sonucKodu != 0)
            {
               NResult _seferGrupEkleSonuc = new()
               {
                  Message = seferGrupEkleSonuc.@return.sonucMesaji,
                  ResponseType = ResponseType.Error
               };
               return Ok(_seferGrupEkleSonuc);

            }
            else
            {
               NResult<seferGrupEkleResponse> _seferGrupEkleSonuc = new()
               {
                  Data = seferGrupEkleSonuc,
                  Message = seferGrupEkleSonuc.@return.sonucMesaji,
                  ResponseType = ResponseType.Success
               };
               return Ok(_seferGrupEkleSonuc);
            }

         }
         catch (Exception ex)
         {
            NResult result = new()
            {
               Message = "UETDS Bağlantısı başarısız. UETDS Sonuç:  " + ex.Message + "",
               ResponseType = ResponseType.Success
            };
            return Ok(result);
         }
      }
      /// <summary>
      /// 
      /// </summary>
      /// <remarks>
      /// SeferEkle işleminden sonra size verilen uetdsSeferReferansNo bilgisi ile işlem yapmalısınız. 
      /// <br></br>
      /// YolcuEkle işleminde Ad ve Soyad UETDS sisteminde kiril alfabesi kabul edilmediği için göndereceğiniz isim örneğin полина api tarafından UETDS sistemine Polina olarak gidecektir. Göndermiş olduğunuz kiril alfabeli isimler ingilizce olarak UETDS sistemlerine iletilir.
      /// </remarks>
      [HttpPost]
      public async Task<IActionResult> YolcuEkle([FromBody] SeferYolcuBilgileri m, int uetdsSeferReferansNo)
      {
         var client = servis.baglanTest();
         uetdsYtsUser servisUser = new()
         {
            kullaniciAdi = servis.kullaniciTest,
            sifre = servis.sifreTest
         };
         try
         {
            var kullaniciKontrolSonuc = await client.kullaniciKontrolAsync(client.ClientCredentials.UserName.UserName, client.ClientCredentials.UserName.Password);
            uetdsAriziSeferYolcuBilgileriInput grupBilgi = new()
            {
               grupId = Convert.ToInt32(m.grupId),
               adi = yardim.RussianToEng(m.adi),
               soyadi = yardim.RussianToEng(m.soyadi),
               cinsiyet = m.cinsiyet,
               koltukNo = m.koltukNo,
               tcKimlikPasaportNo = m.tcKimlikPasaportNo == "" ? "YOK" : m.tcKimlikPasaportNo,
               telefonNo = m.telefonNo,
               uyrukUlke = m.uyrukUlke
            };
            var yolcuEkleSonuc = await client.yolcuEkleAsync(servisUser, uetdsSeferReferansNo, grupBilgi);
            if (yolcuEkleSonuc.@return.sonucKodu != 0)
            {
               NResult _yolcuEkleSonuc = new()
               {
                  Message = yolcuEkleSonuc.@return.sonucMesaji,
                  ResponseType = ResponseType.Error
               };
               return Ok(_yolcuEkleSonuc);
            }
            else
            {
               NResult _yolcuEkleSonuc = new()
               {
                  Message = yolcuEkleSonuc.@return.sonucMesaji,
                  ResponseType = ResponseType.Success
               };
               return Ok(_yolcuEkleSonuc);
            }

         }
         catch (Exception ex)
         {
            NResult result = new()
            {
               Message = "UETDS Bağlantısı başarısız. UETDS Sonuç:  " + ex.Message + "",
               ResponseType = ResponseType.Success
            };
            return Ok(result);
         }
      }
      /// <summary>
      /// 
      /// </summary>
      /// <remarks>
      /// SeferEkle ekleme işleminden sonra size verilen uetdsSeferReferansNo bilgisi ile işlem yapmalısınız. 
      /// <br></br>
      /// PersonelEkle işleminde Ad ve Soyad UETDS sisteminde kiril alfabesi kabul edilmediği için göndereceğiniz isim örneğin полина api tarafından UETDS sistemine Polina olarak gidecektir. Göndermiş olduğunuz kiril alfabeli isimler ingilizce olarak UETDS sistemlerine iletilir.
      /// <br></br>
      /// turKodu Alanı yok ise 0 gönderilebilir.
      /// <br></br>
      /// Cinsiyet 1 ise erkek 0 ise kadındır.
      /// <br></br>
      /// Uyruk bilgisi zorunlu değildir. Boş gönderilebilir.
      /// <br></br>
      /// TC zorunlu değildir. Boş gönderilebilir.
      /// </remarks>
      [HttpPost]
      public async Task<IActionResult> PersonelEkle([FromBody] PersonelBilgileri m, int uetdsSeferReferansNo)
      {
         var client = servis.baglanTest();
         uetdsYtsUser servisUser = new()
         {
            kullaniciAdi = servis.kullaniciTest,
            sifre = servis.sifreTest
         };
         try
         {
            var kullaniciKontrolSonuc = await client.kullaniciKontrolAsync(client.ClientCredentials.UserName.UserName, client.ClientCredentials.UserName.Password);
            uetdsAriziSeferPersonelBilgileriInput[] personelBilgi = new uetdsAriziSeferPersonelBilgileriInput[] {
                                                        new uetdsAriziSeferPersonelBilgileriInput() {
                                                            turKodu = 0,
                                                            adi = yardim.RussianToEng(m.adi),
                                                            soyadi = yardim.RussianToEng(m.soyadi),
                                                            telefon = m.telefon,
                                                            cinsiyet = m.cinsiyet == "1" ? "E" : "K",
                                                            uyrukUlke = m.uyrukUlke == "" ? "YOK" : m.uyrukUlke,
                                                            tcKimlikPasaportNo = m.tcKimlikPasaportNo == "" ? "YOK" : m.tcKimlikPasaportNo
                                                        } };

            var personelEkleSonuc = await client.personelEkleAsync(servisUser, uetdsSeferReferansNo, personelBilgi);
            if (personelEkleSonuc.@return.sonucKodu != 0)
            {
               NResult _personelEkleSonuc = new()
               {
                  Message = personelEkleSonuc.@return.sonucMesaji,
                  ResponseType = ResponseType.Error
               };
               return Ok(_personelEkleSonuc);
            }
            else
            {
               NResult _personelEkleSonuc = new()
               {
                  Message = personelEkleSonuc.@return.sonucMesaji,
                  ResponseType = ResponseType.Success
               };
               return Ok(_personelEkleSonuc);
            }

         }
         catch (Exception ex)
         {
            NResult result = new()
            {
               Message = "UETDS Bağlantısı başarısız. UETDS Sonuç:  " + ex.Message + "",
               ResponseType = ResponseType.Success
            };
            return Ok(result);
         }
      }
   }
}
