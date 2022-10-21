using System;
using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Model
{
    public class PencaCompartida : Penca
    {

        [Required]
        public decimal CostEntry { get; set; }

        [Required]
        public decimal Pozo { get; set; }

        [Required]
        public decimal Commission { get; set; }

    }
}

