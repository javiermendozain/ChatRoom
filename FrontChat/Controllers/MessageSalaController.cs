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
    public class MessageSalaController : Controller
    {
      
        private readonly IChatTracebilityProvider chatTracebilityProvider;

        ICollection<ChatTracebility> chatTracebility { get; set; }

        public MessageSalaController(
            IChatTracebilityProvider chatTracebilityProvider
            )
        {
     
            this.chatTracebilityProvider = chatTracebilityProvider;
        }


        public async Task<IActionResult> MessageSala(int SalaID)
        {
            
            if (SalaID > 0 )
            {
                chatTracebility = await chatTracebilityProvider.GetAllBySalaAsync(SalaID);
                ViewData["chatTracebility"] = chatTracebility;
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
