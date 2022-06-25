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
        public async Task<BaseResponse> Get()
        {
            var persons = await _unitOfWork.PersonRepository
                                        .GetAllAsync<PersonDTO>(x => x.AsDto<PersonDTO>(new PersonDTO()));

            return new BaseResponse()
            {
                Status = HttpStatusCode.OK,
                Data = persons,
                Message = "Kişiler başarı ile getirildi.",
                Success = true
            };
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse> Get(Guid id)
        {
            Person person = await _unitOfWork.PersonRepository.GetAsyncWithInclude<List<ContactInformation>>(id,x => x.ContactInformations);

            if (object.Equals(person,default(Person)))
            {
                return new BaseResponse()
                {
                    Status = HttpStatusCode.NotFound,
                    Message = "Güncellemeye çalıştığınız kişi bulunamadı. Lütfen tekrar deneyiniz",
                    Success = false,
                    Data = id
                };
            }
            
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
                    Id = x.Id,
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

            await _unitOfWork.PersonRepository.CreateAsync(person);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse()
            {
                Status = HttpStatusCode.Created,
                Data = person,
                Message = "Kişi başarı ile oluşturuldu.",
                Success = true
            };
        }

        [HttpPut("{id}")]
        public async Task<BaseResponse> UpdateAsync(Guid id,UpdatePersonDTO personDto)
        {
            Person person =
                await _unitOfWork.PersonRepository.GetAsyncWithInclude<List<ContactInformation>>(id,
                    x => x.ContactInformations);
            if (object.Equals(person,default(Person)))
            {
                return new BaseResponse()
                {
                    Status = HttpStatusCode.NotFound,
                    Message = "Güncellemeye çalıştığınız kişi bulunamadı. Lütfen tekrar deneyiniz",
                    Success = false,
                    Data = personDto
                };
            }

            person.Name = personDto.Name;
            person.Surname = personDto.Surname;
            person.Company = personDto.Company;
            person.UpdatedOn = personDto.UpdatedOn;

            foreach (var contactInformationDto in personDto.ContactInformations)
            {
                var existingInformation =
                    person.ContactInformations.FirstOrDefault(i => i.Id == contactInformationDto.Id);
                if (existingInformation != null)
                {
                    existingInformation.Information = contactInformationDto.Information;
                    existingInformation.InformationType = contactInformationDto.InformationType;
                    existingInformation.UpdatedOn = contactInformationDto.UpdatedOn;
                }
                else
                {
                    person.ContactInformations.Add(new ContactInformation()
                    {
                        Information = contactInformationDto.Information,
                        InformationType = contactInformationDto.InformationType,
                        CreatedOn = contactInformationDto.UpdatedOn
                    });
                }
            }

            _unitOfWork.PersonRepository.Update(person);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse()
            {
                Data = person,
                Message = "Kişi başarı ile güncellendi",
                Status = HttpStatusCode.Accepted,
                Success = true
            };


        }

        [HttpDelete("{id}")]
        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            Person person = await _unitOfWork.PersonRepository.GetAsync(id);
            if (object.Equals(person,default(Person)))
            {
                return new BaseResponse()
                {
                    Status = HttpStatusCode.NotFound,
                    Message = "Silmeye çalıştığınız kişi bulunamadı. Lütfen tekrar deneyiniz",
                    Success = false,
                    Data = id
                };
            }

            _unitOfWork.PersonRepository.Remove(person);
            await _unitOfWork.SaveChangesAsync();
            
            return new BaseResponse()
            {
                Message = "Kişi başarı ile silindi",
                Status = HttpStatusCode.Accepted,
                Success = true
            }; 
        }
    }

}