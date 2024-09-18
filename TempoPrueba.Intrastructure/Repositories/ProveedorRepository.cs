using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TempoPrueba.Core.DTOs;
using TempoPrueba.Core.Entities;
using TempoPrueba.Core.Interfaces;
using TempoPrueba.Intrastructure.Data;

namespace TempoPrueba.Intrastructure.Repositories
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly IMongoCollection<Proveedor> _context;

        // Constructor para inyectar la colección desde las pruebas
        public ProveedorRepository(IMongoCollection<Proveedor> context)
        {
            _context = context;
        }

        // Constructor para inyectar las configuraciones de MongoDB (para uso regular)
        public ProveedorRepository(IOptions<MongoDBProvider> mongoSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoSettings.Value.DataBaseName);

            _context = mongoDatabase.GetCollection<Proveedor>(mongoSettings.Value.CollectionName);
        }

        public async Task<IEnumerable<Proveedor>> GetAll()
        {
            var camps = await _context.Find(provedor => true).ToListAsync();
            return (camps);
        }
        public async Task<Proveedor> Get(string Id)
        {
            var campo = await _context.Find(proveedor => proveedor.Nit == Id).FirstOrDefaultAsync();
            return (campo);
        }
        public async Task<Proveedor> Insert(Proveedor tbCampos)
        {
            await _context.InsertOneAsync(tbCampos);
            return tbCampos;
        }
        public async Task<bool> Update(string Id, ProveedorDTO tbCampos)
        {
            var update = Builders<Proveedor>.Update
                .Set(p => p.RazonSocial, tbCampos.RazonSocial)
                .Set(p => p.Direccion, tbCampos.Direccion)
                .Set(p => p.Ciudad, tbCampos.Ciudad)
                .Set(p => p.Departamento, tbCampos.Departamento)
                .Set(p => p.Email, tbCampos.Email)
                .Set(p => p.Activo, tbCampos.Activo)
                .Set(p => p.FechaCreacion, tbCampos.FechaCreacion)
                .Set(p => p.NombreContacto, tbCampos.NombreContacto)
                .Set(p => p.EmailContacto, tbCampos.EmailContacto);

            var regs = await _context.UpdateOneAsync(p => p.Nit == Id, update);
            return (regs.ModifiedCount >= 1 ? true : false);
        }
        public async Task<bool> Delete(string Id)
        {
            var regs = await _context.DeleteOneAsync(p => p.Nit == Id);
            return (regs.DeletedCount >= 1 ? true : false);
        }
    }
}
