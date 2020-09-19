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
    public class AppointmentDataService : IAppointmentDataService
    {
        private readonly HttpClient _httpClient;

        public AppointmentDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Appointment>> GetAppointments() =>
            await JsonSerializer.DeserializeAsync<IEnumerable<Appointment>>
                (await _httpClient.GetStreamAsync($"api/appointments"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        public async Task<Appointment> GetAppointment(int ID)
        {
            return await JsonSerializer.DeserializeAsync<Appointment>
                (await _httpClient.GetStreamAsync($"api/appointments/{ID}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<Appointment> AddAppointment(Appointment appointment)
        {
            var AppointmentJson =
                new StringContent(JsonSerializer.Serialize(appointment), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/appointments", AppointmentJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Appointment>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateAppointment(Appointment appointment)
        {
            var AppointmentJson =
                new StringContent(JsonSerializer.Serialize(appointment), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/appointments", AppointmentJson);
        }

        public async Task DeleteAppointment(int AppointmentId)
        {
            await _httpClient.DeleteAsync($"api/appointments/{AppointmentId}");
        }
    }
}

