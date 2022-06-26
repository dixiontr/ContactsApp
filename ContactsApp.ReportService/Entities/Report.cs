using ContactsApp.Core.Interfaces.Entity;

namespace ContactsApp.ReportService.Entities
{
    public class Report : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public ReportStatus Status { get; set; }
        public List<ReportData> ReportDatas { get; set; }
    }
}