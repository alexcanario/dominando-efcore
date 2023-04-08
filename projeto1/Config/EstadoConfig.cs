using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projeto1.Domain;

namespace projeto1.Config;

public class EstadoConfig : IEntityTypeConfiguration<Estado>
{
    public void Configure(EntityTypeBuilder<Estado> builder)
    {
        builder.ToTable("Estados");

        builder
            .HasOne(e => e.Governador)
            .WithOne(g => g.Estado)
            .HasForeignKey<Governador>(g => g.EstadoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}