using MiAlmacen.Data.Repositories;
using MiAlmacen.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiAlmacen.API.Controllers
{
    [Route("api/caja")]
    [ApiController]
    public class CajaController : ControllerBase
    {
        private readonly CajaRepository _repository;
        public CajaController(CajaRepository repository)
        {
            _repository = repository;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var cajas = _repository.GetAll();
        //    return Ok(cajas);
        //}

        [HttpGet]
        public async Task<IActionResult> GetLast()
        {
            var caja = _repository.GetLast();
            return Ok(caja);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var caja = _repository.GetOne(id);
            return Ok(caja);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CajaModel model)
        {
            return Ok(_repository.Post(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] CajaModel model)
        {
            return Ok(_repository.Put(id, model));
        }
    }
}
