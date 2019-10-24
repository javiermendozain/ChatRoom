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
    public class EnroladosProvider : IEnroladosProvider
    {
        private readonly IHttpClientFactory httpClientFactory;

        public EnroladosProvider(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<int> AddAsync(Enrolado enrolado)
        {
        // Create httpClient instance
            var client = httpClientFactory.CreateClient("chatService");

            var body = new StringContent(JsonConvert.SerializeObject(enrolado), System.Text.Encoding.UTF8, "application/json");

            // Getting response of request POST at httpClient created as service on ConfigureServices
            var response = await client.PostAsync("api/enrolados", body);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                // Deserialize content with fastest seserializer included in .NET 3.0
                // Looks for more info https://www.newtonsoft.com/json
                var result = JsonConvert.DeserializeObject<int>(content);

                // If correct is added, returns true and result
                return (result);
            }

            return (0);
        }

        public async Task<bool> UpdateAsync(string Id, Enrolado enrolado)
        {
            // Create httpClient instance
            var client = httpClientFactory.CreateClient("chatService");

            var body = new StringContent(JsonConvert.SerializeObject(enrolado), System.Text.Encoding.UTF8, "application/json");

            // Getting response of request GET at httpClient created as service on ConfigureServices
            var response = await client.PutAsync($"api/enrolados/{Id}", body);

            if (response.IsSuccessStatusCode)
            {
                // Getting content as string
                var content = await response.Content.ReadAsStringAsync();

                // Deserialize content with fastest seserializer included in .NET 3.0
                // Looks for more info https://www.newtonsoft.com/json
                var result = JsonConvert.DeserializeObject<bool>(content);

                // Finally return the enrolados collections
                return result;
            }

            return false;
        }
    }
}
