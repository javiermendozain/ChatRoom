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
    public class UsuariosProvider : IUsuariosProvider
    {
        private readonly IHttpClientFactory httpClientFactory;

        public UsuariosProvider(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<bool> AddAsync(Usuario usuario)
        {
            // Create httpClient instance
            var client = httpClientFactory.CreateClient("chatService");

            var body = new StringContent(JsonConvert.SerializeObject(usuario), System.Text.Encoding.UTF8, "application/json");

            // Getting response of request POST at httpClient created as service on ConfigureServices
            var response = await client.PostAsync("api/usuarios", body);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                try
                {
                    // Deserialize content with fastest seserializer included in .NET 3.0
                    // Looks for more info https://www.newtonsoft.com/json
                    var result = JsonConvert.DeserializeObject<bool>(content);

                    // If correct is added, returns true and result
                    return result;
                    
                }
                catch (System.Exception)
                {
                    
                    return false;
                }
            }
            return false;
        }
        public async Task<ICollection<Usuario>> GetAllByStatusAsync(int status)
        {
             // Create httpClient instance
            var client = httpClientFactory.CreateClient("chatService");

            // Getting response of request GET at httpClient created as service on ConfigureServices
            var response = await client.GetAsync($"api/usuarios/{status}");

            if (response.IsSuccessStatusCode)
            {
                // Getting content as string
                var content = await response.Content.ReadAsStringAsync();

                // Deserialize content with fastest seserializer included in .NET 3.0
                // Looks for more info https://www.newtonsoft.com/json
                var result = JsonConvert.DeserializeObject<ICollection<Usuario>>(content);

                // Finally return the usuario collection
                return result;
            }

            return null;
        }

        public  async Task<bool> UpdateAsync(string Id, Usuario usuario)
        {
            // Create httpClient instance
            var client = httpClientFactory.CreateClient("chatService");

            var body = new StringContent(JsonConvert.SerializeObject(usuario), System.Text.Encoding.UTF8, "application/json");

            // Getting response of request GET at httpClient created as service on ConfigureServices
            var response = await client.PutAsync($"api/usuarios/{Id}", body);

            if (response.IsSuccessStatusCode)
            {
                // Getting content as string
                var content = await response.Content.ReadAsStringAsync();

                // Deserialize content with fastest seserializer included in .NET 3.0
                // Looks for more info https://www.newtonsoft.com/json
                var result = JsonConvert.DeserializeObject<bool>(content);

                // Finally return the usuario collection
                return result;
            }

            return false;
        }

        
    }
}
