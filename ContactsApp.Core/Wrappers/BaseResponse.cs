using System.Net;

namespace ContactsApp.Core.Wrappers
{

    public class BaseResponse : BaseResponse<object>
    {
        public BaseResponse()
        {
            Status = HttpStatusCode.OK;
            Success = true;
        }
    }

    public class BaseResponse<T>
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public bool Success { get; set; }
    }

}