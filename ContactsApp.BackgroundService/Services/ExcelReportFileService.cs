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
                var filename = $"{DateTime.Now.ToShortDateString()}-{reportId}.xlsx";
                xlWorkbook.SaveAs(filename);

                return Path.Combine(Directory.GetCurrentDirectory(), filename);
            }
        }
    }
}