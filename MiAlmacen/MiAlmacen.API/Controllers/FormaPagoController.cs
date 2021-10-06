using MiAlmacen.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MiAlmacen.API.Controllers
{
    [Route("api/formaspago")]
    [ApiController]
    public class FormaPagoController : ControllerBase
    {
        private readonly FormaPagoRepository _repository;
        public FormaPagoController(FormaPagoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fpago = _repository.GetAll();
            return Ok(fpago);
        }
    }
}