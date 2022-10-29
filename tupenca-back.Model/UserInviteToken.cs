using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tupenca_back.Model
{
    public class UserInviteToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Token { get; set; }

        [Required]
        public int PencaId { get; set; }
    }
}
