using System;
using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;

namespace tupenca_back.Controllers.Dto
{
    public class PremioDto
    {

        public int Id { get; set; }

        public string? Image { get; set; }

        public int Position { get; set; }

        public decimal Percentage { get; set; }

    }
}

