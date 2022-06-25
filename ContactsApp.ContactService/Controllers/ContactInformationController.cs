using ContactsApp.ContactService.DTOs;
using ContactsApp.ContactService.Entities;
using ContactsApp.ContactService.Extensions;
using ContactsApp.ContactService.UnitOfWork;
using ContactsApp.Core.Customs.Exceptions;
using ContactsApp.Core.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.ContactService.Controllers
{
    [ApiController]
    [Route("contactinformations")]
    public class ContactInformationController : ControllerBase
    {
        private IContactUnitOfWork _unitOfWork;

        public ContactInformationController(IContactUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("{personId}")]
        public async Task<BaseResponse> Create(Guid personId, CreateContactInformationDTO createContactInformationDto)
        {
            if (personId == Guid.Empty) return new InvalidModelException("Kişi Id").HandleException();
            
            if (!ModelState.IsValid) return new InvalidModelException(ModelState.GetErrorMessages()).HandleException();
            
            Person person = await _unitOfWork.PersonRepository.GetAsync(personId);
            
            if (person.Equals(default(Person))) return new NotFoundException("İletişim bilgisi eklemeye çalıştığınız kişi").HandleException();
            

            createContactInformationDto.PersonId = personId;

            ContactInformation contactInformation = createContactInformationDto.AsContactInformation();

            await _unitOfWork.ContactInformationRepository.CreateAsync(contactInformation);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse()
            {
                Message = "İletişim bilgisi başarı ile eklendi.",
                Data = contactInformation
            };
        }

        [HttpPut("{id}")]
        public async Task<BaseResponse> Update(Guid id, UpdateContactInformationDTO updateContactInformationDto)
        {
            if (id == Guid.Empty) return new InvalidModelException("Id").HandleException();
            
            if (!ModelState.IsValid) return new InvalidModelException(ModelState.GetErrorMessages()).HandleException();

            ContactInformation contactInformation = await _unitOfWork.ContactInformationRepository.GetAsync(id);
            
            if (contactInformation.Equals(default(ContactInformation))) return new NotFoundException("Düzenlemek istediğiniz iletişim bilgisi").HandleException();

            contactInformation.UpdateContactInformationByDTO(updateContactInformationDto);

            _unitOfWork.ContactInformationRepository.Update(contactInformation);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse()
            {
                Data = contactInformation,
                Message = "İletişim bilgisi başarı ile güncellendi"
            };
        }

        [HttpDelete("{id}")]
        public async Task<BaseResponse> Delete(Guid id)
        {
            if (id == Guid.Empty) return new InvalidModelException("Id").HandleException();

            ContactInformation contactInformation = await _unitOfWork.ContactInformationRepository.GetAsync(id);
            if (contactInformation.Equals(default(ContactInformation))) return new NotFoundException("Silmek istediğiniz iletişim bilgisi").HandleException();

            _unitOfWork.ContactInformationRepository.Remove(contactInformation);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse()
            {
                Message = "İletişim bilgisi başarı ile silindi"
            };
        }
    }

}