using System.Net.Http.Json;
using ContactsApp.Core.DTOs;
using ContactsApp.Core.Wrappers;

namespace ContactsApp.BackgroundService.Clients
{
    public class ContactClient
    {
        private readonly HttpClient _httpClient;
        public ContactClient()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        
            _httpClient = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri("http://contact:80")
            };
            
        }
        public async Task<List<ContactInformationDetailDTO>> GetContactInformationsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<BaseResponse<List<ContactInformationDetailDTO>>>("/contactinformations");
            Console.WriteLine(response.Message);
            return response.Data;
        }
    }
}