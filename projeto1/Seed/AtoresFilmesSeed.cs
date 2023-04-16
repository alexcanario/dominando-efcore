using Microsoft.EntityFrameworkCore;
using projeto1.Domain;

namespace projeto1.Seed;

public static class AtoresFilmesSeed
{
    public static void SowFilmesEAtores(ModelBuilder modelBuilder)
    {
        Ator a1 = new() { Id = 1, Nome = "Rafael" };
        Ator a2 = new() { Id = 2, Nome = "Pires" };
        Ator a3 = new() { Id = 3, Nome = "Bruno" };

        Filme f1 = new() { Id = 1, Descricao = "A volta dos que não foram" };
        Filme f2 = new() { Id = 2, Descricao = "De volta para o futuro" };
        Filme f3 = new() { Id = 3, Descricao = "Poeira em alto mar" };

        // a1.Filmes?.Add(f1);
        // a1.Filmes?.Add(f2);

        // a2.Filmes?.Add(f2);
        // a2.Filmes?.Add(f3);

        // a3.Filmes?.Add(f3);
        // a3.Filmes?.Add(f1);

        modelBuilder.Entity<Filme>().HasData(f1, f2, f3);
        modelBuilder.Entity<Ator>().HasData(a1, a2, a3);
    }
}