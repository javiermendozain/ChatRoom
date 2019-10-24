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
    public class SalaUsuarioController : Controller
    {
        private readonly IUsuariosProvider usuariosProvider;

        private readonly ISalasProvider salasProvider;

        ICollection<Sala> salasByUser { get; set; }

        ICollection<Usuario> usuariosInActivos { get; set; }

        ICollection<Usuario> usuariosActivos { get; set; }

        public SalaUsuarioController( IUsuariosProvider usuariosProvider, ISalasProvider salasProvider )
        {
            this.usuariosProvider = usuariosProvider;
            this.salasProvider = salasProvider;
        }
        
        public async Task<IActionResult> SalaUsuario(string viewSalaUserID, string viewSalaUserName)
        {
             if (viewSalaUserID != null)
            {
                salasByUser = await salasProvider.GetAllByUserAsync(viewSalaUserID);
                ViewData["salasByUser"] = salasByUser;
                ViewData["viewSalaUserName"] = viewSalaUserName;
                ViewData["viewSalaUserID"] = viewSalaUserID;
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
