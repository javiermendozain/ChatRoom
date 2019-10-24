using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FrontChat.Models;
using FrontChat.Interfaces;

namespace FrontChat.Controllers
{
    public class MessageSalaUsuarioController : Controller
    {
      
        private readonly IChatTracebilityProvider chatTracebilityProvider;
        
        ICollection<ChatTracebility> chatTracebility { get; set; }

        public MessageSalaUsuarioController( IChatTracebilityProvider chatTracebilityProvider )
        {
            this.chatTracebilityProvider = chatTracebilityProvider;
        }
        
        public async Task<IActionResult> MessageSalaUsuario(string UserID, int SalaID, string UserName)
        {
            if (UserID != null && SalaID > 0 )
            {
                chatTracebility = await chatTracebilityProvider.GetAllByUserSalaAsync(UserID, SalaID);
                ViewData["chatTracebility"] = chatTracebility;
                ViewData["UserName"] = UserName;
                ViewData["UserID"] = UserID;
            }
                    
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
