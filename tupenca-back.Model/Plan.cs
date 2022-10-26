using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace tupenca_back.Model
{
    public class Plan
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int CantUser { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal PercentageCost { get; set; }

        [Required]
        public int LookAndFeel { get; set; }

    }
}

