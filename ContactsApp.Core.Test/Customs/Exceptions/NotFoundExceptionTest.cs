using System.Net;
using ContactsApp.Core.Customs.Exceptions;
using ContactsApp.Core.Wrappers;
using Xunit;

namespace ContactsApp.Core.Test.Customs.Exceptions
{

    public class NotFoundExceptionTest
    {
        [Fact]
        public void Test_NotFoundException()
        {
            var parameter = "Aradığınız kişi ";
            var expectedOutput = new BaseResponse()
            {
                Status = HttpStatusCode.NotFound,
                Message = $"{parameter} bulunamadı. Lütfen gönderdiğiniz bilgileri kontrol ediniz",
                Success = false
            };

            var actualOutput = new NotFoundException(parameter).HandleException();

            Assert.Equal(expectedOutput.Message, actualOutput.Message);
        }
    }

}