using ContactsApp.ContactService.DTOs;
using ContactsApp.ContactService.Entities;

namespace ContactsApp.ContactService.Extensions
{

    public static class PersonMapper
    {
        public static PersonDetailDTO ToPersonDetailDTO(this Person person)
        {
            return new PersonDetailDTO()
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
        }

        public static Person UpdatePersonByDTO(this Person person, UpdatePersonDTO updatePersonDto)
        {
            person.Name = updatePersonDto.Name;
            person.Surname = updatePersonDto.Surname;
            person.Company = updatePersonDto.Company;
            person.UpdatedOn = updatePersonDto.UpdatedOn;

            foreach (var contactInformationDto in updatePersonDto.ContactInformations)
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
                        Id = contactInformationDto.Id,
                        Information = contactInformationDto.Information,
                        InformationType = contactInformationDto.InformationType,
                        CreatedOn = contactInformationDto.UpdatedOn
                    });
                }
            }

            return person;
        }

        public static Person ToPerson(this CreatePersonDTO createPersonDto)
        {
            return new Person()
            {
                Id = createPersonDto.Id,
                Name = createPersonDto.Name,
                Surname = createPersonDto.Surname,
                Company = createPersonDto.Company,
                CreatedOn = createPersonDto.CreatedOn,
                UpdatedOn = createPersonDto.CreatedOn,
                ContactInformations = createPersonDto.ContactInformations.Select(x => new ContactInformation()
                {
                    Id = x.Id,
                    InformationType = x.InformationType,
                    Information = x.Information,
                    CreatedOn = x.CreatedOn,
                    UpdatedOn = x.CreatedOn
                }).ToList()
            };
        }
    }

}