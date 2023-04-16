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

        // ConversorCustomizado();

        // PropriedadesDeSombra();

        // TrabalhandoComPropriedadesSombra();

        // ConsultandoPropriedadeSombra();

        // TiposDePropriedade();

        //Aula 8.12 Configurando relacionamento 1 para 1, 08/03/2023
        // Relacionamento1Para1();

        //Aula 8.13 Configurando relacionamento muitos para muitos, 16/04/2023 
        //RelacionamentoMuitosParaMuitos();

        //Aula 8.15 Campos de apoio, 16/04/2023
        // CampoDeApoio();

        //Aula 8.16 Modelo Tph
        ModeloTph();

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

    private static void PropriedadesDeSombra()
    {
        using var db = new ApplicationContext();

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }

    private static void TrabalhandoComPropriedadesSombra()
    {
        using var db = new ApplicationContext();

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        var departamento = new Departamento { Descricao = "Trabalhando com propriedade sombra" };

        db.Departamentos?.Add(departamento);
        db.Entry(departamento).Property("UltimaAtualizacao").CurrentValue = DateTime.Now;
        db.SaveChanges();
    }

    private static void ConsultandoPropriedadeSombra()
    {
        using var db = new ApplicationContext();

        var dep = db.Departamentos?.Where(p => EF.Property<DateTime>(p, "UltimaAtualizacao") < DateTime.Now).ToArray();
    }

    private static void TiposDePropriedade()
    {
        using var db = new ApplicationContext();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        var cliente = new Cliente { Nome = "Fulano", Telefone = "71 9.9600-9564", Endereco = new Endereco { Logradouro = "Rua", Bairro = "Centro", Cidade = "Salvador", Estado = "Bahia" } };

        db.Clientes?.Add(cliente);
        db.SaveChanges();

        var clientes = db.Clientes?.AsNoTracking().ToList();

        var options = new System.Text.Json.JsonSerializerOptions { WriteIndented = true };

        clientes?.ForEach(c =>
        {
            var json = System.Text.Json.JsonSerializer.Serialize(c, options);
            Console.WriteLine(json);
        });

        var clst = System.Text.Json.JsonSerializer.Serialize(clientes, options);
        Console.WriteLine(clst);
    }

    private static void Relacionamento1Para1()
    {
        using var db = new ApplicationContext();

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        var estados = db.Estados?.Include(_ => _.Governador).AsNoTracking().ToList();
        if (estados == null) return;

        estados.ForEach(e =>
        {
            Console.WriteLine($"Estado: {e.Nome}, Governador: {e.Governador?.Nome}");
        });
    }

    private static void RelacionamentoMuitosParaMuitos()
    {
        using var db = new ApplicationContext();

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        foreach (var atores in db.Atores?.Include(_ => _.Filmes))
        {
            Console.WriteLine($"Ator: {atores.Nome}");
            if (atores.Filmes != null && atores.Filmes.Any()) continue;

            foreach (var filmes in atores.Filmes)
            {
                Console.WriteLine($"\tFilme: {filmes.Descricao}");
            }
        }
    }

    //Aula 8.15 Campo de Apoio
    private static void CampoDeApoio()
    {
        using var db = new ApplicationContext();

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        // var doc = new Documento();
        // doc.SetCpf("1234567901");

        // db.Documentos?.Add(doc);
        // db.SaveChanges();

        foreach (var docs in db.Documentos?.AsNoTracking())
        {
            // Console.WriteLine($"Cpf: {docs.CPF}");
            Console.WriteLine($"Cpf: {docs.GetCPF()}");
        }
    }

    //Alex Canario 16/04/2023, Aula 8.16, configurando modelo de dados com TPH
    private static void ModeloTph()
    {
        using var db = new ApplicationContext();

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        // var instrutor = new Instrutor() { Nome = "Rafael Almeida", Desde = DateTime.Now, Materia = "Matematica" };

        // var aluno = new Aluno() { Nome = "Maria Carvalho", DataContrato = DateTime.Now, Idade = 21 };

        // db.AddRange(instrutor, aluno);
        // db.SaveChanges();

        var instrutores = db.Instrutores?.AsNoTracking().ToList();
        var alunos = db.Pessoas?.OfType<Aluno>()?.AsNoTracking().ToList();

        Console.WriteLine("Instrutores:");
        Console.WriteLine("-----------------------------------");
        foreach (var i in instrutores)
        {
            Console.WriteLine($"Id: {i.Id} -> {i.Nome}, Materia: {i.Materia}, Desde: {i.Desde}");
        }
        Console.WriteLine("");


        Console.WriteLine("Alunos:");
        Console.WriteLine("-----------------------------------");
        foreach (var a in alunos)
        {
            Console.WriteLine($"Id: {a.Id} -> {a.Nome}, Idade: {a.Idade}, Matriculado em: {a.DataContrato}");
        }
        Console.WriteLine("");
    }
}