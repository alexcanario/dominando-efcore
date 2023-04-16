namespace projeto1.Domain;

public class Ator
{
    public int Id { get; set; }
    public string? Nome { get; set; }

    public List<Filme>? Filmes { get; } = new();
}