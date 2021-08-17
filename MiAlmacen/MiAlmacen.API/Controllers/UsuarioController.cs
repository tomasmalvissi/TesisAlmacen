using MiAlmacen.Data.Repositories;
using MiAlmacen.Model;
using MiAlmacen.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiAlmacen.API.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepository _repository;
        public UsuarioController(UsuarioRepository repository)
        {
            repository = _repository;
        }

        [HttpGet]
        public Task<IEnumerable<UsuarioModel>> Get()
        {
            _repository.Get();
            throw new InvalidOperationException();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(UsuarioModel model)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] UsuarioModel model)
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok();
        }
    }
}
