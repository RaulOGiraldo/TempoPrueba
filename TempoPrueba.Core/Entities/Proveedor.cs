using MongoDB.Bson.Serialization.Attributes;

namespace TempoPrueba.Core.Entities
{
    public class Proveedor
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Nit { get; set; }
        public string? RazonSocial { get; set; }
        public string? Direccion { get; set; }
        public string? Ciudad { get; set; }
        public string? Departamento { get; set; }
        public string? Email { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? NombreContacto { get; set; }
        public string? EmailContacto { get; set; }
    }
}
