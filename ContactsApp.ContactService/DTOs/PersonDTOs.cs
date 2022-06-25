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
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string Company { get; set; }
        
        public DateTime CreatedOn = DateTime.Now;
        public List<CreateContactInformationDTO> ContactInformations { get; set; }
        
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
        
        public DateTime UpdatedOn = DateTime.Now;

        public List<UpdateContactInformationDTO> ContactInformations { get; set; }
    }

    public record DeletePersonDTO : IDto
    {
        public Guid Id { get; set; }
    }
    
    

}