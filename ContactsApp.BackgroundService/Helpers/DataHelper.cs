using ContactsApp.BackgroundService.Entities;
using ContactsApp.Core.DTOs;
using ContactsApp.Core.Entities;

namespace ContactsApp.BackgroundService.Helpers
{

    public static class DataHelper
    {
        public static List<ReportDataDTO> ToReportData(this List<ContactInformationDetailDTO> contacts)
        {
            var locationGroups = contacts.GetLocationGroups();
            
            var phoneNumbers = contacts.GetPhoneGroups();

            return locationGroups.ToReportDataList(phoneNumbers);
        }

        public static IEnumerable<IGrouping<string, ContactInformationDetailDTO>> GetLocationGroups(
            this List<ContactInformationDetailDTO> contacts)
        {
            return contacts
                .Where(x => x.InformationType == InformationType.Location)
                .GroupBy(x => x.Information);
        }
        public static IEnumerable<IGrouping<Guid, ContactInformationDetailDTO>> GetPhoneGroups(
            this List<ContactInformationDetailDTO> contacts)
        {
            return contacts.Where(x => x.InformationType == InformationType.PhoneNumber)
                .GroupBy(x => x.PersonId);
        }

        public static List<ReportDataDTO> ToReportDataList(
            this IEnumerable<IGrouping<string, ContactInformationDetailDTO>> locationGroups,
            IEnumerable<IGrouping<Guid, ContactInformationDetailDTO>> phoneNumbers)
        {
            return locationGroups.Select(locationGroup =>
            {
                var personIds = locationGroup.GetPersonIds();
                return new ReportDataDTO()
                {
                    Location = locationGroup.Key,
                    PersonCount = personIds.Count(),
                    PhoneNumberCount = phoneNumbers.GetPhoneNumberCount(personIds)
                };
            }).ToList();
        }

        public static List<Guid> GetPersonIds(this IGrouping<string, ContactInformationDetailDTO> locationGroup)
        {
            return locationGroup
                    .GroupBy(x => x.PersonId)
                    .Select(x => x.Key)
                    .ToList();
        }

        public static int GetPhoneNumberCount(this IEnumerable<IGrouping<Guid, ContactInformationDetailDTO>> phoneNumbers,
            List<Guid> personIds)
        {
            return phoneNumbers
                .Where(x => personIds.Contains(x.Key))
                .Sum(x => x.Count());
        }
    }

}