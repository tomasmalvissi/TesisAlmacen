using MiAlmacen.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiAlmacen.API.Controllers
{
    [Route("api/estadisticas")]
    [ApiController]
    public class EstadisticasController : ControllerBase
    {
        private readonly EstadisticaRepository _repository;

        public EstadisticasController(EstadisticaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("ventas")]
        public async Task<IActionResult> GetVentasXPeriodo()
        {
            var ventas = _repository.GetVentasPeriodo();
            return Ok(ventas);
        }

        [HttpGet("compras")]
        public async Task<IActionResult> GetComprasXPeriodo()
        {
            var compras = _repository.GetComprasPeriodo();
            return Ok(compras);
        }

        [HttpGet("productos")]
        public async Task<IActionResult> GetTopProductos()
        {
            var ventas = _repository.GetTopProductos();
            return Ok(ventas);
        }

        [HttpGet("clientes")]
        public async Task<IActionResult> GetTopClientes()
        {
            var ventas = _repository.GetTopClientes();
            return Ok(ventas);
        }

        [HttpGet("ventasxdia")]
        public async Task<IActionResult> GetVentasXDias()
        {
            var ventas = _repository.GetVentasXDia();
            return Ok(ventas);
        }
    }
}
