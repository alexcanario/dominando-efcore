using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using projeto1.Data;
using projeto1.Domain;

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
        // _count = 0;
        // GerenciarEstadoDaConexao(false);

        // _count = 0;
        // GerenciarEstadoDaConexao(true);

        // SqlInjection();

        // MigracoesPendentes();

        // AplicarMigracaoEmTempoDeExecucao();

        // TodasMigracoes();

        // MigracoesJaAplicadas();

        // GerarScriptDoBanco();

        // CarregamentoAdiantado();

        // CarregamentoExplicito();

        CarregamentoLento();


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

    static void SqlInjection()
    {
        using var db = new ApplicationContext();

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        var listaDepartamentos = new List<Departamento> { new Departamento("Departamento 1"), new Departamento("Departamento 2") };
        db.Departamentos?.AddRange(listaDepartamentos);
        db.SaveChanges();


        //Forma correta
        // var descricaoAntiga = "Departamento 1";
        // db.Database.ExecuteSqlRaw("update departamentos set descricao = 'Descricao 1 alterado' where descricao = {0}", descricaoAntiga);

        //Forma incorreta
        var filtro = "teste ' or 1='1";
        db.Database.ExecuteSqlRaw($"update departamentos set descricao = 'Descricao haqueado sql injection' where descricao = '{filtro}'");

        if (db.Departamentos == null) return;
        foreach (var dep in db.Departamentos.AsNoTracking())
        {
            Console.WriteLine($"Id: {dep.Id}, Departamento: {dep.Descricao}");
        }
    }

    static void MigracoesPendentes()
    {
        using var db = new ApplicationContext();

        var migracoesPendentes = db.Database.GetPendingMigrations();

        Console.WriteLine($"Total: {migracoesPendentes.Count()}");

        foreach (var migracao in migracoesPendentes)
        {
            Console.WriteLine($"Migracao: {migracao}");
        }
    }

    static void AplicarMigracaoEmTempoDeExecucao()
    {
        using var db = new ApplicationContext();

        db.Database.Migrate();
    }

    static void TodasMigracoes()
    {
        using var db = new ApplicationContext();

        var migracoes = db.Database.GetMigrations();

        Console.WriteLine($"Total: {migracoes.Count()}");

        foreach (var migracao in migracoes)
        {
            Console.WriteLine($"Migracao: {migracao}");
        }
    }

    static void MigracoesJaAplicadas()
    {
        using var db = new ApplicationContext();

        var migracoes = db.Database.GetAppliedMigrations();

        Console.WriteLine($"Total de migracoes aplicadas: {migracoes.Count()}");

        foreach (var migracao in migracoes)
        {
            Console.WriteLine($"Migracao aplicada: {migracao}");
        }
    }

    static void GerarScriptDoBanco()
    {
        using var db = new ApplicationContext();
        var script = db.Database.GenerateCreateScript();

        Console.WriteLine(script);
    }

    static void CarregamentoAdiantado()
    {
        using var db = new ApplicationContext();
        PopularTabelas(db);

        var departamentos = db.Departamentos?.Include(d => d.Funcionarios);
        if (!departamentos.Any())
        {
            Console.WriteLine($"Sem departamentos para exibir!");
            return;
        }

        foreach (var dep in departamentos)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Departamento: {dep.Descricao}");

            if (dep.Funcionarios?.Any() ?? false)
            {
                foreach (var func in dep.Funcionarios)
                {
                    Console.WriteLine($"\tFuncionario: {func.Nome}");
                }
            }
            else
            {
                Console.WriteLine($"\tNenhum funcionário encontrado.");
            }
        }
    }

    static void CarregamentoExplicito()
    {
        using var db = new ApplicationContext();
        PopularTabelas(db);

        var departamentos = db.Departamentos;

        foreach (var dep in departamentos)
        {
            if (dep.Id == 2)
            {
                // db.Entry(dep).Collection(d => d.Funcionarios).Load();
                db.Entry(dep).Collection(d => d.Funcionarios).Query().Where(d => d.Id > 2).ToList();
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Departamento: {dep.Descricao}");

            if (dep.Funcionarios?.Any() ?? false)
            {
                foreach (var func in dep.Funcionarios)
                {
                    Console.WriteLine($"\tFuncionario: {func.Nome}");
                }
            }
            else
            {
                Console.WriteLine($"\tNenhum funcionário encontrado.");
            }
        }
    }

    static void CarregamentoLento()
    {
        using var db = new ApplicationContext();
        PopularTabelas(db);

        var departamentos = db.Departamentos.ToList();

        // db.ChangeTracker.LazyLoadingEnabled = false;


        foreach (var dep in departamentos)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Departamento: {dep.Descricao}");

            if (dep.Funcionarios?.Any() ?? false)
            {
                foreach (var func in dep.Funcionarios)
                {
                    Console.WriteLine($"\tFuncionario: {func.Nome}");
                }
            }
            else
            {
                Console.WriteLine($"\tNenhum funcionário encontrado.");
            }
        }
    }

    static void PopularTabelas(ApplicationContext db)
    {
        if (!db.Departamentos.Any())
        {
            db.Departamentos.AddRange(
                new Departamento { Descricao = "Departamento 01", Funcionarios = new List<Funcionario> { new Funcionario("Alex Canario", "111.222.333.44", 1) } },
                new Departamento
                {
                    Descricao = "Departamento 02",
                    Funcionarios = new List<Funcionario> {
                        new Funcionario("Nanda Canario", "222.333.444.55", 2),
                        new Funcionario("Kaique Canario", "333.444.555.66", 3),
                        new Funcionario("Breno Canario", "444.555.666.77", 4),
                        new Funcionario("Bruna Canario", "555.666.777.88", 4),
                    }
                }
            );

            db.SaveChanges();
            db.ChangeTracker.Clear();
        }
    }



}