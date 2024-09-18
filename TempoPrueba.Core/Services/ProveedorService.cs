using TempoPrueba.Core.DTOs;
using TempoPrueba.Core.Entities;
using TempoPrueba.Core.Interfaces;
using TempoPrueba.Core.QueryFilters;

namespace TempoPrueba.Core.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly IProveedorRepository _repository;
        public ProveedorService(IProveedorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Proveedor>> GetAll(ProveedorQueryFilter filters)
        {
            var pasos = await _repository.GetAll();

            if (filters.Nit != null)
            {
                pasos = pasos.Where(x => x.Nit== filters.Nit);
            }
            if (filters.RazonSocial != null)
            {
                pasos = pasos.Where(x => x.RazonSocial.ToLower().Contains(filters.RazonSocial.ToLower()));
            }

            return pasos;
        }

        public async Task<Proveedor> Get(string Id)
        {
            return await _repository.Get(Id);
        }
        public async Task<bool> Insert(Proveedor tbCampo)
        {
            var regx = await _repository.Insert(tbCampo);
            return regx != null ? true : false;
        }
        public async Task<bool> Update(string Id, ProveedorDTO tbCampo)
        {
            return await _repository.Update(Id, tbCampo);
        }
        public async Task<bool> Delete(string Id)
        {
            return await _repository.Delete(Id);
        }

    }
}
