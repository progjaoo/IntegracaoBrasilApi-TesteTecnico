using IntegracaoWebApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntegracaoWebApi.Infrastructure.Configurations
{
    public class BancoConfiguration : IEntityTypeConfiguration<Banco>
    {
        public void Configure(EntityTypeBuilder<Banco> builder)
        {
            builder.ToTable("Bancos");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Nome)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(b => b.NomeCompleto)
                   .HasMaxLength(300);

            builder.Property(b => b.Ispb)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(b => b.Codigo);

            builder.HasIndex(b => b.Ispb)
                   .IsUnique();

            builder.HasIndex(b => b.Codigo)
                   .IsUnique()
                   .HasFilter("[Codigo] IS NOT NULL");
        }
    }
}
