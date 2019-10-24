using Microsoft.EntityFrameworkCore;
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
    public class ChatTracebilityProvider : IChatTracebilityProvider
    {
        public Task<bool> AddAsync(ChatTracebility chatTracebility)
        {

            using (var context = new Context())
            {
                try
                {
                    // Adds a chatTracebility
                    context.ChatTracebility.Add(chatTracebility);

                    // Saves changes
                    context.SaveChanges();
                    return Task.FromResult((true));           

                }
                catch (Exception )
                {
                    return Task.FromResult((false));
                }
               
            }
        }

        public Task<ICollection<ChatTracebility>> GetAllBySalaAsync(int? IdSala)
        {
            List<ChatTracebility> response = new List<ChatTracebility>();

            using (var context = new Context())
            {
                // Filter user by Name and it's status active
                var query = context.ChatTracebility
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

        }

        public Task<ICollection<ChatTracebility>> GetAllByUserSalaAsync(string IdUsuarios, int? IdSala)
        {
            List<ChatTracebility> response = new List<ChatTracebility>();

            using (var context = new Context())
            {
                // Filter user by Name and it's status active
                var query = context.ChatTracebility
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
}
