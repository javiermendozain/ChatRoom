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
    public class SalasController : ControllerBase
    {
        private readonly ISalasProvider salasProvider;

        public SalasController(ISalasProvider salasProvider)
        {
            this.salasProvider = salasProvider;
        }
     
        [HttpGet]
        public async Task<IActionResult>  GetAllAsync()
        {
            var result = await salasProvider.GetAllAsync();;
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();            
        }

        [HttpGet("usuario/{id}")]
        public async Task<IActionResult> GetAllByUserAsync(string id)
        {
            if (String.IsNullOrEmpty(id) )
            {
                return BadRequest();                
            }

            var results = await salasProvider.GetAllByUserAsync(id);

            if (results != null)
            {
                return Ok(results);
            }

            return NotFound();
        }

        [HttpGet("state/{status}")]
        public async Task<IActionResult> GetAllByStatusAsync(int? status)
        {
            if (status == null )
            {
                return BadRequest();                
            }

            var results = await salasProvider.GetAllByStatusAsync(status);

            if (results != null)
            {
                return Ok(results);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult>  AddAsync(Sala sala){

            if (sala == null)
            {
                return BadRequest();
            }

            var result = await salasProvider.AddAsync(sala);;
            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpPut("{id}")] 
        public async Task<IActionResult>  UpdateAsync(string id, Sala sala)
        {
            if (sala == null && String.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var result = await salasProvider.UpdateAsync(id, sala);;
            if (result)
            {
                return Ok(sala.Id);
            }

            return NotFound();
        }



    }
}