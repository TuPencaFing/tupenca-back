using System;
using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Controllers.Dto
{
    public class PremioDto
    {

        public int Id { get; set; }

        public int Position { get; set; }

        public decimal Percentage { get; set; }

    }
}

