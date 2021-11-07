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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = _repository.GetAll();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cliente = _repository.GetOne(id);
            return Ok(cliente);
        }

        [HttpGet("existe/{dni}")]
        public async Task<IActionResult> ExisteCliente(long dni)
        {
            var existe = _repository.ExisteCliente(dni);
            return Ok(existe);
        }

        [HttpGet("deuda/{id}")]
        public async Task<IActionResult> GetDeuda(int id)
        {
            var deuda = _repository.GetDeuda(id);
            return Ok(deuda);
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
