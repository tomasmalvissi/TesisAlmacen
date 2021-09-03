using MiAlmacen.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiAlmacen.Blazor.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteModel>> GetAllClientes(string filtro);
        Task<ClienteModel> GetUnCliente(int id);
        Task<ClienteModel> Alta(ClienteModel cliente);
        Task<ClienteModel> Editar(ClienteModel cliente);
        Task<int> Eliminar(int id);
    }
}
