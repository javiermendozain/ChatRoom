using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackChat.Interfaces;
using BackChat.Models;

namespace BackChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatTracebilityController : ControllerBase
    {
        private readonly IChatTracebilityProvider chatTracebilityProvider;

        public ChatTracebilityController(IChatTracebilityProvider chatTracebilityProvider)
        {
            this.chatTracebilityProvider = chatTracebilityProvider;
        }

        [HttpGet("sala/{idsala}")]
        public async Task<IActionResult>  GetAllBySalaAsync(int? idsala)
        {
            if (idsala == null)
            {
                return BadRequest();                
            }

            var results = await chatTracebilityProvider.GetAllBySalaAsync(idsala);

            if (results != null)
            {
                return Ok(results);
            }

            return NotFound();
        }

        [HttpGet("{idusuarios}/{idsala}")]
        public async Task<IActionResult>  GetAllByUserSalaAsync(string idusuarios, int? idsala)
        {
            if ( String.IsNullOrEmpty(idusuarios) &&  idsala == null)
            {
                return BadRequest();                
            }

            var results = await chatTracebilityProvider.GetAllByUserSalaAsync(idusuarios, idsala);

            if (results != null)
            {
                return Ok(results);
            }

            return NotFound();
        }
        
        [HttpPost]
        public async Task<IActionResult>  AddAsync(ChatTracebility chatTracebility)
        {
            if (chatTracebility == null)
            {
                return BadRequest();
            }

            var result = await chatTracebilityProvider.AddAsync(chatTracebility);

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}