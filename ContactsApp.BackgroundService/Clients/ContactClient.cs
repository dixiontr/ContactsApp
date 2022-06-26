using System.Net.Http.Json;
using ContactsApp.Core.DTOs;
using ContactsApp.Core.Wrappers;

namespace ContactsApp.BackgroundService.Clients
{

    public static class ContactClient
    {
        private static readonly HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7297")
        };
        
        public static async Task<List<ContactInformationDetailDTO>> GetContactInformationsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<BaseResponse<List<ContactInformationDetailDTO>>>("/contactinformations");
            Console.WriteLine(response.Message);
            return response.Data;
        }
        
    }

}