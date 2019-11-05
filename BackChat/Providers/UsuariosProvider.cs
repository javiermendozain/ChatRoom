using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackChat.DataContext;
using BackChat.Interfaces;
using BackChat.Models;


namespace BackChat.Providers
{
    public class UsuariosProvider : IUsuariosProvider
    {
        private readonly Context dbContext;

        public UsuariosProvider(Context dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<bool> AddAsync(Usuario usuario)
        {
           
            // Filter user by Name and it's status active
            var nUsers = dbContext.Usuario
            .Where(u => u.Name.Contains(usuario.Name))
            .Where(u => u.Status == 1).Count();

            // Validate than the new user don't exist
            if (nUsers <= 0)
            {
                try
                {
                // Adds a User
                dbContext.Usuario.Add(usuario);

                // Saves changes
                dbContext.SaveChanges();
                    return Task.FromResult(true);           

                }
                catch (Exception )
                {
                    return Task.FromResult(false);
                }
            }
            else
            {
                return Task.FromResult(false);
            }
                  

        }

        public Task<ICollection<Usuario>> GetAllByStatusAsync(int? status)
        {

            // Filter user by Name and it's status active
            var Users = dbContext.Usuario
                .Where(u => u.Status == status);

            var count = Users.Count();
            // Validate than the new user don't exist
            if (count > 0)
            {
                return Task.FromResult((ICollection<Usuario>)Users.ToList());
            }

            return Task.FromResult((ICollection<Usuario>)(new List<Usuario>()));    
        }
       
        public Task<bool> UpdateAsync(string Id, Usuario usuario)
        {
         
            try
            {
                // Attach method and then mark the desired field as modified manually.
                dbContext.Attach(usuario);
                dbContext.Entry(usuario).Property(u => u.Status).IsModified = true;
                dbContext.SaveChanges();
                return Task.FromResult(true);
            }
            catch (System.Exception)
            {
                return Task.FromResult(false);
            }
               
              
        }
    }
}
