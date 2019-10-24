using System.Collections.Generic;
using System.Threading.Tasks;
using FrontChat.Models;
using System.Net.WebSockets;


namespace FrontChat.Interfaces
{

    public interface IChatTracebilityProvider
    {
        Task<ICollection<ChatTracebility>> GetAllBySalaAsync(int IdSala);

        Task<ICollection<ChatTracebility>> GetAllByUserSalaAsync(string IdUsuarios, int IdSala);

        Task<bool> AddAsync(ClientWebSocket clientWS, ChatTracebility chatTracebility);

    }
   
}
