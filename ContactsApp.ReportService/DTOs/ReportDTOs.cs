using ContactsApp.Core.Interfaces.DTO;
using ContactsApp.ReportService.Entities;

namespace ContactsApp.ReportService.DTOs
{

    public record ReportDTO : IDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public ReportStatus Status { get; set; }
    }

    public record ReportDetailDTO : IDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public ReportStatus Status { get; set; }
        public List<ReportDataDTO> ReportDatas { get; set; }
    }

    public record ReportDataDTO : IDto
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneNumberCount { get; set; }
    }

}