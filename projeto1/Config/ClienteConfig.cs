using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projeto1.Domain;

namespace projeto1.Config;

public class ClienteConfig : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        //Aula 8.11
        builder.OwnsOne(_ => _.Endereco, e =>
            {
                e.Property(_ => _.Bairro)
                    .HasColumnName("Bairro");
                // e.ToTable("Enderecos_Clientes");
            });
    }
}