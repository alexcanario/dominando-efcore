using Microsoft.EntityFrameworkCore;
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

        // FixGapEnsureCreated();

        // HealthCheckDatabase();

        //Aquecer os dados
        // new ApplicationContext().Departamentos?.AsNoTracking().Any();
        _count = 0;
        GerenciarEstadoDaConexao(false);

        _count = 0;
        GerenciarEstadoDaConexao(true);
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

    static void HealthCheckDatabase()
    {
        using var db = new ApplicationContext();
        var canConnect = db.Database.CanConnect();

        var msg = canConnect ? "Posso me conectar!" : "Não posso me conectar";
        Console.WriteLine(msg);
    }

    static int _count;
    static void GerenciarEstadoDaConexao(bool gerenciarEstadoConexao = false)
    {
        using var db = new ApplicationContext();
        var time = System.Diagnostics.Stopwatch.StartNew();

        var conexao = db.Database.GetDbConnection();
        conexao.StateChange += (_, __) => ++_count;
        if (gerenciarEstadoConexao)
        {
            conexao.Open();
        }

        for (var i = 0; i < 200; i++)
        {
            db.Departamentos?.AsNoTracking().Any();
        }

        time.Stop();
        var msg = $"Tempo: {time.Elapsed.ToString()}, {gerenciarEstadoConexao}, Contador: {_count}";

        Console.WriteLine(msg);
    }

    static void ExecuteSql()
    {
        using var db = new ApplicationContext();

        //Primeira opcao
        using (var cmd = db.Database.GetDbConnection().CreateCommand())
        {
            cmd.CommandText = "SELECT 1";
            cmd.ExecuteNonQuery();
        }

        //Segunda opcao
        var descricao = "teste";
        // db.Database.ExecuteSqlRaw("update departamentos set descricao = 'teste' where id = 1");
        //Opcao mais seguro, nao permite sql injection
        //Transforma o parametro em dbparameter
        db.Database.ExecuteSqlRaw("update departamentos set descricao = {0} where id = 1", descricao);

        //Terceira opcao
        //Transforma o parametro em dbparameter
        db.Database.ExecuteSqlInterpolated($"update departamentos set descricao = {descricao} where id = 1");

    }
}