using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using projeto1.Domain;

namespace projeto1.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Funcionario>? Funcionarios { get; set; }
        public DbSet<Departamento>? Departamentos { get; set; }

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

            modelBuilder.Entity<Departamento>().Property(p => p.Descricao).UseCollation("SQL_Latin1_General_CP1_CS_AS");

            modelBuilder.HasSequence<int>("seq_funcionario_id", "my_sequences")
                .StartsAt(1)
                .IncrementsBy(2)
                .HasMin(1);

            modelBuilder.Entity<Funcionario>().Property(p => p.Id).HasDefaultValueSql("NEXT VALUE FOR my_sequences.seq_funcionario_id");
        }
    }
}