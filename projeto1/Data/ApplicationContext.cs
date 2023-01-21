using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using projeto1.Domain;

namespace projeto1.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly StreamWriter _writer = new StreamWriter("ef-core-log-txt", append: true);
        public DbSet<Funcionario>? Funcionarios { get; set; }
        public DbSet<Departamento>? Departamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConn = "Server=(local); Database=C003; Encrypt=True; Integrated Security=True; Trust Server Certificate=true; pooling=true; MultipleActiveResultSets=True";
            optionsBuilder
                .UseSqlServer(strConn, o => o.MaxBatchSize(100))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();

        }

        public override void Dispose()
        {
            _writer.Dispose();
            base.Dispose();
        }
    }
}