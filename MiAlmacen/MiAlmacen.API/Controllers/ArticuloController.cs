using MiAlmacen.Data.Repositories;
using MiAlmacen.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiAlmacen.API.Controllers
{
    [Route("api/articulos")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly ArticuloRepository _repository;
        public ArticuloController(ArticuloRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var articulos = _repository.GetAll();
            return Ok(articulos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var articulo = _repository.GetOne(id);
            return Ok(articulo);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ArticuloModel model)
        {
            return Ok(_repository.Post(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ArticuloModel model)
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
