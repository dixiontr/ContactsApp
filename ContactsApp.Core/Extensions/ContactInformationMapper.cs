using ContactsApp.Core.DTOs;
using ContactsApp.Core.Entities;

namespace ContactsApp.Core.Extensions
{

    public static class ContactInformationMapper
    {
        public static ContactInformation AsContactInformation(this CreateContactInformationDTO createContactInformationDto)
        {
            return new ContactInformation()
            {
                Id = createContactInformationDto.Id,
                PersonId = createContactInformationDto.PersonId,
                InformationType = createContactInformationDto.InformationType,
                Information = createContactInformationDto.Information,
                CreatedOn = createContactInformationDto.CreatedOn,
                UpdatedOn = createContactInformationDto.CreatedOn
            };
        }

        public static ContactInformation UpdateContactInformationByDTO(this ContactInformation contactInformation,
            UpdateContactInformationDTO updateContactInformationDto)
        {
            contactInformation.Information = updateContactInformationDto.Information;
            contactInformation.InformationType = updateContactInformationDto.InformationType;
            contactInformation.UpdatedOn = updateContactInformationDto.UpdatedOn;

            return contactInformation;
        }
    }

}