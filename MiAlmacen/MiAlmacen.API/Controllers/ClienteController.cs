using MiAlmacen.Data.Repositories;
using MiAlmacen.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiAlmacen.API.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteRepository _repository;
        public ClienteController(ClienteRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("{filtro}")]
        public async Task<IEnumerable<ClienteModel>> Get(string filtro)
        {
            var user = _repository.Get(filtro);
            List<ClienteModel> clientes = new();
            foreach (var item in user)
            {
                ClienteModel model = new();
                model.Id = item.Id;
                model.Nombre = item.Nombre;
                model.Direccion = item.Direccion;
                model.Telefono = item.Telefono;
                clientes.Add(model);
            }
            return clientes;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cliente = _repository.GetOne(id);
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ClienteModel model)
        {
            return Ok(_repository.Post(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ClienteModel model)
        {
            return Ok(_repository.Put(id, model));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(_repository.Delete(id));
        }
    }
}
