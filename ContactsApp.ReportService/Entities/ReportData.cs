using ContactsApp.Core.Interfaces.Entity;

namespace ContactsApp.ReportService.Entities
{

    public class ReportData : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneNumberCount { get; set; }
        public Guid ReportId { get; set; }
    }

}