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
    public class MessageDataService : IMessageDataService
    {
        private readonly HttpClient _httpClient;

        public MessageDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Message>> GetMessages() =>
            await JsonSerializer.DeserializeAsync<IEnumerable<Message>>
                (await _httpClient.GetStreamAsync($"api/messages"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        public async Task<Message> GetMessage(int ID)
        {
            return await JsonSerializer.DeserializeAsync<Message>
                (await _httpClient.GetStreamAsync($"api/messages/{ID}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<Message> AddMessage(Message credential)
        {
            var MessageJson =
                new StringContent(JsonSerializer.Serialize(credential), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/messages", MessageJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Message>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateMessage(Message credential)
        {
            var MessageJson =
                new StringContent(JsonSerializer.Serialize(credential), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/messages", MessageJson);
        }

        public async Task DeleteMessage(int MessageId)
        {
            await _httpClient.DeleteAsync($"api/messages/{MessageId}");
        }
    }
}
