using Microsoft.EntityFrameworkCore;
using projeto1.Domain;

namespace projeto1.Seed;

public class InstrutoresSeed
{
    public static void SowInstrutores(ModelBuilder modelBuilder)
    {
        var instrutor = new Instrutor() { Id = 1, Nome = "Rafael Almeida", Desde = DateTime.Now, Materia = "Matematica" };

        modelBuilder.Entity<Instrutor>().HasData(instrutor);
    }
}