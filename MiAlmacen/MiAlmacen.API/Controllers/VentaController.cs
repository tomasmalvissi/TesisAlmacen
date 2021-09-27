using MiAlmacen.Data.Entities;
using MiAlmacen.Data.Repositories;
using MiAlmacen.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiAlmacen.API.Controllers
{
    [Route("api/ventas")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly VentaRepository _repository;

        public VentaController(VentaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ventas = _repository.GetAll();
            return Ok(ventas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var venta = _repository.GetOne(id);
            return Ok(venta);
        }


        [HttpPut("{pago}")]
        public async Task<IActionResult> PutSaldo(Ventas venta, float pago)
        {
            var nuevoSaldo = _repository.PutSaldo(venta, pago);
            return Ok(nuevoSaldo);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Ventas venta)
        {
            return Ok(_repository.Post(venta));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(_repository.Delete(id));
        }
    }
}