using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using projeto1.Data;

namespace DominandoEFCore;
class Program
{
    static void Main(string[] args)
    {
        // Console.WriteLine("Hello, World!");

        // EnsureCreatedAndDeleted();
        // GapDoEnsureCreated();

        FixGapEnsureCreated();
    }

    static void EnsureCreatedAndDeleted()
    {
        using var db = new ApplicationContext();

        // db.Database.EnsureCreated();
        db.Database.EnsureDeleted();
    }

    static void GapDoEnsureCreated()
    {
        using var db1 = new ApplicationContext();
        using var db2 = new ApplicationContextCidade();

        db1.Database.EnsureCreated();
        db2.Database.EnsureCreated();
    }

    static void FixGapEnsureCreated()
    {
        using var db1 = new ApplicationContext();
        using var db2 = new ApplicationContextCidade();

        db1.Database.EnsureCreated();
        db2.Database.EnsureCreated();

        var databaseCreator = db2.GetService<IRelationalDatabaseCreator>();
        databaseCreator.CreateTables();
    }
}