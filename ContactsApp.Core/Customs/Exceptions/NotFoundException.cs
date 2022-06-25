using System.Net;

namespace ContactsApp.Core.Customs.Exceptions
{

    public class NotFoundException : aBaseException
    {
        public NotFoundException(string entityName)
        {
            StatusCode = HttpStatusCode.NotFound;
            Message = $"{entityName} bulunamadı. Lütfen gönderdiğiniz bilgileri kontrol ediniz";
            Success = false;
            
        }
        
    }

}