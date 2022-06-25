using System.ComponentModel.DataAnnotations;
using ContactsApp.ContactService.Entities;
using ContactsApp.ContactService.Extensions;
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
        [Required(ErrorMessage = "İletişim türü alanı, gönderilmesi zorunlu bir alandır.")]
        public InformationType InformationType { get; set; }
        [Required(ErrorMessage = "İletişim bilgisi alanı, gönderilmesi zorunlu bir alandır.")]
        public string Information { get; set; }
        public DateTime CreatedOn = DateTime.Now.SetKindUtc();
    }
    public record UpdateContactInformationDTO : IDto
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "İletişim türü alanı, gönderilmesi zorunlu bir alandır.")]
        public InformationType InformationType { get; set; }
        [Required(ErrorMessage = "İletişim bilgisi alanı, gönderilmesi zorunlu bir alandır.")]
        public string Information { get; set; }
        public DateTime UpdatedOn = DateTime.Now.SetKindUtc();
    }

    public record DeleteContactInformationDTO : IDto
    {
        [Required(ErrorMessage = "İletişim bilgisi Idsi, gönderilmesi zorunlu bir alandır.")]
        public Guid Id { get; set; }
    }

}