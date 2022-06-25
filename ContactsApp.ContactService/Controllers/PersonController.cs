using System.Net;
using ContactsApp.ContactService.DTOs;
using ContactsApp.ContactService.Entities;
using ContactsApp.ContactService.UnitOfWork;
using ContactsApp.Core.Mappers;
using ContactsApp.Core.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.ContactService.Controllers
{
    [ApiController]
    [Route("persons")]
    public class PersonController : ControllerBase
    {
        private readonly IContactUnitOfWork _unitOfWork;


        public PersonController(IContactUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public BaseResponse Get()
        {
            var persons = _unitOfWork.PersonRepository
                                        .GetAllAsync<PersonDTO>(x => x.AsDto<PersonDTO>(new PersonDTO())).Result;

            return new BaseResponse()
            {
                Status = HttpStatusCode.OK,
                Data = persons,
                Message = "Kişiler başarı ile getirildi.",
                Success = true
            };
        }

        [HttpGet("{id}")]
        public BaseResponse Get(Guid id)
        {
            Person person = _unitOfWork.PersonRepository.GetAsyncWithInclude<List<ContactInformation>>(x => x.ContactInformations).Result;

            PersonDetailDTO personDto = new PersonDetailDTO()
            {
                Id = person.Id,
                Name = person.Name,
                Surname = person.Surname,
                Company = person.Company,
                CreatedOn = person.CreatedOn,
                UpdatedOn = person.UpdatedOn,
                ContactInformations = person.ContactInformations.Select(x => new ContactInformationDTO()
                {
                    InformationType = x.InformationType,
                    Information = x.Information,
                    CreatedOn = x.CreatedOn,
                    UpdatedOn = x.UpdatedOn
                }).ToList()
            };
            
            return new BaseResponse()
            {
                Status = HttpStatusCode.OK,
                Data = personDto,
                Message = "Kişi başarı ile getirildi.",
                Success = true
            };
        }

        [HttpPost]
        public async Task<BaseResponse> CreateAsync(CreatePersonDTO personDto)
        {
            Person person = new Person()
            {
                Id = personDto.Id,
                Name = personDto.Name,
                Surname = personDto.Surname,
                Company = personDto.Company,
                CreatedOn = personDto.CreatedOn,
                UpdatedOn = personDto.CreatedOn,
                ContactInformations = personDto.ContactInformations.Select(x => new ContactInformation()
                {
                    Id = x.Id,
                    InformationType = x.InformationType,
                    Information = x.Information,
                    CreatedOn = x.CreatedOn,
                    UpdatedOn = x.CreatedOn
                }).ToList()
            };

            person = await _unitOfWork.PersonRepository.CreateAsync(person);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse()
            {
                Status = HttpStatusCode.Created,
                Data = person,
                Message = "Kişi başarı ile oluşturuldu.",
                Success = true
            };
        }
        
    }

}