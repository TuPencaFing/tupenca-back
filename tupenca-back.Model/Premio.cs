using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace tupenca_back.Model
{
    public class Premio
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int Position { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal Percentage { get; set; }

    }
}

