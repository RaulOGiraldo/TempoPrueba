using EcopetrolPlaneacion.Infrastructure.Data.Configurations;
using InsttanttFlujos.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TempoPrueba.Infrastructure.Data.Configurations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace InsttanttFlujos.Infrastructure.Data
{
    public partial class InsttanttFlujosContext : DbContext
    {
        public InsttanttFlujosContext()
        {
        }

        public InsttanttFlujosContext(DbContextOptions<InsttanttFlujosContext> options)
            : base(options)
        {
        }

        public class YourDbContextFactory : IDesignTimeDbContextFactory<InsttanttFlujosContext>
        {
            public InsttanttFlujosContext CreateDbContext(string[] args)
            {
                var builder = WebApplication.CreateBuilder(args);
                builder.Services.AddDbContext<InsttanttFlujosContext>();

                var optionsBuilder = new DbContextOptionsBuilder<InsttanttFlujosContext>();
                optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

                return new InsttanttFlujosContext(optionsBuilder.Options);
            }
        }

        //public DbSet<TbFlujo> TbFlujos { get; set; }
        //public DbSet<TbPaso> TbPasos { get; set; }
        //public DbSet<TbFlujoEstruc> TbFlujoEstrucs { get; set; }
        //public DbSet<TbFlujoEstrucCampo> TbFlujoEstrucCampos { get; set; }
        //public DbSet<TbMovimFlujo> TbMovimFlujos { get; set; }
        public DbSet<ExpAudit> ExpAudits { get; set; }
        public DbSet<TbCampo> TbCampos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuraciones de tablas

            modelBuilder.ApplyConfiguration(new TbCampoConfigurations());
            modelBuilder.ApplyConfiguration(new ExpAuditConfigurations());

            // Generar los datos Iniciales para PASOS   
            //var paso1 = new TbPaso { IdPaso = "STP-00001", Nombre = "PASO - 1", Sigla = "P1" };
            //var paso2 = new TbPaso { IdPaso = "STP-00002", Nombre = "PASO - 2", Sigla = "P2" };
            //var paso3 = new TbPaso { IdPaso = "STP-00003", Nombre = "PASO - 3", Sigla = "P3" };
            //var paso4 = new TbPaso { IdPaso = "STP-00004", Nombre = "PASO - 4", Sigla = "P4" };
            //var paso5 = new TbPaso { IdPaso = "STP-00005", Nombre = "PASO - 5", Sigla = "P5" };

            //modelBuilder.Entity<TbPaso>().HasData(new TbPaso[] 
            //    { paso1, paso2, paso3, paso4, paso5 });

            //// Generar los datos Iniciales para CAMPOS   
            //var campo1  = new TbCampo { IdCampo = "F-00001", Nombre = "TIPO DE DOCUMENTO :", Tipo = "string", Longitud = 30, NroDecimales = 0, Validacion = "Required" };
            //var campo2  = new TbCampo { IdCampo = "F-00002", Nombre = "NUMERO DOCUMENTO :", Tipo = "number", Longitud = 10, NroDecimales = 0, Validacion = "Required, > 6" };
            //var campo3  = new TbCampo { IdCampo = "F-00003", Nombre = "PRIMER NOMBRE :", Tipo = "string", Longitud = 25, NroDecimales = 0, Validacion = "Required, > 1" };
            //var campo4  = new TbCampo { IdCampo = "F-00004", Nombre = "SEGUNDO NOMBRE :", Tipo = "string", Longitud = 25, NroDecimales = 0, Validacion = "" };
            //var campo5  = new TbCampo { IdCampo = "F-00005", Nombre = "PRIMER APELLIDO :", Tipo = "string", Longitud = 25, NroDecimales = 0, Validacion = "Required, > 1" };
            //var campo6  = new TbCampo { IdCampo = "F-00006", Nombre = "SEGUNDO APELLIDO :", Tipo = "string", Longitud = 25, NroDecimales = 0, Validacion = "" };
            //var campo7  = new TbCampo { IdCampo = "F-00007", Nombre = "DIRECCION :", Tipo = "string", Longitud = 50, NroDecimales = 0, Validacion = "Required, > 5" };
            //var campo8  = new TbCampo { IdCampo = "F-00008", Nombre = "TELEFONO :", Tipo = "number", Longitud = 15, NroDecimales = 0, Validacion = "Required, > 7" };
            //var campo9  = new TbCampo { IdCampo = "F-00009", Nombre = "PRODUCTO :", Tipo = "string", Longitud = 50, NroDecimales = 0, Validacion = "Required" };
            //var campo10 = new TbCampo { IdCampo = "F-00010", Nombre = "PRECIO :", Tipo = "decimal", Longitud = 15, NroDecimales = 2, Validacion = "Required, > 0" };


        }
    }
}
