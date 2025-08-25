using IntegracaoWebApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntegracaoWebApi.Infrastructure.Configurations
{
    public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Enderecos");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .UseIdentityColumn();

            builder.Property(e => e.Cep)
                   .HasMaxLength(9) 
                   .IsRequired();

            builder.Property(e => e.Rua)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(e => e.Regiao)
                   .HasMaxLength(120)
                   .IsRequired();

            builder.Property(e => e.Cidade)
                   .HasMaxLength(120)
                   .IsRequired();

            builder.Property(e => e.Estado)
                   .HasMaxLength(2)
                   .IsRequired();

            builder.HasIndex(e => e.Cep)
                   .IsUnique();
        }
    }
}
