using System.Collections.Generic;
using System.Threading.Tasks;
using FrontChat.Models;

namespace FrontChat.Interfaces
{

    public interface IEnroladosProvider
    {
        Task<int> AddAsync(Enrolado enrolado);

        Task<bool> UpdateAsync(string Id, Enrolado enrolado);

    }

}
