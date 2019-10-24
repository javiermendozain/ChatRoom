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
    public class AdminController : Controller
    {
        private readonly IUsuariosProvider usuariosProvider;

        private readonly ISalasProvider salasProvider;
       
        ICollection<Usuario> usuariosInActivos { get; set; }

        ICollection<Usuario> usuariosActivos { get; set; }

        ICollection<Sala> salas { get; set; }

        ICollection<Sala> salasByUser { get; set; }

        public AdminController( IUsuariosProvider usuariosProvider, ISalasProvider salasProvider )
        {
            this.usuariosProvider = usuariosProvider;
            this.salasProvider  = salasProvider;
        }

        private async Task GettingSalaByUser(string viewSalaUserID)
        {
            salasByUser = await salasProvider.GetAllByUserAsync(viewSalaUserID);
            ViewData["salasByUser"] = salasByUser;
        }

         private async Task ExpulsarUsuario(string expulsar)
        {
            var user = usuariosActivos.Where(us=>us.UID == expulsar).FirstOrDefault();
            if(user != null)
            {
                user.Status = 0;
                var isSuccess =  await usuariosProvider.UpdateAsync(expulsar,user);
                if(isSuccess){
                    usuariosActivos =  await usuariosProvider.GetAllByStatusAsync(1); // Activos
                    usuariosInActivos = await usuariosProvider.GetAllByStatusAsync(0); // Inactivo
        
                }
            }
        }

        public async Task<IActionResult> Admin(string expulsar, string viewSalaUserID )
        {
            /* Obteniendo usuarios activos e inactivos */
            usuariosActivos =  await usuariosProvider.GetAllByStatusAsync(1); // Activos
            usuariosInActivos = await usuariosProvider.GetAllByStatusAsync(0); // Inactivo

            /* Obteniendo las salas que han existido  */
            salas = await salasProvider.GetAllByStatusAsync(0); // Desactivadas
         
            if (!String.IsNullOrEmpty(viewSalaUserID))
            {
                await GettingSalaByUser(viewSalaUserID);
            }

            // Expulsar usuario activo
            if (!String.IsNullOrEmpty(expulsar))
            {
               await ExpulsarUsuario(expulsar);                
            }

            ViewData["usuariosActivos"] = usuariosActivos;
            ViewData["usuariosInActivos"] = usuariosInActivos;
            ViewData["salas"] = salas;
            ViewData["viewSalaUserID"] = viewSalaUserID;
   
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
