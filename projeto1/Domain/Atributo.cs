using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace projeto1.Domain
{
    [Table("TabelaAtributos")]
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