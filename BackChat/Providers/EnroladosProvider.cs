using System;
using System.Linq;
using System.Threading.Tasks;
using BackChat.DataContext;
using BackChat.Interfaces;
using BackChat.Models;


namespace BackChat.Providers
{
    public class EnroladosProvider : IEnroladosProvider
    {
        private readonly Context dbcontext;

        public EnroladosProvider(Context dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public Task<int> AddAsync(Enrolado enrolado)
        {

            try
            {
                // Adds a enrolado
                dbcontext.Enrolado.Add(enrolado);
                // Saves changes
                dbcontext.SaveChanges();

                return Task.FromResult((enrolado.Id));  
            }
            catch (Exception)
            {
                    
                // Si ya exitio una vez, actualiza el estado a unido nuevamente
                enrolado.Status = 1;
                dbcontext.Attach(enrolado);
                dbcontext.Entry(enrolado).Property(u => u.Status).IsModified = true;

                // Saves changes
                dbcontext.SaveChanges();

                return Task.FromResult((enrolado.Id));
            }
            
        }

        public Task<bool> UpdateAsync(string Id, Enrolado enrolado)
        {

            try
            {
                // Attach method and then mark the desired field as modified manually.
                dbcontext.Attach(enrolado);
                dbcontext.Entry(enrolado).Property(en => en.Status).IsModified = true;
                dbcontext.SaveChanges();

                    
                // Obteniene lista de enrolados habilidados
                var queryEnrolado = dbcontext.Enrolado
                    .Where(en => en.SalaId == enrolado.SalaId )
                    .Where(en => en.Status == 1);
            
                // Si la lista de enrolados es igual a 0, Deshabilita la sala
                if (queryEnrolado.Count() == 0)
                {
                    var querySala = dbcontext.Sala
                        .Where(sala => sala.Id == enrolado.SalaId )
                        .FirstOrDefault();

                    // Cambia el estado de la sala, inhabilitarla
                    querySala.Status = 0;
                    dbcontext.Attach(querySala);
                    dbcontext.Entry(querySala).Property(sa => sa.Status).IsModified = true;
                }

                dbcontext.SaveChanges();

                return Task.FromResult(true);
            }
            catch (Exception)
            {
                    
                return Task.FromResult(false);
            }
                
        }
    }
}
