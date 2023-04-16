using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projeto1.Domain;

namespace projeto1.Config;

public class DocumentoConfig : IEntityTypeConfiguration<Documento>
{
    public void Configure(EntityTypeBuilder<Documento> builder)
    {
        builder.ToTable("Documentos");

        // builder.Property(d => d.CPF).HasField("_cpf");
        // builder.Property("_cpf");
        builder.Property("_cpf").HasColumnName("Cpf").HasMaxLength(11);
    }
}