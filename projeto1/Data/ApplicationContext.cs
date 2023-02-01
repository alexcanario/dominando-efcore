using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using projeto1.Conversores;
using projeto1.Domain;

namespace projeto1.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Funcionario>? Funcionarios { get; set; }
        public DbSet<Departamento>? Departamentos { get; set; }
        public DbSet<Estado>? Estados { get; set; }
        public DbSet<Conversor>? Conversores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConn = "Server=(local); Database=C003; Encrypt=True; Integrated Security=True; Trust Server Certificate=true; pooling=true; MultipleActiveResultSets=True";
            optionsBuilder
                .UseSqlServer(strConn)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");

            // modelBuilder.Entity<Departamento>().Property(p => p.Descricao).UseCollation("SQL_Latin1_General_CP1_CS_AS");

            // modelBuilder.HasSequence<int>("seq_funcionario_id", "my_sequences")
            //     .StartsAt(1)
            //     .IncrementsBy(2)
            //     .HasMin(1);

            // modelBuilder.Entity<Funcionario>().Property(p => p.Id).HasDefaultValueSql("NEXT VALUE FOR my_sequences.seq_funcionario_id");

            //8.03 Índices
            // modelBuilder.Entity<Departamento>()
            //     .HasIndex(p => p.Descricao);

            //Índide com chave composto
            // modelBuilder.Entity<Funcionario>()
            //     .HasIndex(p => new { p.Nome, p.Cpf })
            //     .HasDatabaseName("idx_meu_indice_composto")
            //     .HasFilter("Nome IS NOT NULL") //Filtro para nao indexar funcionarios com nome nulo
            //     .HasFillFactor(80)
            //     .IsUnique();

            //Aula 8.04 Propagação de dados (SEED)
            // modelBuilder.Entity<Estado>().HasData(
            //     new Estado { Id = 1, Nome = "Bahia" },
            //     new Estado { Id = 2, Nome = "Sergipe" }
            // );

            //Aula 8.05 Schemas
            // modelBuilder.HasDefaultSchema("Cadastros");
            // modelBuilder.Entity<Estado>().ToTable("Estados", "Uf");

            //Aula 8.06 Conversores
            // var conversor1 = new ValueConverter<Versao, string>(p => p.ToString(), p => (Versao)Enum.Parse(typeof(Versao), p));
            // var conversor2 = new EnumToStringConverter<Versao>();

            // modelBuilder.Entity<Conversor>()
            //     .Property(p => p.Versao)
            //     //.HasConversion<string>();
            //     //.HasConversion(p => p.ToString(), p => (Versao)Enum.Parse(typeof(Versao), p));
            //     // .HasConversion(conversor1);
            //     .HasConversion(conversor2);

            //Conversores disponiveis
            //Microsoft.EntityFrameworkCore.Storage.ValueConversion...

            //Aula 8.07
            // modelBuilder.Entity<Conversor>()
            //     .Property(p => p.Status)
            //     .HasConversion(new ConversorCustom());

            //Aula 8.09
            modelBuilder.Entity<Departamento>().Property<DateTime>("UltimaAtualizacao");

        }
    }
}