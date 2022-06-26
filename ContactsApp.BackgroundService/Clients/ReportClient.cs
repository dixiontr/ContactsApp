using ContactsApp.BackgroundService.Entities;
using Microsoft.AspNetCore.Http;

namespace ContactsApp.BackgroundService.Clients
{
    public static class ReportClient
    {
        private static readonly HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7118")
        };

        public static async Task UpdateReportStatus(Guid id, ReportStatus status)
        {
            await _httpClient.GetAsync($"/reports/{id}/{status}");
        }

        public static async Task DataPulled(Guid id)
        {
            await UpdateReportStatus(id, ReportStatus.DataPulled);
        }
        public static async Task InProgress(Guid id)
        {
            await UpdateReportStatus(id, ReportStatus.InProgress);
        }
        public static async Task Ready(Guid id, string fileName)
        {
            await _httpClient.GetAsync($"/finishedreport/{id}/{fileName}");
        }
        public static async Task Failed(Guid id)
        {
            await UpdateReportStatus(id, ReportStatus.Failed);
        }
        
    }

}