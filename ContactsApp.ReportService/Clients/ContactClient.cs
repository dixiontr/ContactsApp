using ContactsApp.Core.DTOs;
using ContactsApp.Core.Wrappers;

namespace ContactsApp.ReportService.Clients
{

    public class ContactClient
    {
        private readonly HttpClient _httpClient;
        public ContactClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ContactInformationDetailDTO>> GetContactInformationsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<BaseResponse<List<ContactInformationDetailDTO>>>("/contactinformations");
            return response is null ? response.Data : new List<ContactInformationDetailDTO>();
        }
    }

}