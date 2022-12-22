using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projeto1.Domain;

namespace projeto1.Data
{
    public class ApplicationContextCidade : DbContext
    {
        public DbSet<Cidade>? Cidades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConn = "Server=(local); Database=C002; Encrypt=True; Integrated Security=True; Trust Server Certificate=true; pooling=true; MultipleActiveResultSets=True";
            optionsBuilder
                .UseSqlServer(strConn)
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}