using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackChat.DataContext;
using BackChat.Interfaces;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

using BackChat.Models;


namespace BackChat.Providers
{
    public class SalasProvider : ISalasProvider
    {
        public Task<bool> AddAsync(Sala sala)
        {

            using (var context = new Context())
            {
                // Filter user by Name and it's status active
                var nSala = context.Sala
                .Where(u => u.Name.Contains(sala.Name))
                .Where(u => u.Status == 1).Count();

                // Validate than the new Sala don't exist
                if (nSala <= 0)
                {
                    try
                    {
                        // Adds a Sala
                        context.Sala.Add(sala);

                        // Saves changes
                        context.SaveChanges();
                    }
                    catch (Exception )
                    {
                        return Task.FromResult((false));
                    }
                }
                else
                {
                    return Task.FromResult((false));
                }
            }

            return Task.FromResult((true));   
        }

        public Task<ICollection<Sala>> GetAllAsync()
        {
            using (var context = new Context())
            {
                var Salas = context.Sala;
                var count = Salas.Count();
                
                // Validate than exist Salas
                if (count > 0)
                {
                    return Task.FromResult((ICollection<Sala>)Salas.ToList());
                }
            }

            return Task.FromResult((ICollection<Sala>)(new List<Sala>()));    
       
        }

        public Task<ICollection<Sala>> GetAllByStatusAsync(int? status)
        {

            using (var context = new Context())
            {
                // Filter Salas by status 
                var Salas = context.Sala
                    .Where(s => s.Status == status);

                var count = Salas.Count();
                // Validate than exist salas
                if (count > 0)
                {
                    return Task.FromResult((ICollection<Sala>)Salas.ToList());
                }
            }

            return Task.FromResult((ICollection<Sala>)(new List<Sala>()));   
        }

        public Task<ICollection<Sala>> GetAllByUserAsync(string id)
        {
            List<Sala> response = new List<Sala>();

            using (var context = new Context())
            {
                // Filter user by Name and it's status active
                var query = context.Enrolado
                    .Include(en => en.Usuario)
                    .Include(sa => sa.Sala)
                    .Where(enrolado => enrolado.Usuario.UID == id );
           
                foreach (var items in query)
                {
                    response.Add(new Sala(){
                       Name = items.Sala.Name,
                       Id = items.Sala.Id,
                       Status = items.Sala.Status
                    });
                }
              
                return Task.FromResult((ICollection<Sala>)response);
            }

        }

        public Task<bool> UpdateAsync(string Id, Sala sala)
        {

            using (var context = new Context())
            {
                try
                {
                      // Attach method and then mark the desired field as modified manually.
                    context.Attach(sala);
                    context.Entry(sala).Property(u => u.Name).IsModified = true;
                    context.SaveChanges();
                    return Task.FromResult(true);
                }
                catch (Exception)
                {
                    
                    return Task.FromResult(false);
                }
                
            }

        }
    }
}
