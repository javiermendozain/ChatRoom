using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FrontChat.Models;
using FrontChat.Interfaces;
using Blazored.Toast.Services;


namespace FrontChat.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsuariosProvider usuariosProvider;
        private readonly IToastService toastService;

     
        public HomeController(IUsuariosProvider usuariosProvider, IToastService toastService)
        {
            this.usuariosProvider = usuariosProvider;
            this.toastService = toastService;
        }

        public IActionResult Index()
        {
            toastService.ShowSuccess("I'm a SUCCESS message with a custom heading", "Congratulations!");
           // toastService.ShowInfo("I'm an INFO message");
           // toastService.ShowSuccess("I'm a SUCCESS message with a custom heading", "Congratulations!");
           // toastService.ShowWarning("I'm a WARNING message");
           // toastService.ShowError("I'm an ERROR message");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
