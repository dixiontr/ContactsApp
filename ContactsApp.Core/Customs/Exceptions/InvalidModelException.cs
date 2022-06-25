using System.Net;
using ContactsApp.Core.Wrappers;

namespace ContactsApp.Core.Customs.Exceptions
{

    public class InvalidModelException : aBaseException
    {
        public InvalidModelException(string parameter)
        {
            StatusCode = HttpStatusCode.BadRequest;
            Message = $"{parameter} paremetresini hatalı gönderdiniz. Parametreleri kontol edip tekrar deneyiniz";
            Success = false;
        }

        public InvalidModelException(List<string> errorMessages)
        {
            StatusCode = HttpStatusCode.BadRequest;
            Message = string.Join(Environment.NewLine, errorMessages);
            Success = false;
        }
        
    }

}