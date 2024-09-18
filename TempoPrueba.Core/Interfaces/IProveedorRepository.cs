using TempoPrueba.Core.DTOs;
using TempoPrueba.Core.Entities;

namespace TempoPrueba.Core.Interfaces
{
    public interface IProveedorRepository
    {
        Task<IEnumerable<Proveedor>> GetAll();
        Task<Proveedor> Get(string Id);
        Task<Proveedor> Insert(Proveedor tbCampos);
        Task<bool> Update(string Id, ProveedorDTO tbCampos);
        Task<bool> Delete(string Id);
    }
}
