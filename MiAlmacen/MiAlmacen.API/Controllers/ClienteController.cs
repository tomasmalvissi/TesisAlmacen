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

        [HttpPost]
        public async Task<IActionResult> Post(ClienteModel model)
        {
            return Ok(_repository.Post(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] ClienteModel model)
        {
            return Ok(_repository.Put(model));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(_repository.Delete(id));
        }
    }
}
