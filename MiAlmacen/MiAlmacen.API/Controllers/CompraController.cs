using MiAlmacen.Data.Repositories;
using MiAlmacen.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MiAlmacen.API.Controllers
{
    [Route("api/compras")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly CompraRepository _repository;

        public CompraController(CompraRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var compras = _repository.GetAll();
            return Ok(compras);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var compra = _repository.GetOne(id);
            return Ok(compra);
        }

        [HttpGet("existerecibo/{nroRecibo}")]
        public async Task<IActionResult> Get(long nroRecibo)
        {
            var existe = _repository.ExisteRecibo(nroRecibo);
            return Ok(existe);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CompraModel compra)
        {
            return Ok(_repository.Post(compra));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(_repository.Delete(id));
        }
    }
}