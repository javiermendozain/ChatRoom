using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackChat.DataContext;
using BackChat.Interfaces;
using BackChat.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace BackChat.Providers
{
    public class UsuariosProvider : IUsuariosProvider
    {
        public Task<bool> AddAsync(Usuario usuario)
        {
            using (var context = new Context())
            {
                // Filter user by Name and it's status active
                var nUsers = context.Usuario
                .Where(u => u.Name.Contains(usuario.Name))
                .Where(u => u.Status == 1).Count();

                // Validate than the new user don't exist
                if (nUsers <= 0)
                {
                    try
                    {
                        // Adds a User
                        context.Usuario.Add(usuario);

                        // Saves changes
                        context.SaveChanges();
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

        }

        public Task<ICollection<Usuario>> GetAllByStatusAsync(int? status)
        {


            Console.WriteLine("--------------- Prividers---------------------------");


            using (var context = new Context())
            {
                // Filter user by Name and it's status active
                var Users = context.Usuario
                    .Where(u => u.Status == status);

                var count = Users.Count();
                // Validate than the new user don't exist
                if (count > 0)
                {
                    return Task.FromResult((ICollection<Usuario>)Users.ToList());
                }
            }

            return Task.FromResult((ICollection<Usuario>)(new List<Usuario>()));    
        }
       
        public Task<bool> UpdateAsync(string Id, Usuario usuario)
        {
            using (var context = new Context())
            {
                try
                {
                    // Attach method and then mark the desired field as modified manually.
                    context.Attach(usuario);
                    context.Entry(usuario).Property(u => u.Status).IsModified = true;
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
