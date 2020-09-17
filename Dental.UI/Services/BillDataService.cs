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
    public class BillDataService : IBillDataService
    {
        private readonly HttpClient _httpClient;

        public BillDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Bill>> GetBills() =>
            await JsonSerializer.DeserializeAsync<IEnumerable<Bill>>
                (await _httpClient.GetStreamAsync($"api/bills"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        public async Task<Bill> GetBill(int ID)
        {
            return await JsonSerializer.DeserializeAsync<Bill>
                (await _httpClient.GetStreamAsync($"api/bills/{ID}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<Bill> AddBill(Bill bill)
        {
            var BillJson =
                new StringContent(JsonSerializer.Serialize(bill), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/bills", BillJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Bill>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateBill(Bill bill)
        {
            var BillJson =
                new StringContent(JsonSerializer.Serialize(bill), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/bills", BillJson);
        }

        public async Task DeleteBill(int BillId)
        {
            await _httpClient.DeleteAsync($"api/bills/{BillId}");
        }
    }
}

