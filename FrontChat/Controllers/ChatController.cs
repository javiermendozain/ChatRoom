using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Hanssens.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using FrontChat.Models;
using FrontChat.Interfaces;

namespace FrontChat.Controllers
{
    public class ChatController : Controller
    {
        private readonly IUsuariosProvider usuariosProvider;
        private readonly IChatTracebilityProvider chatTracebilityProvider;
        private readonly ISalasProvider salasProvider;
        private readonly IEnroladosProvider enroladosProvider;

        private LocalStorage LocalStorage;

        private ClientWebSocket client;

        ICollection<ChatTracebility> chatTracebility { get; set; }
               

        public ICollection<ChatTracebility> messagesChat { get; set; }

        ICollection<Sala> salasDisponble { get; set; }


        public ChatController(
            IUsuariosProvider usuariosProvider, 
            IChatTracebilityProvider chatTracebilityProvider,
            ISalasProvider salasProvider,
            IEnroladosProvider enroladosProvider)
        {
            this.usuariosProvider = usuariosProvider;
            this.chatTracebilityProvider = chatTracebilityProvider;
            this.salasProvider = salasProvider;
            this.enroladosProvider = enroladosProvider;

            // Inicializacion de LocaStorage
            this.LocalStorage =  new LocalStorage();
            this.client = new ClientWebSocket();

            RunWebSockets().GetAwaiter().GetResult();
        }

        private  async Task RunWebSockets()
        {
            await client.ConnectAsync(new Uri("ws://localhost:49803/notifications"), CancellationToken.None);
            Receiving();
        }

        private  async Task Receiving()
        {
            var buffer = new byte[1024 * 4];
            bool stop = true;
            while(stop)
            {
                var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var messageReceived = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    var message = JsonConvert.DeserializeObject<ChatTracebility>(messageReceived);
                    
                    Console.WriteLine("---------Mensaje recibido: | " + message.Message+" |---------");
                    messagesChat.Add(message);
                    ViewData["messagesChat"] = messagesChat;
                    View(ViewData);
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    break;
                }
                //stop = result.MessageType != WebSocketMessageType.Close;
            }
         
        }

