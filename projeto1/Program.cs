using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using projeto1.Data;
using projeto1.Domain;

namespace DominandoEFCore;
class Program
{
    static void Main(string[] args)
    {
        // using var db = new ApplicationContext();
        // RecriaBanco(db);
        // Setup(db);

        // Propagacao(db);
        // Schema(db);

        // Conversor(db);

        ConversorCustomizado();
    }

    private static void RecriaBanco(ApplicationContext db)
    {
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
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

    private static void Propagacao(ApplicationContext db)
    {
        var script = db.Database.GenerateCreateScript();
        Console.WriteLine(script);
    }

    private static void Schema(ApplicationContext db)
    {
        var script = db.Database.GenerateCreateScript();
        Console.WriteLine(script);
    }

    private static void Conversor(ApplicationContext db) => Schema(db);

    private static void ConversorCustomizado()
    {
        using var db = new ApplicationContext();

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        db.Conversores?.Add(new Conversor { Status = Status.Devolvido });
        db.SaveChanges();

        var conversorEmAnalise = db.Conversores?.AsNoTracking().FirstOrDefault(p => p.Status == Status.Analise);
        var conversorDevolvido = db.Conversores?.AsNoTracking().FirstOrDefault(p => p.Status == Status.Devolvido);
    }



}