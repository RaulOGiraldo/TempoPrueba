using System;
using System.Linq;
using System.Threading.Tasks;
using TempoPrueba.Core.DTOs;
using TempoPrueba.Core.Entities;
using TempoPrueba.Intrastructure.Repositories;
using Xunit;

namespace TempoPrueba.UnitTest
{
    public class ServicesProvedorTest 
    {
        private readonly BasePruebas _basepruebas;
        public ServicesProvedorTest() 
        {
            _basepruebas = new BasePruebas();
        }

        [Fact]
        public async Task GetAll_ReturnAllProveedores()
        {
            // Preparación
            var coleccionProveedores = _basepruebas.ObtenerColeccion<Proveedor>("Proveedores");
            var _repository = new ProveedorRepository(coleccionProveedores);
            
            // Prueba
            var result = _repository.GetAll();
            var respuesta = result.Result.Count();

            // Verificación
            Assert.NotNull(result);
            Assert.True(respuesta > 0);
        }

        [Fact]
        public async Task Get_ReturnOneProveedorExist()
        {
            // Preparación
            var coleccionProveedores = _basepruebas.ObtenerColeccion<Proveedor>("Proveedores");
            var _repository = new ProveedorRepository(coleccionProveedores);

            // Prueba
            var nit = "18506470";
            var result = _repository.Get(nit);
            var respuesta = result.Result.Nit;

            // Verificación
            Assert.NotNull(result);
            Assert.Equal(nit, respuesta);
        }

        [Fact]
        public async Task Insert_NewRegisterProveedor()
        {
            // Preparación
            var coleccionProveedores = _basepruebas.ObtenerColeccion<Proveedor>("Proveedores");
            var _repository = new ProveedorRepository(coleccionProveedores);

            var nit = Guid.NewGuid().ToString(); 
            var proveedorToInsert = new Proveedor
            {
                Nit = nit,
                RazonSocial = "Proveedor Insertado",
                Direccion = "Calle Falsa 123",
                Ciudad = "Ciudad de Prueba",
                Departamento = "Departamento de Prueba",
                Email = "proveedor@ejemplo.com",
                Activo = true,
                FechaCreacion = DateTime.Now,
                NombreContacto = "Contacto de Prueba",
                EmailContacto = "contacto@ejemplo.com"
            };


            // Prueba
            var result = _repository.Insert(proveedorToInsert);
            var respuesta = result.Result.Nit;

            // Verificación
            Assert.NotNull(result);
            Assert.Equal(nit, respuesta);
        }

        [Fact]
        public async Task Update_RegisterProveedorExist()
        {
            // Preparación
            var coleccionProveedores = _basepruebas.ObtenerColeccion<Proveedor>("Proveedores");
            var _repository = new ProveedorRepository(coleccionProveedores);

            var nit = "18506470";
            var comp = Guid.NewGuid().ToString().Substring(1, 10);
            var proveedorToInsert = new ProveedorDTO
            {
                Nit = nit,
                RazonSocial = "Proveedor : " + comp,
                Direccion = "Calle Falsa 123",
                Ciudad = "Ciudad de Prueba",
                Departamento = "Departamento de Prueba",
                Email = "proveedor@ejemplo.com",
                Activo = true,
                FechaCreacion = DateTime.Now,
                NombreContacto = "Contacto de Prueba",
                EmailContacto = "contacto@ejemplo.com"
            };


            // Prueba
            var result = _repository.Update(nit, proveedorToInsert);
            var respuesta = result.Result;

            // Verificación
            Assert.NotNull(result);
            Assert.True(respuesta);
        }

        [Fact]
        public async Task Delete_RegisterProveedor()
        {
            // Preparación
            var coleccionProveedores = _basepruebas.ObtenerColeccion<Proveedor>("Proveedores");
            var _repository = new ProveedorRepository(coleccionProveedores);

            var nit = Guid.NewGuid().ToString();
            var proveedorToInsert = new Proveedor
            {
                Nit = nit,
                RazonSocial = "Proveedor para Eliminar",
                Direccion = "Calle Falsa 12300",
                Ciudad = "Ciudad de Prueba",
                Departamento = "Departamento de Prueba",
                Email = "proveedor@ejemplo.com",
                Activo = true,
                FechaCreacion = DateTime.Now,
                NombreContacto = "Contacto de Prueba",
                EmailContacto = "contacto@ejemplo.com"
            };


            // Prueba
            var result = _repository.Insert(proveedorToInsert);

            var resultDel = _repository.Delete(nit);
            var respuesta = resultDel.Result;

            // Verificación
            Assert.NotNull(result);
            Assert.NotNull(resultDel);
            Assert.True(respuesta);
        }
    }
}
