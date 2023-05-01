using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace projeto1.Domain
{
    [Table("TabelaAtributos")]
    [Index(nameof(Descricao), IsUnique = true)]
    [Index(nameof(Campo1), nameof(Campo2), IsUnique = true)]

    public class Atributo
    {
        [Key]
        public int iD { get; set; }

        [Column("MinhaDescricao", TypeName = "NVARCHAR(50)")]
        public string? Descricao { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Observacao { get; set; }

        public int Campo1 { get; set; }
        public int Campo2 { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int SomaCampo1Campo2 { get; set; }
    }
}