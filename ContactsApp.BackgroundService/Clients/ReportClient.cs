using ContactsApp.BackgroundService.Entities;

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
        public static async Task Ready(Guid id)
        {
            await UpdateReportStatus(id, ReportStatus.Ready);
        }
        public static async Task Failed(Guid id)
        {
            await UpdateReportStatus(id, ReportStatus.Failed);
        }
        
    }

}