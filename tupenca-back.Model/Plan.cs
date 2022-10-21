using System;
using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Model
{
    public class Plan
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int CantUser { get; set; }

        [Required]
        public decimal PercentageCost { get; set; }

        [Required]
        public int LookAndFeel { get; set; }

    }
}

