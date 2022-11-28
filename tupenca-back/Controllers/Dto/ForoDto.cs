using System;
using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;

namespace tupenca_back.Controllers.Dto
{
    public class ForoDto
    {

        [Required]
        public string? Message { get; set; }

        [Required]
        public int PencaId { get; set; }

    }
}

