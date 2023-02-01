namespace projeto1.Domain
{
    public class Funcionario
    {
        public Funcionario(string nome, string cpf, int rg)
        {
            Nome = nome;
            Cpf = cpf;
            Rg = rg;
        }

        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public int Rg { get; set; }

        // public int DepartamentoId { get; set; }
        public virtual Departamento? Departamento { get; set; }
    }
}