using Microsoft.EntityFrameworkCore;
using projeto1.Domain;

namespace projeto1.Seed;

public static class EstadoSeed
{
    public static void SowEstado(ModelBuilder modelBuilder)
    {
        var sergipe = new Estado()
        {
            Id = 1,
            Nome = "Sergipe",
            Governador = new Governador { Id = 1, Nome = "Rafael Almeida", EstadoId = 1 }
        };

        modelBuilder.Entity<Estado>().HasData(sergipe);
    }
}