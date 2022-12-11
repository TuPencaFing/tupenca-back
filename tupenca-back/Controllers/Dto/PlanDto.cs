using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Controllers.Dto
{
    public class PlanDto
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int CantUser { get; set; }

        public decimal PercentageCost { get; set; }

        public int LookAndFeel { get; set; }

        public int CantPencas { get; set; }

        public int Cost { get; set; }

    }
}

