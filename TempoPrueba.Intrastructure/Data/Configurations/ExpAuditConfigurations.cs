using InsttanttFlujos.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcopetrolPlaneacion.Infrastructure.Data.Configurations
{
    class ExpAuditConfigurations : IEntityTypeConfiguration<ExpAudit>
    {
        public void Configure(EntityTypeBuilder<ExpAudit> entity)
        {

            entity.HasKey(e => e.Id);

            entity.ToTable("ExpAuditorias");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");

            entity.Property(e => e.Fecha)
                .HasColumnType("DateTime")
                .HasColumnName("Fecha");

            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Usuario");

            entity.Property(e => e.Terminal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Terminal");

            entity.Property(e => e.Accion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Accion");

            entity.Property(e => e.Tabla)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Tabla");

            entity.Property(e => e.Identificador)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("Identificador");

            entity.Property(e => e.Aplicacion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Aplicacion");

            entity.Property(e => e.Justificacion)
                .IsUnicode(false)
                .HasColumnName("Justificacion");

        }
    }
}
