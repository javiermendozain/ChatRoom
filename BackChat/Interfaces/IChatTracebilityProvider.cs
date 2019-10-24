using System.Collections.Generic;
using System.Threading.Tasks;
using BackChat.Models;

namespace BackChat.Interfaces
{

    public interface IChatTracebilityProvider
    {
        Task<ICollection<ChatTracebility>> GetAllBySalaAsync(int? IdSala);

        Task<ICollection<ChatTracebility>> GetAllByUserSalaAsync(string IdUsuarios, int? IdSala);

        Task<bool> AddAsync(ChatTracebility chatTracebility);

       
    }
   
}
