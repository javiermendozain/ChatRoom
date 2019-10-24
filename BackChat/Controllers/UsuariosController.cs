using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackChat.Interfaces;
using BackChat.Models;
using BackChat.Providers;

namespace BackChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosProvider usuariosProvider;

        private MessageHandlerProvider MessageHandlerProvider { get; set; }
        
         public UsuariosController(IUsuariosProvider usuariosProvider, MessageHandlerProvider MessageHandlerProvider)
        {
            this.usuariosProvider = usuariosProvider;
            this.MessageHandlerProvider = MessageHandlerProvider;    
        }


        [HttpGet("{status}")]
        public async Task<IActionResult> GetAllByStatusAsync(int? status)
        {

            if (status == null)
            {
                return BadRequest();
            }
            var result = await usuariosProvider.GetAllByStatusAsync(status);

            return Ok(result);
        }

  
        [HttpPost]
        public async Task<IActionResult> AddAsync(Usuario usuario)
        {

            if (usuario == null)
            {
                return BadRequest();
            }

            var result = await usuariosProvider.AddAsync(usuario);
            if (result)
            {
                return Ok(result);
            }
          
            return NotFound();

        }

        [HttpPut("{id}")] 
        public async Task<IActionResult> UpdateAsync(string id, Usuario usuario)
        {
           
            if (usuario == null && String.IsNullOrEmpty(id) )
            {
                return BadRequest();
            }

            var result = await usuariosProvider.UpdateAsync(id, usuario);
            if (result)
            {
                return Ok(result);
            }

            return NotFound();

        }


    }
}