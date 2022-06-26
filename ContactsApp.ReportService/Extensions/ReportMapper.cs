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
            if (report.ReportDatas == null) report.ReportDatas = new List<ReportData>();
            return new ReportDetailDTO()
            {
                Id = report.Id,
                CreatedOn = report.CreatedOn,
                Status = report.Status,
                ReportDatas = report.ReportDatas.Select(x => x.AsReportDataDTO()).ToList()
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