using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projeto1.Domain;
using projeto1.Seed;

namespace projeto1.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Funcionario>? Funcionarios { get; set; }
        public DbSet<Departamento>? Departamentos { get; set; }
        public DbSet<Estado>? Estados { get; set; }
        public DbSet<Conversor>? Conversores { get; set; }
        public DbSet<Cliente>? Clientes { get; set; }

        //ALex Canario 16/04/2023
        public DbSet<Ator>? Atores { get; set; }
        public DbSet<Filme>? Filmes { get; set; }

        //Alex Canario 16/04/2023
        public DbSet<Documento>? Documentos { get; set; }

        //Alex Canario 16/04/2023, Aula 8.16, configurando modelo de dados com TPH
        public DbSet<Pessoa>? Pessoas { get; set; }
        public DbSet<Instrutor>? Instrutores { get; set; }
        public DbSet<Aluno>? Alunos { get; set; }

        //Alex Canario, 16/04/2023, Aula 8.18, Bolsa de propriedades
        public DbSet<Dictionary<string, object>>? Configuracoes => Set<Dictionary<string, object>>("Configuracoes");

        // Aula 9.01, 01/05/2023 Atributo Table
        public DbSet<Atributo>? Atributos { get; set; }

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
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");

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

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

            //Alex Canario, 16/04/2023, Aula 8.18, Bolsa de propriedades
            modelBuilder.SharedTypeEntity<Dictionary<string, object>>("Configuracoes", b =>
            {
                b.Property<int>("Id");

                b.Property<string>("Chave").HasColumnType("VARCHAR(40)").IsRequired(true);
                b.Property<string>("Valor").HasColumnType("VARCHAR(255)").IsRequired(true);
            });

            ClienteSeed.SowCliente(modelBuilder);
            EstadoSeed.SowEstado(modelBuilder);
            AtoresFilmesSeed.SowFilmesEAtores(modelBuilder);
            DocumentoSeed.SowDocumentos(modelBuilder);
            AlunosSeed.SowAlunos(modelBuilder);
            InstrutoresSeed.SowInstrutores(modelBuilder);
        }
    }
}