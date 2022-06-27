using System;
using System.Collections.Generic;
using System.Net;
using ContactsApp.Core.Customs.Exceptions;
using ContactsApp.Core.Wrappers;
using Xunit;

namespace ContactsApp.Core.Test.Customs.Exceptions
{

    public class InvalidModelExceptionTest
    {
        [Fact]
        public void Test_InvalidModelException_String_Initializer()
        {
            string parameter = "Kişi";

            var expectedOutput = new BaseResponse()
            {
                Status = HttpStatusCode.BadRequest,
                Message = $"{parameter} paremetresini hatalı gönderdiniz. Parametreleri kontol edip tekrar deneyiniz",
                Success = false,
            };

            var actualOutput = new InvalidModelException(parameter).HandleException();
            
            Assert.Equal(expectedOutput.Message,actualOutput.Message);
        }
        
        [Fact]
        public void Test_InvalidModelException_String_List_Initializer()
        {
            List<string> parameters = new List<string>()
            {
                "Hatalı parametre 1",
                "Hatalı parametre 2",
                "Hatalı parametre 3"
            };

            var expectedOutput = new BaseResponse()
            {
                Status = HttpStatusCode.BadRequest,
                Message =string.Join(Environment.NewLine, parameters),
                Success = false,
            };

            var actualOutput = new InvalidModelException(parameters).HandleException();
            
            Assert.Equal(expectedOutput.Message,actualOutput.Message);
        }
        
    }

}