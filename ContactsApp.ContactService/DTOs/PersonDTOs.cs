using System.ComponentModel.DataAnnotations;
using ContactsApp.Core.DTOs;
using ContactsApp.Core.Extensions;
using ContactsApp.Core.Interfaces.DTO;

namespace ContactsApp.ContactService.DTOs
{
    public record PersonDetailDTO : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        
        public DateTime CreatedOn { get; set; }
        
        public DateTime UpdatedOn { get; set; }

        public List<ContactInformationDTO> ContactInformations { get; set; }
    }

    public record PersonDTO : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public record CreatePersonDTO : IDto
    {
        public Guid Id = Guid.NewGuid();
        
        [Required(ErrorMessage = "İsim alanı, gönderilmesi zorunlu bir alandır.")]
        [MaxLength(50,ErrorMessage = "İsim 50 karakteri geçemez.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Soyisim alanı, gönderilmesi zorunlu bir alandır.")]
        [MaxLength(50,ErrorMessage = "Soyisim 50 karakteri geçemez.")]
        public string Surname { get; set; }
        
        [Required(ErrorMessage = "Şirket alanı, gönderilmesi zorunlu bir alandır.")]
        [MaxLength(250,ErrorMessage = "Şirket 250 karakteri geçemez.")]
        public string Company { get; set; }
        
        public DateTime CreatedOn = DateTime.Now.SetKindUtc();
        public List<CreateContactInformationDTO> ContactInformations { get; set; }
        
    }
    
    public record UpdatePersonDTO : IDto
    {
        [Required(ErrorMessage = "İsim alanı, gönderilmesi zorunlu bir alandır.")]
        [MaxLength(50,ErrorMessage = "İsim 50 karakteri geçemez.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Soyisim alanı, gönderilmesi zorunlu bir alandır.")]
        [MaxLength(50,ErrorMessage = "Soyisim 50 karakteri geçemez.")]
        public string Surname { get; set; }
        
        [Required(ErrorMessage = "Şirket alanı, gönderilmesi zorunlu bir alandır.")]
        [MaxLength(250,ErrorMessage = "Şirket 250 karakteri geçemez.")]
        public string Company { get; set; }
        
        public DateTime UpdatedOn = DateTime.Now.SetKindUtc();

        public List<UpdateContactInformationDTO> ContactInformations { get; set; }
    }
}