using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupenca_back.Controllers.Dto;

namespace tupenca_back.Model
{
    public class UsuarioScoreDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Username must be between 6 and 40 character in length.")]
        public string Username { get; set; }

        [Required]
        public int Score { get; set; }

    }
}
