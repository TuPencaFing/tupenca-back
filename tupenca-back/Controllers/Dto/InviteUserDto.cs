using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tupenca_back.Model
{
    public class InviteUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int PencaId { get; set; }
    }
}
