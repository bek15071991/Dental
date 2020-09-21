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
    public class ScheduleDataService : IScheduleDataService
    {
        private readonly HttpClient _httpClient;

        public ScheduleDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Schedule>> GetSchedules() =>
            await JsonSerializer.DeserializeAsync<IEnumerable<Schedule>>
                (await _httpClient.GetStreamAsync($"api/schedules"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        public async Task<Schedule> GetSchedule(int ID)
        {
            return await JsonSerializer.DeserializeAsync<Schedule>
                (await _httpClient.GetStreamAsync($"api/schedules/{ID}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<Schedule> AddSchedule(Schedule schedule)
        {
            var ScheduleJson =
                new StringContent(JsonSerializer.Serialize(schedule), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/schedules", ScheduleJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Schedule>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateSchedule(Schedule schedule)
        {
            var ScheduleJson =
                new StringContent(JsonSerializer.Serialize(schedule), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/schedules", ScheduleJson);
        }

        public async Task DeleteSchedule(int ScheduleId)
        {
            await _httpClient.DeleteAsync($"api/schedules/{ScheduleId}");
        }
    }
}
