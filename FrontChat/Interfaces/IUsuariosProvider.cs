using System.Collections.Generic;
using System.Threading.Tasks;
using FrontChat.Models;

namespace FrontChat.Interfaces
{
    public interface IUsuariosProvider
    {
        Task<ICollection<Usuario>> GetAllByStatusAsync(int status);

        Task<bool> AddAsync(Usuario usuario);

        Task<bool> UpdateAsync(string Id, Usuario usuario);

    }
}
