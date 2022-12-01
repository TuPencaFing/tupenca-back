using System;
using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Model
{
	public class UsuarioPremio
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Premio { get; set; }

        public string? Banco { get; set; }

        public string? CuentaBancaria { get; set; }

        [Required]
        public bool PendientePago { get; set; }

        [Required]
        public bool Reclamado { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public int IdPenca { get; set; }

    }
}

