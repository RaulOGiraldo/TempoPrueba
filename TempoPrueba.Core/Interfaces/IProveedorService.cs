using TempoPrueba.Core.DTOs;
using TempoPrueba.Core.Entities;
using TempoPrueba.Core.QueryFilters;

namespace TempoPrueba.Core.Interfaces
{
    public interface IProveedorService
    {
        Task<IEnumerable<Proveedor>> GetAll(ProveedorQueryFilter filters);
        Task<Proveedor> Get(string Id);
        Task<bool> Insert(Proveedor tbCampo);
        Task<bool> Update(string Id, ProveedorDTO tbCampo);
        Task<bool> Delete(string Id);
    }
}
