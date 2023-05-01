using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace projeto1.Domain
{
    [NotMapped]
    public class NaoMapeada
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class Mapeada
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [NotMapped]
        public string? ColunaNaoMapeada { get; set; }
    }
}