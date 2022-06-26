namespace ContactsApp.BackgroundService.Clients
{

    public class ReportClient
    {
        private static readonly HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7118")
        };

        public static async Task UpdateReportStatus(Guid id, ReportStatus status)
        {
            await _httpClient.GetAsync($"/reports/{id}/{status}");
        }
        
    }

}