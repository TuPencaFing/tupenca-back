using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tupenca_back.Model
{
    public class Equipo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [InverseProperty("EquipoLocal")]
        public ICollection<Evento>? EventosLocal { get; set; }

        [InverseProperty("EquipoVisitante")]
        public ICollection<Evento>? EventosVisitante { get; set; }

    }
}
