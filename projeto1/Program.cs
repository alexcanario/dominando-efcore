using Microsoft.EntityFrameworkCore;
using projeto1.Data;
using projeto1.Domain;

namespace DominandoEFCore;
class Program
{
    static void Main(string[] args)
    {
        // Console.WriteLine("Hello, World!");

        // FiltroLocal();

        // IgnoreFiltroGlobal();

        ConsultaProjetada();

    }

    static void Setup(ApplicationContext db)
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

    static void FiltroLocal()
    {
        using var db = new ApplicationContext();
        Setup(db);

        var departamentos = db.Departamentos?.Where(d => d.Id > 0 && !d.Excluido).ToList();
        // var departamentos = db.Departamentos?.ToList();

        if (departamentos == null)
        {
            Console.WriteLine("Não existem departamentos:");
            return;
        }

        foreach (var dep in departamentos)
        {
            Console.WriteLine($"Descricao: {dep.Descricao}");
        }
    }

    static void IgnoreFiltroGlobal()
    {
        using var db = new ApplicationContext();
        Setup(db);

        var departamentos = db.Departamentos?.IgnoreQueryFilters().Where(d => d.Id > 0).ToList();

        if (departamentos == null)
        {
            Console.WriteLine("Não existem departamentos:");
            return;
        }

        foreach (var dep in departamentos)
        {
            Console.WriteLine($"Descricao: {dep.Descricao}");
        }
    }

    static void ConsultaProjetada()
    {
        using var db = new ApplicationContext();
        Setup(db);

        var departamentos = db.Departamentos?
        .Where(d => d.Id > 0)
        .Select(d => new { d.Descricao, Funcionarios = d.Funcionarios.Select(f => f.Nome) })
        .ToList();

        if (departamentos == null)
        {
            Console.WriteLine("Não existem departamentos:");
            return;
        }

        foreach (var dep in departamentos)
        {
            Console.WriteLine($"Descricao: {dep.Descricao}");
            foreach (var func in dep.Funcionarios)
            {
                Console.WriteLine($"\t Nome: {func}");
            }
        }
    }
}