using Dental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dental.UI.Services
{
    public class ClientDataService : IClientDataService
    {
        private readonly HttpClient _httpClient;

        public ClientDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Client>> GetClients()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Client>>
                (await _httpClient.GetStreamAsync($"api/clients"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<Client> GetClient(int ID)
        {
            return await JsonSerializer.DeserializeAsync<Client>
                (await _httpClient.GetStreamAsync($"api/clients/{ID}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<Client> AddClient(Client account)
        {
            var ClientJson =
                new StringContent(JsonSerializer.Serialize(account), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/clients", ClientJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Client>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateClient(Client account)
        {
            var ClientJson =
                new StringContent(JsonSerializer.Serialize(account), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/clients", ClientJson);
        }

        public async Task DeleteClient(int ClientId)
        {
            await _httpClient.DeleteAsync($"api/clients/{ClientId}");
        }
    }
}
