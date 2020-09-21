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
    public class ChargeDataService : IChargeDataService
    {
        private readonly HttpClient _httpClient;

        public ChargeDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Charge>> GetCharges() =>
            await JsonSerializer.DeserializeAsync<IEnumerable<Charge>>
                (await _httpClient.GetStreamAsync($"api/charges"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        public async Task<Charge> GetCharge(int ID)
        {
            return await JsonSerializer.DeserializeAsync<Charge>
                (await _httpClient.GetStreamAsync($"api/charges/{ID}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<Charge> AddCharge(Charge charge)
        {
            var ChargeJson =
                new StringContent(JsonSerializer.Serialize(charge), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/charges", ChargeJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Charge>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateCharge(Charge charge)
        {
            var ChargeJson =
                new StringContent(JsonSerializer.Serialize(charge), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/charges", ChargeJson);
        }

        public async Task DeleteCharge(int ChargeId)
        {
            await _httpClient.DeleteAsync($"api/charges/{ChargeId}");
        }
    }
}
