using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using BackChat.DataContext;
using BackChat.Interfaces;
using BackChat.Models;


namespace BackChat.Providers
{
    public class ChatTracebilityProvider : IChatTracebilityProvider
    {
        private readonly Context dbcontext;

        public ChatTracebilityProvider( Context dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public Task<bool> AddAsync(ChatTracebility chatTracebility)
        {

            try
            {
                // Adds a chatTracebility
                dbcontext.ChatTracebility.Add(chatTracebility);

                // Saves changes
                dbcontext.SaveChanges();
                return Task.FromResult((true));           

            }
            catch (Exception )
            {
                return Task.FromResult((false));
            }

        }

        public Task<ICollection<ChatTracebility>> GetAllBySalaAsync(int? IdSala)
        {
            List<ChatTracebility> response = new List<ChatTracebility>();

            // Filter user by Name and it's status active
            var query = dbcontext.ChatTracebility
                .Include(ch => ch.enrolado)
                    .ThenInclude(en => en.Sala)
                        .Where(ch => ch.enrolado.Sala.Id == IdSala );


            foreach (var items in query)
            {
                response.Add(new ChatTracebility(){
                    Message = items.Message,
                    AtDate = items.AtDate,
                    Id = items.Id,
                    UserName = items.UserName,
                    UserID = items.UserID,
                });
            }
              
            return Task.FromResult((ICollection<ChatTracebility>)response);
            

        }

        public Task<ICollection<ChatTracebility>> GetAllByUserSalaAsync(string IdUsuarios, int? IdSala)
        {
            List<ChatTracebility> response = new List<ChatTracebility>();
           
            // Filter user by Name and it's status active
            var query = dbcontext.ChatTracebility
                .Include(ch => ch.enrolado)
                    .ThenInclude(en => en.Sala)
                        .Where(ch => ch.enrolado.Sala.Id == IdSala )
                        .Where(ch => ch.enrolado.Usuario.UID == IdUsuarios );

            foreach (var items in query)
            {
                response.Add(new ChatTracebility(){
                    Message = items.Message,
                    AtDate = items.AtDate,
                    Id = items.Id,
                    UserName = items.UserName,
                    UserID = items.UserID
                });
            }
              
            return Task.FromResult((ICollection<ChatTracebility>)response);
        }
    }
}
