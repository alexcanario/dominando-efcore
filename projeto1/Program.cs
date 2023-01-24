using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using projeto1.Data;
using projeto1.Domain;

namespace DominandoEFCore;
class Program
{
    static void Main(string[] args)
    {
        // ConsultarDepartamento();

        // DadosSensiveis();

        // HabilitandoBatchSize();

        TempoComandoGeral();
    }

    private static void Setup(ApplicationContext db)
    {
        if (db.Departamentos == null) return;

        if (!db.Departamentos.Any())
        {
            db.Departamentos.AddRange(
                new Departamento { Descricao = "Departamento 01", Excluido = true, Funcionarios = new List<Funcionario> { new Funcionario("Alex Canario", "111.222.333.44", 1) } },
                new Departamento
                {
                    Descricao = "Departamento 02",
                    Excluido = false,
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

    private static void ConsultarDepartamento()
    {
        using var db = new ApplicationContext();

        var departamentos = db.Departamentos?.Where(d => d.Id > 0).ToArray();
    }

    private static void DadosSensiveis()
    {
        using var db = new ApplicationContext();

        var descricao = "Departamento 01";
        var departamentos = db.Departamentos?.Where(d => d.Descricao == descricao).ToArray();
    }

    private static void HabilitandoBatchSize()
    {
        using var db = new ApplicationContext();

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        for (var i = 1; i <= 50; i++)
        {
            db.Departamentos?.Add(new Departamento { Descricao = $"Departamento " + i });
        }

        db.SaveChanges();
    }

    private static void TempoComandoGeral()
    {
        using var db = new ApplicationContext();

        // db.Database.ExecuteSqlRaw("SELECT 1");

        db.Database.SetCommandTimeout(10);

        //Executando o mesmo comando sql com um tempo de 7 segundos
        db.Database.ExecuteSqlRaw("WAITFOR DELAY '00:00:07'; SELECT 1");
    }
}