        private async Task LogOut(){
            try
            {
                // Obteniendo usuario Logiado
                var usLogged = LocalStorage.Get<Usuario>("usuario");
                if(usLogged != null)
                {
                    usLogged.Status = 0;
                    var isSuccess =  await usuariosProvider.UpdateAsync(usLogged.UID, usLogged);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error al salir");
            }

            // Clears the in-memory contents of the LocalStorage, but leaves any persisted state on disk intact.
            LocalStorage.Clear();

            // Deletes the persisted file on disk, if it exists, but keeps the in-memory data intact.
            LocalStorage.Destroy(); 

        }

        private async Task Login(string nickname){
           
            string UID = Guid.NewGuid().ToString();
            Usuario  usuario = new  Usuario(){
                UID = UID,
                Name = nickname,
                Status = 1
            };
            var res = await usuariosProvider.AddAsync(usuario);
            if (res)
            {
                LocalStorage =  new LocalStorage();
                // store a usuario
                LocalStorage.Store("usuario", usuario);
                LocalStorage.Persist();
            }
        }

        private async Task Join (string salaNameUnir, int salaIDUnir)
        {
            // Uniendo usuario y sala
            try
            {
                // Obteniendo usuario Logiado
                var usLogged = LocalStorage.Get<Usuario>("usuario");
           
                Enrolado enrolado = new Enrolado()
                {
                    Status = 1,
                    SalaId = salaIDUnir,
                    UsuarioId = usLogged.UID
                };
                var res = await enroladosProvider.AddAsync(enrolado);
                LocalStorage.Store("salaID", salaIDUnir);
                LocalStorage.Store("enroladoID", res);
                LocalStorage.Store("salaNameUnir", salaNameUnir);
                LocalStorage.Persist();
                ViewData["salaNameUnir"] = salaNameUnir;


            }
            catch (System.Exception)
            {
                Console.WriteLine("Usuario no logeado");
            }

        }
      
        private async Task AddSala(string nameSala)
        {
            Sala sala = new Sala()
            {
                Name = nameSala,
                Status = 1
            };
            var isSuccess = await salasProvider.AddAsync(sala);
        }
 
        // Salir de la sala
        private async Task Salir()
        {
            try
            {
                var usserLogged = LocalStorage.Get<Usuario>("usuario");
                var enroladoID = LocalStorage.Get("enroladoID");
                var salaID = LocalStorage.Get("salaID");
                
                Enrolado enrolado = new Enrolado()
                {
                    Id = int.Parse($"{enroladoID}"), 
                    Status = 0,
                    SalaId = int.Parse($"{salaID}"),
                    UsuarioId = usserLogged.UID
                };
                var res = await enroladosProvider.UpdateAsync($"{enroladoID}",enrolado);

                // Elimina la unión de la sala en localstorage
                LocalStorage.Destroy();
                LocalStorage.Clear();

                // Permite mantener el usuario logiado
                LocalStorage.Store("usuario", usserLogged);
                LocalStorage.Persist();               

            }
            catch (Exception)
            {
                Console.WriteLine("Error al salir de la sala");
            }
        }

        private async Task SendingMessage(string messageInput)
        {
            try
            {
                // Obteniendo usuario de LocalStorage
                var us = LocalStorage.Get<Usuario>("usuario");
                var enroladoID = LocalStorage.Get("enroladoID");
                var salaID = LocalStorage.Get("salaID");
             
                DateTime dateTime = DateTime.Now;
                ChatTracebility message = new ChatTracebility()
                {
                    AtDate = dateTime,
                    Message = messageInput,
                    UserName = us.Name, 
                    UserID = us.UID, 
                    enroladoId = int.Parse($"{enroladoID}")
                };

                // Enviando mensaje por WS
                await chatTracebilityProvider.AddAsync(client, message);
               
                // Escuchando mensajes por WS
                Receiving();
            }
            catch (System.Exception)
            {
                Console.WriteLine("El Usuario no se ha unido aún");
            }
        }

        private async Task GettingMessage()
        {
            try
            {
                // Obteniendo usuario de LocalStorage
                var us = LocalStorage.Get<Usuario>("usuario");
                var salaID = LocalStorage.Get("salaID");

                // Obtener Mensaje en la sala
                messagesChat = await chatTracebilityProvider.GetAllByUserSalaAsync(us.UID, int.Parse($"{salaID}"));
                ViewData["messagesChat"] = messagesChat; 
            }
            catch (Exception)
            {
                Console.WriteLine("Usuario no logeado");
            }
        }

        private async Task GettingSalas()
        {
           // Obteniendo salas disponibles                   
            salasDisponble = await salasProvider.GetAllByStatusAsync(1); //1: Activas  0: Inactiva
            ViewData["salasDisponble"] = salasDisponble;
        }
         
        public  async Task<IActionResult> Chat(
            int SalaSalir,
            int salaIDUnir,
            int logout,
            string salaNameUnir, 
            string nickname, 
            string messageInput,
            string nameSala)
        {
            // Cerrar la sesión
            if (logout > 0)
            {
                await LogOut();
            }

            // Salir de la sala
            if (SalaSalir > 0)
            {
               await Salir();
            }

            if (!String.IsNullOrEmpty(nickname))
            {
               await Login(nickname);                
            }

            if (!String.IsNullOrEmpty(nameSala))
            {
               await AddSala(nameSala);
            }

            if (!String.IsNullOrEmpty(messageInput))
            {
               await SendingMessage(messageInput);
            }
           
            if (!String.IsNullOrEmpty(salaNameUnir) && salaIDUnir > 0)
            {
              await Join(salaNameUnir, salaIDUnir);
            }
                   
     
            // Obteniendo las salas
            await GettingSalas();
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
