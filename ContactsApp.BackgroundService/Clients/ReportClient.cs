using System.Text;
using System.Text.Json;
using ContactsApp.BackgroundService.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ContactsApp.BackgroundService.Clients
{
    public class ReportClient
    {
        
        private readonly HttpClient _httpClient;
        
        public ReportClient()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            _httpClient = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri("http://reports:80")
            };
        }

        public async Task UpdateReportStatus(Guid id, ReportStatus status)
        {
            await _httpClient.GetAsync($"/reports/{id}/{status}");
        }

        public async Task DataPulled(Guid id)
        {
            await UpdateReportStatus(id, ReportStatus.DataPulled);
        }
        public async Task InProgress(Guid id)
        {
            await UpdateReportStatus(id, ReportStatus.InProgress);
        }
        public async Task Ready(Guid id, string fileName)
        {
            var data = new ReportUrlDTO()
            {
                Id = id,
                FileUrl = fileName
            };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"/finishedreport",content);
        }
        public async Task Failed(Guid id)
        {
            await UpdateReportStatus(id, ReportStatus.Failed);
        }
        
    }

}