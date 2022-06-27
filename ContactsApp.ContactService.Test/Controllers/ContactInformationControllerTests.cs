using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ContactsApp.ContactService.Controllers;
using ContactsApp.ContactService.Entities;
using ContactsApp.ContactService.UnitOfWork;
using ContactsApp.Core.DTOs;
using ContactsApp.Core.Entities;
using ContactsApp.Core.Extensions;
using ContactsApp.Core.Wrappers;
using Moq;
using Xunit;

namespace ContactsApp.ContactService.Test.Controllers
{
    public class ContactInformationControllerTests 
    {
        private readonly Mock<IContactUnitOfWork> _mockUoW;
        private readonly ContactInformationController _controller;

        public ContactInformationControllerTests()
        {
            _mockUoW = new Mock<IContactUnitOfWork>();
            _controller = new ContactInformationController(_mockUoW.Object);
        }

        [Fact]
        public async void Get_Action_Executes_Base_Response()
        {
            _mockUoW.Setup(uow =>uow.ContactInformationRepository.GetAllAsync()).ReturnsAsync((new List<ContactInformation>() {new ContactInformation(), new ContactInformation()}));

            var result = _controller.Get();

            var taskresponse = Assert.IsType<Task<BaseResponse>>(result);
            var response = Assert.IsType<BaseResponse>(await taskresponse);
            Assert.Equal(HttpStatusCode.OK,(HttpStatusCode)response.Status);
        }

        [Fact]
        public async void Get_Action_Executes_ReturnExactNumberOfEmployees()
        {
            _mockUoW.Setup(uow =>uow.ContactInformationRepository.GetAllWithSelectAsync(x => x.AsContactInformationDetailDto())).ReturnsAsync((new List<ContactInformationDetailDTO>() {new ContactInformationDetailDTO(), new ContactInformationDetailDTO()}));

            var result = _controller.Get();

            var taskresponse = Assert.IsType<Task<BaseResponse>>(result);
            var baseResponse = Assert.IsType<BaseResponse>(await taskresponse);
            Assert.Equal(HttpStatusCode.OK,(HttpStatusCode)baseResponse.Status);
            
            var data = Assert.IsType<List<ContactInformationDetailDTO>>(baseResponse.Data);
            Assert.Equal(2,data.Count);
        } 
        [Fact]
        public async void Create_Empty_Guid_Response()
        {
            var result =await _controller.Create(Guid.Empty, new CreateContactInformationDTO());
            
            var invalidModelError =Assert.IsType<BaseResponse>(result);
            Assert.Equal(HttpStatusCode.BadRequest,invalidModelError.Status);
            Assert.Equal(false,invalidModelError.Success);
            
            _mockUoW.Verify(x => x.ContactInformationRepository.CreateAsync(It.IsAny<ContactInformation>()), Times.Never);
            
        }
        [Fact]
        public async void Create_Empty_Object_Response()
        {
            _controller.ModelState.AddModelError("Name", "Name is required"); 
            var id = Guid.NewGuid();
            _mockUoW
                .Setup(uow =>uow
                                                    .PersonRepository
                                                    .GetAsync(id))
                                                    .ReturnsAsync(new Person(){Id = Guid.NewGuid(),Name = "Test",Surname = "Test",Company = "Test"});
            

            var result =await _controller.Create(id, new CreateContactInformationDTO());

            var invalidModelError =Assert.IsType<BaseResponse>(result);
            Assert.Equal(HttpStatusCode.BadRequest,invalidModelError.Status);
            Assert.Equal(false,invalidModelError.Success);
            
            _mockUoW.Verify(x => x.ContactInformationRepository.CreateAsync(It.IsAny<ContactInformation>()), Times.Never);
        }
        [Fact]
        public async void Create_Invalid_Object_Response()
        {
            _controller.ModelState.AddModelError("Name", "Name is required"); 
            var dto = new CreateContactInformationDTO()
            {
                Information = "Test Info"
            };
            var id = Guid.NewGuid();
            _mockUoW
                .Setup(uow =>uow
                    .PersonRepository
                    .GetAsync(id))
                .ReturnsAsync(new Person(){Id = Guid.NewGuid(),Name = "Test",Surname = "Test",Company = "Test"});

            var result = _controller.Create(id, dto);
            
            var taskresponse = Assert.IsType<Task<BaseResponse>>(result);
            var invalidModelError =Assert.IsType<BaseResponse>(await taskresponse);
            Assert.Equal(HttpStatusCode.BadRequest,invalidModelError.Status);
            Assert.Equal(false,invalidModelError.Success);
            
            _mockUoW.Verify(x => x.ContactInformationRepository.CreateAsync(It.IsAny<ContactInformation>()), Times.Never);
        }
        
        [Fact]
        public async void Update_Empty_Guid_Response()
        {
            var result =await _controller.Update(Guid.Empty, new UpdateContactInformationDTO());
            
            var invalidModelError =Assert.IsType<BaseResponse>(result);
            Assert.Equal(HttpStatusCode.BadRequest,invalidModelError.Status);
            Assert.Equal(false,invalidModelError.Success);
            
            _mockUoW.Verify(x => x.ContactInformationRepository.CreateAsync(It.IsAny<ContactInformation>()), Times.Never);
            
        }
        
        [Fact]
        public async void Update_Invalid_Object_GUID_Response()
        {
            _controller.ModelState.AddModelError("Name", "Name is required"); 
            var dto = new UpdateContactInformationDTO()
            {
                Information = "Test Info"
            };
            var id = Guid.NewGuid();

            _mockUoW
                .Setup(uow => uow.ContactInformationRepository.GetAsync(id))
                .ReturnsAsync(default(ContactInformation));

            var result = _controller.Update(id, dto);
            
            var taskresponse = Assert.IsType<Task<BaseResponse>>(result);
            var invalidModelError =Assert.IsType<BaseResponse>(await taskresponse);
            Assert.Equal(HttpStatusCode.BadRequest,invalidModelError.Status);
            Assert.Equal(false,invalidModelError.Success);
            
            _mockUoW.Verify(x => x.ContactInformationRepository.CreateAsync(It.IsAny<ContactInformation>()), Times.Never);
        }
    }
}