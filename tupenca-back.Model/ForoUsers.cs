using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace tupenca_back.Model
{
    public class ForoUsers
    {
        [Key]
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
