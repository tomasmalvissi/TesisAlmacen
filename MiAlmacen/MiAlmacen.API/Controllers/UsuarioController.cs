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
        public async Task<IEnumerable<UsuarioModel>> Get()
        {
            var user = _repository.GetAll();
            List<UsuarioModel> usuarios = new();
            foreach (var item in user)
            {
                UsuarioModel model = new();
                model.Id = item.Id;
                model.Nombre = item.Nombre;
                model.Usuario = item.Usuario;
                model.Email = item.Email;
                model.Contraseña = item.Contraseña;
                usuarios.Add(model);
            }
            return usuarios;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = _repository.GetOne(id);
            return Ok(user);
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
