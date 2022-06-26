using ClosedXML.Excel;
using ContactsApp.BackgroundService.Entities;

namespace ContactsApp.BackgroundService.Services
{
    public static class ExcelReportFileService 
    {
        public static string BuildFile(List<ReportDataDTO> data, string reportId)
        {
            using (var xlWorkbook = new XLWorkbook())
            {
                var workSheet = xlWorkbook.Worksheets.Add("Report");
                workSheet.FirstCell().InsertTable<ReportDataDTO>(data, false);
                
                workSheet.Columns().AdjustToContents();
                workSheet.Rows().AdjustToContents();
                var directory = Directory.GetCurrentDirectory().Replace("BackgroundService", "ReportService");
                var filename = $"{DateTime.Now.ToShortDateString()}-{reportId}.xlsx";

                var fileDirectory = Path.Combine(directory, "Reports", filename);
                xlWorkbook.SaveAs(fileDirectory);

                return filename;
            }
        }
    }
}