using Microsoft.EntityFrameworkCore;
using projeto1.Domain;

namespace projeto1.Seed;

public class AlunosSeed
{
    public static void SowAlunos(ModelBuilder modelBuilder)
    {
        var aluno = new Aluno() { Id = 2, Nome = "Maria Carvalho", DataContrato = DateTime.Now, Idade = 21 };

        modelBuilder.Entity<Aluno>().HasData(aluno);
    }
}