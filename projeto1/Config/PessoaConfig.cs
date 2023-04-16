using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projeto1.Domain;

namespace projeto1.Config;

public class PessoaConfig : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.ToTable("Pessoas");

        builder
            .HasDiscriminator<int>("TipoPessoa")
                .HasValue<Pessoa>(1)
                .HasValue<Aluno>(2)
                .HasValue<Instrutor>(3);
    }
}