using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace projeto1.Domain
{
    public class Aeroporto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }

        [InverseProperty("AeroportoPartida")]
        public ICollection<Voo>? VoosQuePartiram { get; set; }

        [InverseProperty("AeroportoChegada")]
        public ICollection<Voo>? VoosQueChegaram { get; set; }
    }
}