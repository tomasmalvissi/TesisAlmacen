using MiAlmacen.Data.Repositories;
using MiAlmacen.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiAlmacen.API.Controllers
{
    [Route("api/proveedores")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly ProveedorRepository _repository;
        public ProveedorController(ProveedorRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var proveedores = _repository.GetAll();
            return Ok(proveedores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var proveedor = _repository.GetOne(id);
            return Ok(proveedor);
        }

        [HttpGet("existe/{cuit}")]
        public async Task<IActionResult> ExisteProveedor(long cuit)
        {
            var existe = _repository.ExisteProveedor(cuit);
            return Ok(existe);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProveedorModel model)
        {
            return Ok(_repository.Post(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ProveedorModel model)
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