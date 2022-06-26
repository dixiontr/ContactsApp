using ContactsApp.ReportService.DTOs;
using ContactsApp.ReportService.Entities;

namespace ContactsApp.ReportService.Extensions
{
    public static class ReportMapper
    {
        public static ReportDTO AsReportDTO(this Report report)
        {
            return new ReportDTO()
            {
                Id = report.Id,
                CreatedOn = report.CreatedOn,
                Status = report.Status
            };
        }

        public static ReportDetailDTO AsReportDetailDTO(this Report report)
        {
            return new ReportDetailDTO()
            {
                Id = report.Id,
                CreatedOn = report.CreatedOn,
                Status = report.Status,
                FileUrl = report.FileUrl,
            };
        }

        public static ReportDataDTO AsReportDataDTO(this ReportData reportData)
        {
            return new ReportDataDTO()
            {
                Id = reportData.Id,
                Location = reportData.Location,
                PersonCount = reportData.PersonCount,
                PhoneNumberCount = reportData.PhoneNumberCount
            };
        }
    }

}