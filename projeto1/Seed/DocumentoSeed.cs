using Microsoft.EntityFrameworkCore;
using projeto1.Domain;

namespace projeto1.Seed;

public static class DocumentoSeed
{
    public static void SowDocumentos(ModelBuilder modelBuilder)
    {
        var doc = new Documento() { Id = 1 };
        doc.SetCpf("12345678901");

        modelBuilder.Entity<Documento>().HasData(doc);
    }
}