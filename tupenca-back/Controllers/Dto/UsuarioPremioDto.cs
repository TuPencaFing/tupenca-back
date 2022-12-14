using System;
using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;

namespace tupenca_back.Controllers.Dto
{
	public class UsuarioPremioDto
	{
        public int Id { get; set; }

        public decimal Premio { get; set; }

        public string? Banco { get; set; }

        public string? CuentaBancaria { get; set; }

        public bool PendientePago { get; set; }

        public bool Reclamado { get; set; }

        public int IdUsuario { get; set; }

        public int PencaId { get; set; }

        public Penca? Penca { get; set; }

    }
}

