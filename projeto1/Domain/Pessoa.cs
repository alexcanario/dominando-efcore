using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto1.Domain;

public abstract class Pessoa
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Cpf { get; set; }
    public string? Nome { get; set; }
}