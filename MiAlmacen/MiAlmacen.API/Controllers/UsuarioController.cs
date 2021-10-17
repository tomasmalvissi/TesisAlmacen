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
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = _repository.GetAll();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = _repository.GetOne(id);
            return Ok(user);
        }

        [HttpGet]
        [Route("api/usuarios/{username}")]
        public async Task<IActionResult> GetUsers(string username)
        {
            var result = _repository.GetUser(username);
            return Ok(result);
        }

        [HttpGet("{user}/{pass}")]
        public async Task<IActionResult> Login(string user, string pass)
        {
            var result = _repository.Login(user,pass);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(UsuarioModel model)
        {
            return Ok(_repository.Post(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] UsuarioModel model)
        {
            return Ok(_repository.Put(id, model));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(_repository.Delete(id));
        }
    }
}
