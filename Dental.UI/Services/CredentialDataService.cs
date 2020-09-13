using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dental.Data.Models;

namespace Dental.UI.Services
{
    public class CredentialDataService : ICredentialDataService
    {
        private readonly HttpClient _httpClient;

        public CredentialDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Credential>> GetCredentials() =>
            await JsonSerializer.DeserializeAsync<IEnumerable<Credential>>
                (await _httpClient.GetStreamAsync($"api/credentials"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        public async Task<Credential> GetCredential(int ID)
        {
            return await JsonSerializer.DeserializeAsync<Credential>
                (await _httpClient.GetStreamAsync($"api/credentials/{ID}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<Credential> AddCredential(Credential credential)
        {
            var CredentialJson =
                new StringContent(JsonSerializer.Serialize(credential), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/credentials", CredentialJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Credential>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateCredential(Credential credential)
        {
            var CredentialJson =
                new StringContent(JsonSerializer.Serialize(credential), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/credentials", CredentialJson);
        }

        public async Task DeleteCredential(int CredentialId)
        {
            await _httpClient.DeleteAsync($"api/credentials/{CredentialId}");
        }
    }
}
