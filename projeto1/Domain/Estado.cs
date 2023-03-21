namespace projeto1.Domain;

public class Estado
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    // public int GovernadorId { get; set; }
    public Governador? Governador { get; set; }
}