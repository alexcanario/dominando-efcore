using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projeto1.Domain
{
    public class Voo
    {
        public int Id { get; set; }
        public string? Descricao { get; set; }

        public Aeroporto? AeroportoPartida { get; set; }
        public Aeroporto? AeroportoChegada { get; set; }
    }
}