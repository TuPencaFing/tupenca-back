using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace tupenca_back.Model
{
    public class PencaCompartida : Penca
    {

        [Required]
        [Precision(18, 2)]
        public decimal CostEntry { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal Pozo { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal Commission { get; set; }

    }
}

