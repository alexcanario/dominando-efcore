namespace projeto1.Domain;

public class Cliente
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Telefone { get; set; }
    public Endereco? Endereco { get; set; }
}