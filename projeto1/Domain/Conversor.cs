using System.Net;

namespace projeto1.Domain;

public class Conversor
{
    public int Id { get; set; }
    public bool Ativo { get; set; }
    public bool Excluido { get; set; }
    public Versao Versao { get; set; }
    public IPAddress? EnderecoIp { get; set; }
    public Status Status { get; set; }
}

public enum Versao
{
    EfCore1,
    EfCore2,
    EfCore3,
    EfCore5,
}

public enum Status
{
    Analise,
    Enviado,
    Devolvido
}