using MiAlmacen.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiAlmacen.Blazor.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Clientes>> GetAllClientes();
    }
}
