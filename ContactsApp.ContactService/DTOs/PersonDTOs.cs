using System.ComponentModel.DataAnnotations;
using ContactsApp.Core.Interfaces.DTO;

namespace ContactsApp.ContactService.DTOs
{

    public record PersonDetailDTO : IDto
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
        [Required]
        [MaxLength(250)]
        public string Company { get; set; }
        
        public DateTimeOffset CreatedOn { get; set; }
        
        public DateTimeOffset UpdatedOn { get; set; }

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
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string Company { get; set; }
        
        public DateTimeOffset CreatedOn = DateTimeOffset.Now;

        public List<ContactInformationDTO> ContactInformations { get; set; }
        
    }
    
    public record UpdatePersonDTO : IDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string Company { get; set; }
        
        public DateTimeOffset UpdatedOn = DateTimeOffset.Now;

        public List<ContactInformationDTO> ContactInformations { get; set; }
    }

    public record DeletePersonDTO : IDto
    {
        public Guid Id { get; set; }
    }
    
    

}