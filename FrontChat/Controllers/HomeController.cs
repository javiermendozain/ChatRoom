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
    public class HomeController : Controller
    {
        private readonly IUsuariosProvider usuariosProvider;

        public HomeController(IUsuariosProvider usuariosProvider)
        {
            this.usuariosProvider = usuariosProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
