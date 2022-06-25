using ContactsApp.ContactService.Entities;
using ContactsApp.Core.Interfaces.DTO;

namespace ContactsApp.ContactService.DTOs
{

    public record ContactInformationDTO : IDto
    {
        public Guid Id { get; set; }
        public InformationType InformationType { get; set; }
        public string Information { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }

    public record CreateContactInformationDTO : IDto
    {
        public Guid Id = Guid.NewGuid();
        public Guid PersonId { get; set; }
        public InformationType InformationType { get; set; }
        public string Information { get; set; }
        public DateTime CreatedOn = DateTime.Now;
    }
    public record UpdateContactInformationDTO : IDto
    {
        public Guid Id { get; set; }
        public InformationType InformationType { get; set; }
        public string Information { get; set; }
        public DateTime UpdatedOn = DateTime.Now;
    }

    public record DeleteContactInformationDTO : IDto
    {
        public Guid Id { get; set; }
    }

}