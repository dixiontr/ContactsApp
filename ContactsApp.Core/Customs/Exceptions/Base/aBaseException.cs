using System.Net;
using ContactsApp.Core.Wrappers;

namespace ContactsApp.Core.Customs.Exceptions
{
    public abstract class aBaseException
    {
        public HttpStatusCode StatusCode { get; init; }
        public string Message {get;init;}
        public bool Success {get;init;}


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