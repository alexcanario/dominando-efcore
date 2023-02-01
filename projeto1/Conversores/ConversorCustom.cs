using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using projeto1.Domain;

namespace projeto1.Conversores;

public class ConversorCustom : ValueConverter<Status, string>
{
    public ConversorCustom() : base(
        p => ConverterParaOBancoDeDados(p),
        q => ConverterParaAplicacao(q),
        new ConverterMappingHints(1))
    { }

    private static string ConverterParaOBancoDeDados(Status status)
    {
        return status.ToString()[0..1];
    }

    private static Status ConverterParaAplicacao(string value)
    {
        var status = Enum.GetValues<Status>().FirstOrDefault(s => s.ToString().StartsWith(value));
        return status;
    }
}