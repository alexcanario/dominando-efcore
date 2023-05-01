using Microsoft.EntityFrameworkCore;

namespace projeto1.Domain
{
    [Keyless]
    public class RelatorioFinanceiro
    {
        public DateTime Data { get; set; }
        public string? Descricao { get; set; }
        public decimal Resultado { get; set; }
    }
}