using System;
using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Model
{
    public class Premio
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int Position { get; set; }

        [Required]
        public decimal Percentage { get; set; }

    }
}

