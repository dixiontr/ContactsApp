using System.Net;
using ContactsApp.Core.Wrappers;

namespace ContactsApp.Core.Customs.Exceptions
{
    public abstract class aBaseException
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message {get;set;}
        public bool Success {get;set;}


        public BaseResponse HandleException()
        {
            return new BaseResponse()
            {
                Status = this.StatusCode,
                Message = this.Message,
                Success = this.Success
            };
        }
    }

}