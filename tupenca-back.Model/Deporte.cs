using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tupenca_back.Model
{
    public class Deporte
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

    }
}
