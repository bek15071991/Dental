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
    public class PaySetupDataService : IPaySetupDataService
    {
        private readonly HttpClient _httpClient;

        public PaySetupDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<PaySetup>> GetPaySetups() =>
            await JsonSerializer.DeserializeAsync<IEnumerable<PaySetup>>
                (await _httpClient.GetStreamAsync($"api/paysetups"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        public async Task<PaySetup> GetPaySetup(int ID)
        {
            return await JsonSerializer.DeserializeAsync<PaySetup>
                (await _httpClient.GetStreamAsync($"api/paysetups/{ID}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<PaySetup> AddPaySetup(PaySetup paysetup)
        {
            var PaySetupJson =
                new StringContent(JsonSerializer.Serialize(paysetup), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/paysetups", PaySetupJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<PaySetup>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdatePaySetup(PaySetup paysetup)
        {
            var PaySetupJson =
                new StringContent(JsonSerializer.Serialize(paysetup), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/paysetups", PaySetupJson);
        }

        public async Task DeletePaySetup(int PaySetupId)
        {
            await _httpClient.DeleteAsync($"api/paysetups/{PaySetupId}");
        }
    }
}
