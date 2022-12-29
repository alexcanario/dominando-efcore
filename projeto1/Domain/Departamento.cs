namespace projeto1.Domain
{
    public class Departamento
    {
        public Departamento(string? descricao)
        {
            Descricao = descricao;
            Ativo = true;
        }

        public int Id { get; set; }
        public string? Descricao { get; set; }
        public bool Ativo { get; set; }

        public List<Funcionario>? Funcionarios { get; set; }
    }
}