using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tupenca_back.Model
{
    public class PersonaResetPassword
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Token { get; set; }

        [Required]
        public Persona? Persona { get; set; }
        public int PersonaId { get; set; }

    }
}
