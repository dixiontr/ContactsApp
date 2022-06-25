using System.ComponentModel.DataAnnotations;
using ContactsApp.Core.Interfaces.Entity;

namespace ContactsApp.ContactService.Entities
{

    public class Person : IEntity
    {
        [Required]
        public Guid Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
        [Required]
        [MaxLength(250)]
        public string Company { get; set; }
        
        
        public List<ContactInformation> ContactInformations { get; set; }
    }

}