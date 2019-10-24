using System.Collections.Generic;
using System.Threading.Tasks;
using FrontChat.Models;

namespace FrontChat.Interfaces
{

    public interface ISalasProvider
    {
        Task<ICollection<Sala>> GetAllAsync();

        Task<ICollection<Sala>> GetAllByUserAsync(string id);

        Task<ICollection<Sala>> GetAllByStatusAsync(int status);

        Task<bool> AddAsync(Sala sala);

        Task<bool> UpdateAsync(string Id, Sala sala);

    }
   
}
