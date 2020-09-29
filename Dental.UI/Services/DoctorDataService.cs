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
    public class DoctorDataService : IDoctorDataService
    {
        private readonly HttpClient _httpClient;

        public DoctorDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Doctor>> GetDoctors() =>
            await JsonSerializer.DeserializeAsync<IEnumerable<Doctor>>
                (await _httpClient.GetStreamAsync($"api/doctors"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        public async Task<Doctor> GetDoctor(int ID)
        {
            return await JsonSerializer.DeserializeAsync<Doctor>
                (await _httpClient.GetStreamAsync($"api/doctors/{ID}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<Doctor> AddDoctor(Doctor doctor)
        {
            var DoctorJson =
                new StringContent(JsonSerializer.Serialize(doctor), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/doctors", DoctorJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Doctor>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateDoctor(Doctor doctor)
        {
            var DoctorJson =
                new StringContent(JsonSerializer.Serialize(doctor), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/doctors", DoctorJson);
        }

        public async Task DeleteDoctor(int DoctorId)
        {
            await _httpClient.DeleteAsync($"api/doctors/{DoctorId}");
        }
    }
}
