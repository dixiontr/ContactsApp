using System.Net;

namespace ContactsApp.Core.Customs.Exceptions
{
    public class UnhandledException : aBaseException
    {
        public UnhandledException()
        {
            StatusCode = HttpStatusCode.InternalServerError;
            Message = "Beklenmedik bir hata oluştu";
            Success = false;
        }
    }
}