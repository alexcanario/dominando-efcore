using Microsoft.EntityFrameworkCore;
using projeto1.Domain;

namespace projeto1.Seed;

public static class ClienteSeed
{
    public static void SowCliente(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(c =>
                    {
                        c.HasData(new Cliente
                        {
                            Id = 10,
                            Nome = "Zezinho"
                        });

                        c.OwnsOne(a => a.Endereco).HasData(
                            new
                            {
                                ClienteId = 10,
                                Logradouro = "Rua"
                            }
                        );
                    });
    }
}