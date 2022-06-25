using System.ComponentModel.DataAnnotations;
using ContactsApp.Core.Interfaces.Entity;

namespace ContactsApp.ContactService.Entities
{

    public class ContactInformation : IEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        [Required]
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
        [Required]
        public InformationType InformationType { get; set; }
        [Required]
        public string Information { get; set; }
    }

}