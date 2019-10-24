using System.Collections.Generic;
using System.Threading.Tasks;
using BackChat.Models;

namespace BackChat.Interfaces
{
    public interface IUsuariosProvider
    {
        Task<ICollection<Usuario>> GetAllByStatusAsync(int? status);

        Task<bool> AddAsync(Usuario usuario);

        Task<bool> UpdateAsync(string Id, Usuario usuario);

    }
}
