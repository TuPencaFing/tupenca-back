using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace tupenca_back.Model
{
	public class UsuarioPremio
	{
        [Key]
        public int Id { get; set; }

        [Required]
        [Precision(18, 2)]
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
        public int PencaId { get; set; }
        public virtual Penca? Penca { get; set; }

    }
}

