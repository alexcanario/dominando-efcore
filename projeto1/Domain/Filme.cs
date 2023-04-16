namespace projeto1.Domain;

public class Filme
{
    public int Id { get; set; }
    public string? Descricao { get; set; }

    public List<Ator>? Atores { get; } = new();
}