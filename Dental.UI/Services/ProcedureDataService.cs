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
    public class ProcedureDataService : IProcedureDataService
    {
        private readonly HttpClient _httpClient;

        public ProcedureDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Procedure>> GetProcedures() =>
            await JsonSerializer.DeserializeAsync<IEnumerable<Procedure>>
                (await _httpClient.GetStreamAsync($"api/procedures"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        public async Task<Procedure> GetProcedure(int ID)
        {
            return await JsonSerializer.DeserializeAsync<Procedure>
                (await _httpClient.GetStreamAsync($"api/procedures/{ID}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<Procedure> AddProcedure(Procedure procedure)
        {
            var ProcedureJson =
                new StringContent(JsonSerializer.Serialize(procedure), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/procedures", ProcedureJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Procedure>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateProcedure(Procedure procedure)
        {
            var ProcedureJson =
                new StringContent(JsonSerializer.Serialize(procedure), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/procedures", ProcedureJson);
        }

        public async Task DeleteProcedure(int ProcedureId)
        {
            await _httpClient.DeleteAsync($"api/procedures/{ProcedureId}");
        }
    }
}
