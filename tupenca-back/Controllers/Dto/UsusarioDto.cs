using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupenca_back.Controllers.Dto;

namespace tupenca_back.Model
{
    public class UsuarioDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Username must be between 6 and 40 character in length.")]
        public string UserName { get; set; }
        public string Image { get; set; }


        public List<EmpresaDto> Empresas { get; set; }


    }
}
