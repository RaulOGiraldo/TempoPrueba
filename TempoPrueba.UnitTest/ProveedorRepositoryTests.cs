using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TempoPrueba.Core.Entities;
using TempoPrueba.Intrastructure.Repositories;
using Xunit;
public class ProveedorRepositoryTests
{
    private readonly Mock<IMongoCollection<Proveedor>> _mockCollection;
    private readonly ProveedorRepository _repository;

    public ProveedorRepositoryTests()
    {
        // Configurar el mock de IMongoCollection
        _mockCollection = new Mock<IMongoCollection<Proveedor>>();

        // Pasar el mock al constructor del repositorio
        _repository = new ProveedorRepository(_mockCollection.Object);
    }

    [Fact]
    public async Task GetAll_ReturnAllProveedores()
    {
        // Configurar el comportamiento del mock
        var mockProveedores = new List<Proveedor>
        {
            new Proveedor { Nit = "123", RazonSocial = "Proveedor1" },
            new Proveedor { Nit = "456", RazonSocial = "Proveedor2" }
        };

        var mockCursor = new Mock<IAsyncCursor<Proveedor>>();
        mockCursor.Setup(_ => _.Current).Returns(mockProveedores);
        mockCursor
            .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
            .Returns(true)
            .Returns(false);
        mockCursor
            .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .ReturnsAsync(false);

        _mockCollection
            .Setup(x => x.FindAsync(It.IsAny<FilterDefinition<Proveedor>>(), It.IsAny<FindOptions<Proveedor, Proveedor>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);

        // Llamar al método que se quiere probar
        var result = await _repository.GetAll();

        // Verificar el resultado
        Assert.NotNull(result);
        Assert.Equal(2, ((List<Proveedor>)result).Count);
        _mockCollection.Verify(x => x.FindAsync(It.IsAny<FilterDefinition<Proveedor>>(), It.IsAny<FindOptions<Proveedor, Proveedor>>(), It.IsAny<CancellationToken>()), Times.Once);
    }


    [Fact]
    public async Task Get_ReturnProveedor_WhenProveedorExists()
    {
        // Configuración del proveedor mock que se va a retornar
        var mockProveedor = new Proveedor { Nit = "123", RazonSocial = "Proveedor1" };

        // Configuración del cursor para simular el resultado de la búsqueda
        var mockCursor = new Mock<IAsyncCursor<Proveedor>>();
        mockCursor.Setup(_ => _.Current).Returns(new List<Proveedor> { mockProveedor });
        mockCursor.SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
            .Returns(true)
            .Returns(false);
        mockCursor.SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .ReturnsAsync(false);

        // Configuración del mock para que Find devuelva el cursor configurado
        _mockCollection.Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<Proveedor>>(),
                It.IsAny<FindOptions<Proveedor, Proveedor>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);

        // Ejecutar el método Get en el repositorio
        var result = await _repository.Get("123");

        // Validar los resultados
        Assert.NotNull(result);
        Assert.Equal("123", result.Nit);
        Assert.Equal("Proveedor1", result.RazonSocial);

        // Verificar que FindAsync fue llamado una vez
        _mockCollection.Verify(x => x.FindAsync(It.IsAny<FilterDefinition<Proveedor>>(),
                                                It.IsAny<FindOptions<Proveedor, Proveedor>>(),
                                                It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Insert_AddProveedor_ToCollection()
    {
        // Configurar un proveedor que se va a insertar
        var proveedorToInsert = new Proveedor
        {
            Nit = "123",
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

        // Configuración del mock para InsertOneAsync
        _mockCollection.Setup(x => x.InsertOneAsync(proveedorToInsert, null, default))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Ejecutar el método Insert
        var result = await _repository.Insert(proveedorToInsert);

        // Validar los resultados
        Assert.NotNull(result);
        Assert.Equal("123", result.Nit);
        Assert.Equal("Proveedor Insertado", result.RazonSocial);

        // Verificar que InsertOneAsync fue llamado una vez con el proveedor correcto
        _mockCollection.Verify(x => x.InsertOneAsync(proveedorToInsert, null, default), Times.Once);
    }

    //[Fact]
    //public async Task Update_UpdateProveedor()
    //{
    //    // Configurar un proveedor con los datos actualizados usando ProveedorDTO
    //    var proveedorId = "123";
    //    var proveedorToUpdate = new ProveedorDTO
    //    {
    //        Nit = "123",
    //        RazonSocial = "Proveedor Actualizado",
    //        Direccion = "Calle Actualizada 456",
    //        Ciudad = "Ciudad de Prueba",
    //        Departamento = "Departamento Actualizado",
    //        Email = "proveedoractualizado@ejemplo.com",
    //        Activo = true,
    //        FechaCreacion = DateTime.Now,
    //        NombreContacto = "Contacto Actualizado",
    //        EmailContacto = "contactoactualizado@ejemplo.com"
    //    };

        // Configuración del mock para UpdateOneAsync
    //    _mockCollection.Setup(x => x.UpdateOneAsync(
    //            It.Is<FilterDefinition<Proveedor>>(f => IsMatchingFilter(f, proveedorId)),
    //            It.Is<UpdateDefinition<Proveedor>>(u => IsMatchingUpdate(u, proveedorToUpdate)),
    //            null,
    //            default))
    //        .ReturnsAsync(new UpdateResult.Acknowledged(1, 1, null))
    //        .Verifiable();

    //    // Ejecutar el método Update
    //    var result = await _repository.Update(proveedorId, proveedorToUpdate);

    //    // Validar los resultados
    //    Assert.True(result);

    //    // Verificar que UpdateOneAsync fue llamado una vez con los parámetros correctos
    //    _mockCollection.Verify(x => x.UpdateOneAsync(
    //        It.Is<FilterDefinition<Proveedor>>(f => IsMatchingFilter(f, proveedorId)),
    //        It.Is<UpdateDefinition<Proveedor>>(u => IsMatchingUpdate(u, proveedorToUpdate)),
    //        null,
    //        default),
    //    Times.Once);
    //}

    //// Método auxiliar para verificar el filtro
    //private bool IsMatchingFilter(FilterDefinition<Proveedor> filter, string expectedId)
    //{
    //    var bsonFilter = filter.ToBsonDocument();
    //    return bsonFilter["Nit"] == expectedId;
    //}

    //// Método auxiliar para verificar la actualización
    //private bool IsMatchingUpdate(UpdateDefinition<Proveedor> update, ProveedorDTO expectedProveedor)
    //{
    //    var bsonUpdate = update.ToBsonDocument();
    //    return bsonUpdate.Elements.Any(e => e.Name == "RazonSocial" && e.Value.AsString == expectedProveedor.RazonSocial) &&
    //           bsonUpdate.Elements.Any(e => e.Name == "Direccion" && e.Value.AsString == expectedProveedor.Direccion) &&
    //           bsonUpdate.Elements.Any(e => e.Name == "Departamento" && e.Value.AsString == expectedProveedor.Departamento);
    //}



    //[Fact]
    //public async Task Delete_ShouldDeleteProveedor()
    //{
    //    // Configuración del mock para DeleteOneAsync
    //    _mockCollection.Setup(x => x.DeleteOneAsync(
    //            It.IsAny<FilterDefinition<Proveedor>>(),
    //            default))
    //        .ReturnsAsync(new DeleteResult.Acknowledged(1))
    //        .Verifiable();

    //    // Ejecutar el método Delete
    //    var result = await _repository.Delete("123");

    //    // Validar los resultados
    //    Assert.True(result);

    //    // Verificar que DeleteOneAsync fue llamado una vez con el filtro correcto
    //    _mockCollection.Verify(x => x.DeleteOneAsync(
    //        It.Is<FilterDefinition<Proveedor>>(f => f.ToBsonDocument()["Nit"] == "123"),
    //        default),
    //    Times.Once);
    //}

}
