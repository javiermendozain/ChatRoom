using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using BackChat.DataContext;
using BackChat.Interfaces;
using BackChat.Models;
using Newtonsoft.Json;


namespace BackChat.Providers
{
    public class EnroladosProvider : IEnroladosProvider
    {
        public Task<int> AddAsync(Enrolado enrolado)
        {

            using (var context = new Context())
            {
                try
                {
                    // Adds a enrolado
                    context.Enrolado.Add(enrolado);
                    // Saves changes
                    context.SaveChanges();

                    return Task.FromResult((enrolado.Id));  
                }
                catch (Exception)
                {
                    
                    // Si ya exitio una vez, actualiza el estado a unido nuevamente
                    enrolado.Status = 1;
                    context.Attach(enrolado);
                    context.Entry(enrolado).Property(u => u.Status).IsModified = true;

                    // Saves changes
                    context.SaveChanges();

                    return Task.FromResult((enrolado.Id));
                }
            }
        }

        public Task<bool> UpdateAsync(string Id, Enrolado enrolado)
        {

            using (var context = new Context())
            {
                try
                {
                    // Attach method and then mark the desired field as modified manually.
                    context.Attach(enrolado);
                    context.Entry(enrolado).Property(en => en.Status).IsModified = true;
                    context.SaveChanges();

                    
                    // Obteniene lista de enrolados habilidados
                    var queryEnrolado = context.Enrolado
                        .Where(en => en.SalaId == enrolado.SalaId )
                        .Where(en => en.Status == 1);
            
                    // Si la lista de enrolados es igual a 0, Deshabilita la sala
                    if (queryEnrolado.Count() == 0)
                    {
                        var querySala = context.Sala
                            .Where(sala => sala.Id == enrolado.SalaId )
                            .FirstOrDefault();

                        // Cambia el estado de la sala, inhabilitarla
                        querySala.Status = 0;
                        context.Attach(querySala);
                        context.Entry(querySala).Property(sa => sa.Status).IsModified = true;
                    }

                    context.SaveChanges();

                    return Task.FromResult(true);
                }
                catch (System.Exception)
                {
                    
                    return Task.FromResult(false);
                }
                
            }
        }
    }
}
