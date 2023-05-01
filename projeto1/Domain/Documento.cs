using Microsoft.EntityFrameworkCore;

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

    [BackingField(nameof(_cpf))]
    public string? CPF { get; }
    public string? GetCPF() => _cpf;
}