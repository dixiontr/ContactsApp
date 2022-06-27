using System.Net;
using ContactsApp.Core.Customs.Exceptions;
using ContactsApp.Core.Wrappers;
using Xunit;

namespace ContactsApp.Core.Test.Customs.Exceptions
{

    public class UnhandledExceptionTest
    {
        [Fact]
        public void Test_UnhandledException()
        {
            var expectedOutput = new BaseResponse()
            {
                Status = HttpStatusCode.InternalServerError,
                Message = "Beklenmedik bir hata oluştu",
                Success = false
            };

            var actualOutput = new UnhandledException().HandleException();
            
            Assert.Equal(expectedOutput.Message,actualOutput.Message);
        }
        
    }

}