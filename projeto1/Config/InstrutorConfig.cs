using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projeto1.Domain;

namespace projeto1.Config;

public class InstrutorConfig : IEntityTypeConfiguration<Instrutor>
{
    public void Configure(EntityTypeBuilder<Instrutor> builder)
    {
        //Aula 8.17 Configura modelo de dados TPT
        builder.ToTable("Instrutores");
    }
}