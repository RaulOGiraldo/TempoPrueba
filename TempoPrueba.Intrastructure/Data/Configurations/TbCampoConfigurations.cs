using InsttanttFlujos.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TempoPrueba.Infrastructure.Data.Configurations
{
    class TbCampoConfigurations : IEntityTypeConfiguration<TbCampo>
    {
        public void Configure(EntityTypeBuilder<TbCampo> entity)
        {

            entity.HasKey(e => e.IdCampo);

            entity.ToTable("TbCampos");

            entity.Property(e => e.IdCampo)
                .HasColumnName("IdCampo")
                .HasMaxLength(7);

            entity.Property(e => e.Nombre)
                .HasColumnName("Nombre")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Tipo)
                .HasColumnName("Tipo")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Longitud)
                .HasColumnName("Longitud")
                .HasColumnType("Integer")
                .IsUnicode(false);

            entity.Property(e => e.NroDecimales)
                .HasColumnName("NroDecimales")
                .HasColumnType("Integer")
                .IsUnicode(false);

            entity.Property(e => e.Validacion)
                .HasColumnName("Validacion")
                .HasMaxLength(250)
                .IsUnicode(false);
        }
    }
}
