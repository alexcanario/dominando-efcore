using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projeto1.Domain;

namespace projeto1.Config;

public class AtorFilmeConfig : IEntityTypeConfiguration<Ator>
{
    public void Configure(EntityTypeBuilder<Ator> builder)
    {
        builder
            .HasMany(a => a.Filmes)
            .WithMany(f => f.Atores)
            .UsingEntity(j => j.ToTable("AtorXFilme"));
    }
}