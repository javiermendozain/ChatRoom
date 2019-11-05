using Newtonsoft.Json;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using BackChat.Interfaces;
using BackChat.Models;
using BackChat.WebSocketManager;

namespace BackChat.Providers
{
    public class MessageHandlerProvider : WebSocketHandler
    {

        private readonly IChatTracebilityProvider chatTracebilityProvider;

        public MessageHandlerProvider(
            WebSocketConnectionManager webSocketConnectionManager,
            IChatTracebilityProvider chatTracebilityProvider ) : base(webSocketConnectionManager)
        {
            this.chatTracebilityProvider = chatTracebilityProvider;

        }

    public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);

            var socketId = WebSocketConnectionManager.GetId(socket);
           // await SendMessageToAllAsync($"{socketId} is now connected");
        }

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            // Getting Socket ID
            var socketId = WebSocketConnectionManager.GetId(socket);

            Console.WriteLine(" ---------- socketId: " + socketId);
            
            // Converte byte to String
            var messageReceived = Encoding.UTF8.GetString(buffer, 0, result.Count);

            // Deserialize content with fastest seserializer included in .NET 3.0
            // Looks for more info https://www.newtonsoft.com/json
            var message = JsonConvert.DeserializeObject<ChatTracebility>(messageReceived);

            await chatTracebilityProvider.AddAsync(message);
            Console.WriteLine(" ------------------ Back receive: " + message);
  
            // Send Message another users on the room
            await SendMessageToAllAsync(messageReceived);
            // await SendMessageAsync(messageReceived);
        }
    }
}