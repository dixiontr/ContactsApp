using System.Net;
using ContactsApp.ContactService.DTOs;
using ContactsApp.ContactService.Entities;
using ContactsApp.ContactService.Extensions;
using ContactsApp.ContactService.UnitOfWork;
using ContactsApp.Core.Customs.Exceptions;
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
        public async Task<BaseResponse> Get()
        {
            var persons = await _unitOfWork.PersonRepository
                                        .GetAllAsync<PersonDTO>(x => x.AsPersonDTO());

            return new BaseResponse()
            {
                Data = persons,
                Message = "Kişiler başarı ile getirildi.",
            };
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new InvalidModelException("Id").HandleException();
            }
            
            Person person = await _unitOfWork.PersonRepository
                                    .GetAsyncWithInclude<List<ContactInformation>>(id,x => x.ContactInformations);

            if (person.Equals(default(Person)))
            {
                return new NotFoundException("Aradığınız Kişi").HandleException();
            }

            PersonDetailDTO personDetailDto = person.AsPersonDetailDTO();
            
            return new BaseResponse()
            {
                Data = personDetailDto,
                Message = "Kişi başarı ile getirildi.",
            };
        }

        [HttpPost]
        public async Task<BaseResponse> CreateAsync(CreatePersonDTO createPersonDto)
        {
            if (!ModelState.IsValid)
            {
                return new InvalidModelException(ModelState.GetErrorMessages()).HandleException();
            }
            
            Person person = createPersonDto.AsPerson();

            await _unitOfWork.PersonRepository.CreateAsync(person);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse()
            {
                Data = person,
                Message = "Kişi başarı ile oluşturuldu.",
            };
        }

        [HttpPut("{id}")]
        public async Task<BaseResponse> UpdateAsync(Guid id,UpdatePersonDTO personDto)
        {
            if (id == Guid.Empty)
            {
                return new InvalidModelException("Id").HandleException();
            }
            if (!ModelState.IsValid)
            {
                return new InvalidModelException(ModelState.GetErrorMessages()).HandleException();
            }
            Person person =
                await _unitOfWork.PersonRepository.GetAsyncWithInclude<List<ContactInformation>>(id,
                    x => x.ContactInformations);
            if (person.Equals(default(Person)))
            {
                return new NotFoundException("Güncellemeye çalıştığınız kişi").HandleException();
            }

            person.UpdatePersonByDTO(personDto);
            _unitOfWork.PersonRepository.Update(person);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse()
            {
                Data = person,
                Message = "Kişi başarı ile güncellendi",
            };
        }

        [HttpDelete("{id}")]
        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new InvalidModelException("Id").HandleException();
            }
            Person person = await _unitOfWork.PersonRepository.GetAsync(id);
            if (person.Equals(default(Person)))
            {
                return new NotFoundException("Silmeye çalıştığınız kişi").HandleException();
            }

            _unitOfWork.PersonRepository.Remove(person);
            await _unitOfWork.SaveChangesAsync();
            
            return new BaseResponse()
            {
                Message = "Kişi başarı ile silindi",
            }; 
        }
    }

}