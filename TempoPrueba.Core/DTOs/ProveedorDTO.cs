﻿namespace TempoPrueba.Core.DTOs
{
    public class ProveedorDTO
    {
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
