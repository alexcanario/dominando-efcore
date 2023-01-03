using Microsoft.EntityFrameworkCore;
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
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Filtro Global
            modelBuilder.Entity<Departamento>().HasQueryFilter(d => !d.Excluido);
            base.OnModelCreating(modelBuilder);
        }
    }
}