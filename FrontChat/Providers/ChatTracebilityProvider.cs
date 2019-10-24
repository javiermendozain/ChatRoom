using System.Text;
using System.Threading;
using System.Net.WebSockets;
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
    public class ChatTracebilityProvider : IChatTracebilityProvider
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ChatTracebilityProvider(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<bool> AddAsync(ClientWebSocket clientWS, ChatTracebility chatTracebility)
        {
           
            // Deserialize content with fastest seserializer included in .NET 3.0
            // Looks for more info https://www.newtonsoft.com/json
            var body = JsonConvert.SerializeObject(chatTracebility);
           
            var bytes = Encoding.UTF8.GetBytes(body);
            await clientWS.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
            
            return true;
        }

        public async Task<ICollection<ChatTracebility>> GetAllBySalaAsync(int IdSala)
        {
            // Create httpClient instance
            var client = httpClientFactory.CreateClient("chatService");

            // Getting response of request GET at httpClient created as service on ConfigureServices
            var response = await client.GetAsync($"api/chattracebility/sala/{IdSala}");

            if (response.IsSuccessStatusCode)
            {
                // Getting content as string
                var content = await response.Content.ReadAsStringAsync();

                // Deserialize content with fastest seserializer included in .NET 3.0
                // Looks for more info https://www.newtonsoft.com/json
                var result = JsonConvert.DeserializeObject<ICollection<ChatTracebility>>(content);

                // Finally return the chatTracebility collection
                return result;
            }

            return null;
        }

        public async Task<ICollection<ChatTracebility>> GetAllByUserSalaAsync(string IdUsuarios, int IdSala)
        {
             // Create httpClient instance
            var client = httpClientFactory.CreateClient("chatService");

            // Getting response of request GET at httpClient created as service on ConfigureServices
            var response = await client.GetAsync($"api/chattracebility/{IdUsuarios}/{IdSala}");

            if (response.IsSuccessStatusCode)
            {
                // Getting content as string
                var content = await response.Content.ReadAsStringAsync();

                // Deserialize content with fastest seserializer included in .NET 3.0
                // Looks for more info https://www.newtonsoft.com/json
                var result = JsonConvert.DeserializeObject<ICollection<ChatTracebility>>(content);

                // Finally return the chatTracebility collection
                return result;
            }

            return null;
        }
    }
}
