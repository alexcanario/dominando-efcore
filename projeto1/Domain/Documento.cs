namespace projeto1.Domain;

public class Documento
{
    private string? _cpf;

    public int Id { get; set; }

    public void SetCpf(string cpf)
    {
        if (string.IsNullOrEmpty(cpf))
        {
            throw new Exception("CPF Inválido!");
        }

        _cpf = cpf;
    }

    // public string? CPF => _cpf;
    public string? GetCPF() => _cpf;
}