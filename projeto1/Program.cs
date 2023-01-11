using Microsoft.Data.SqlClient;
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

        // ConsultaProjetada();

        // ConsultaParametrizada();

        // ConsultaInterpolada();

        // ConsultaComTag();

        // EntendendoConsulta1xN();

        // EntendendoConsultaNx1();

        // DivisaoConsultas();

        IgnorandoSplitQuery();
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

    static void ConsultaParametrizada()
    {
        using var db = new ApplicationContext();
        Setup(db);

        // var id = 0;

        var id = new SqlParameter
        {
            Value = 1,
            SqlDbType = System.Data.SqlDbType.Int
        };

        var departamentos = db.Departamentos?
        // .FromSqlRaw("SELECT * FROM Departamentos WITH(NOLOCK)")
        //A consulta abaixo utilizando o FromSqlRaw, utiliza o recurso de sub-consulta, porém também podemos utilzar o LINQ como o Where abaixo
         .FromSqlRaw("SELECT * FROM Departamentos WHERE Id > {0}", id)
         .Where(p => !p.Excluido)
        .ToList();

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

    static void ConsultaInterpolada()
    {
        using var db = new ApplicationContext();
        Setup(db);

        var id = 1;
        var departamentos = db.Departamentos?
            .FromSqlInterpolated($"SELECT * FROM Departamentos WHERE Id > {id}")
            .ToList();

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

    static void ConsultaComTag()
    {
        using var db = new ApplicationContext();
        Setup(db);

        var departamentos = db.Departamentos?
            .TagWith(@"Estou enviando uma tag para comentar minha query.
                Segunda linha
                Terceira linha")
            .ToList();

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

    private static void EntendendoConsulta1xN()
    {
        using var db = new ApplicationContext();
        Setup(db);

        var departamentos = db.Departamentos?
            .Include(p => p.Funcionarios)
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
                Console.WriteLine($"\tNome: {func.Nome}");
            }
        }
    }

    private static void EntendendoConsultaNx1()
    {
        using var db = new ApplicationContext();
        Setup(db);

        var funcionarios = db.Funcionarios?
            .Include(p => p.Departamento)
            .ToList();

        if (funcionarios == null)
        {
            Console.WriteLine("Não existem funcionarios:");
            return;
        }

        foreach (var func in funcionarios)
        {
            Console.WriteLine($"Nome: {func.Nome} / Departamento: {func.Departamento?.Descricao}");
        }
    }

    private static void DivisaoConsultas()
    {
        using var db = new ApplicationContext();
        Setup(db);

        var departamentos = db.Departamentos?
            .Include(p => p.Funcionarios)
            .Where(d => d.Id < 3)
            //O método AsSplitQuery resolve o problema da explosao cartesiana no carregamento adiantado
            .AsSplitQuery()
            .ToList();

        if (departamentos == null)
        {
            Console.WriteLine("Não existem departamentos:");
            return;
        }

        foreach (var dep in departamentos)
        {
            Console.WriteLine($"Descricao: {dep.Descricao}");
            if (dep.Funcionarios != null)
            {
                foreach (var func in dep.Funcionarios)
                {
                    Console.WriteLine($"\tNome: {func.Nome}");
                }
            }
        }
    }

    private static void IgnorandoSplitQuery()
    {
        using var db = new ApplicationContext();
        Setup(db);

        var departamentos = db.Departamentos?
            .Include(p => p.Funcionarios)
            .Where(d => d.Id < 3)
            //O método AsSingleQuery ignora o recurso de SplitQuery definido de forma global no OnConfigure do context
            .AsSingleQuery()
            .ToList();

        if (departamentos == null)
        {
            Console.WriteLine("Não existem departamentos:");
            return;
        }

        foreach (var dep in departamentos)
        {
            Console.WriteLine($"Descricao: {dep.Descricao}");
            if (dep.Funcionarios != null)
            {
                foreach (var func in dep.Funcionarios)
                {
                    Console.WriteLine($"\tNome: {func.Nome}");
                }
            }
        }
    }
}