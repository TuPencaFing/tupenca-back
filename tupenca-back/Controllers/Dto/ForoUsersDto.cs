using System;
using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;

namespace tupenca_back.Controllers.Dto
{
    public class ForoUsersDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Message { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public string UserName { get; set; }

        public string? Image { get; set; }

        [Required]
        public int PencaId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Creacion { get; set; }

    }
}

