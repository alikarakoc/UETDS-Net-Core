namespace UetdsEntegrasyon.Models
{
   public enum ResponseType
   {
      Error = 0,
      Success = 1
   }
   public class NResult
   {
      public ResponseType ResponseType { get; set; }
      public string Message { get; set; }
   }
   public class NResult<T> : NResult
   {
      public T Data { get; set; }
   }
}
