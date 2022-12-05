using System;
using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;

namespace tupenca_back.Controllers.Dto
{
	public class PuntajeUsuarioPencaDto
	{

        public int Id { get; set; }

        public int PencaId { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioDto Usuario { get; set; }

        public int Score { get; set; }

    }
}

