using System.Collections.Generic;
using System.Threading.Tasks;
using BackChat.Models;

namespace BackChat.Interfaces
{

    public interface IEnroladosProvider
    {
       
        Task<int> AddAsync(Enrolado enrolado);

        Task<bool> UpdateAsync(string Id, Enrolado enrolado);

    }

}
