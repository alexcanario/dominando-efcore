using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projeto1.Domain;

namespace projeto1.Config;

public class AlunoConfig : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        //Aula 8.17 Configura modelo de dados TPT
        builder.ToTable("Alunos");
    }
}