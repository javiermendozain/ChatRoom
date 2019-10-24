using FrontChat.Interfaces;
using FrontChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FrontChat.Providers
{
    public class SalasProvider : ISalasProvider
    {
        private readonly IHttpClientFactory httpClientFactory;

        public SalasProvider(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<bool> AddAsync(Sala sala)
        {
             // Create httpClient instance
            var client = httpClientFactory.CreateClient("chatService");

            var body = new StringContent(JsonConvert.SerializeObject(sala), System.Text.Encoding.UTF8, "application/json");

            // Getting response of request POST at httpClient created as service on ConfigureServices
            var response = await client.PostAsync("api/salas", body);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                // Deserialize content with fastest seserializer included in .NET 3.0
                // Looks for more info https://www.newtonsoft.com/json
                var result = JsonConvert.DeserializeObject<bool>(content);

                // If correct is added, returns true and result
                return (result);
            }

            return (false);
        }

        public async Task<ICollection<Sala>> GetAllAsync()
        {
             // Create httpClient instance
            var client = httpClientFactory.CreateClient("chatService");

            // Getting response of request GET at httpClient created as service on ConfigureServices
            var response = await client.GetAsync("api/salas/");

            if (response.IsSuccessStatusCode)
            {
                // Getting content as string
                var content = await response.Content.ReadAsStringAsync();

                // Deserialize content with fastest seserializer included in .NET 3.0
                // Looks for more info https://www.newtonsoft.com/json
                var result = JsonConvert.DeserializeObject<ICollection<Sala>>(content);

                // Finally return the Salas collection
                return result;
            }

            return null;
        }

        public async Task<ICollection<Sala>> GetAllByStatusAsync(int status)
        {
            // Create httpClient instance
            var client = httpClientFactory.CreateClient("chatService");

            // Getting response of request GET at httpClient created as service on ConfigureServices
            var response = await client.GetAsync($"api/salas/state/{status}");

            if (response.IsSuccessStatusCode)
            {
                // Getting content as string
                var content = await response.Content.ReadAsStringAsync();

                // Deserialize content with fastest seserializer included in .NET 3.0
                // Looks for more info https://www.newtonsoft.com/json
                var result = JsonConvert.DeserializeObject<ICollection<Sala>>(content);

                // Finally return the sala collection
                return result;
            }

            return null;
        }

        public async Task<ICollection<Sala>> GetAllByUserAsync(string id)
        {
               // Create httpClient instance
            var client = httpClientFactory.CreateClient("chatService");

            // Getting response of request GET at httpClient created as service on ConfigureServices
            var response = await client.GetAsync($"api/salas/usuario/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Getting content as string
                var content = await response.Content.ReadAsStringAsync();

                // Deserialize content with fastest seserializer included in .NET 3.0
                // Looks for more info https://www.newtonsoft.com/json
                var result = JsonConvert.DeserializeObject<ICollection<Sala>>(content);

                // Finally return the salas collection
                return result;
            }

            return null;
        }

        public async Task<bool> UpdateAsync(string Id, Sala sala)
        {
            // Create httpClient instance
            var client = httpClientFactory.CreateClient("chatService");

            var body = new StringContent(JsonConvert.SerializeObject(sala), System.Text.Encoding.UTF8, "application/json");

            // Getting response of request GET at httpClient created as service on ConfigureServices
            var response = await client.PutAsync($"api/salas/{Id}", body);

            if (response.IsSuccessStatusCode)
            {
                // Getting content as string
                var content = await response.Content.ReadAsStringAsync();

                // Deserialize content with fastest seserializer included in .NET 3.0
                // Looks for more info https://www.newtonsoft.com/json
                var result = JsonConvert.DeserializeObject<bool>(content);

                // Finally return the sala collection
                return result;
            }

            return false;
        }
    }
}
