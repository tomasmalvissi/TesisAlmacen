using MiAlmacen.Data.Repositories;
using MiAlmacen.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiAlmacen.API.Controllers
{
    [Route("api/movimientos")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly MovimientoRepository _repository;
        public MovimientosController(MovimientoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movimientos = _repository.GetAll();
            return Ok(movimientos);
        }

        //[HttpPost]
        //public async Task<IActionResult> Post(MovimientosCajaModel model)
        //{
        //    return Ok(_repository.Post(model));
        //}
    }
}
