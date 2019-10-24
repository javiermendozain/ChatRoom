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
    public class EnroladosController : ControllerBase
    {
        private readonly IEnroladosProvider enroladosProvider;

        public EnroladosController(IEnroladosProvider enroladosProvider)
        {
            this.enroladosProvider = enroladosProvider;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(Enrolado enrolado)
        {
            if (enrolado == null )
            {
                return BadRequest();
            }

            var result = await enroladosProvider.AddAsync(enrolado);;
            if (result > 0)
            {
                return Ok(result);
            }

            return NotFound();

        }

        [HttpPut("{id}")] 
        public async Task<IActionResult>  UpdateAsync(string id, Enrolado enrolado)
        {
              
            if (enrolado == null && String.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var result = await enroladosProvider.UpdateAsync(id, enrolado);;
            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}