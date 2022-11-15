using System;
using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Model
{
	public class PuntajeUsuarioPenca
	{

        [Key]
        public int Id { get; set; }

        [Required]
        public int PencaId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public int? Score { get; set; } = 0;
    }
}

