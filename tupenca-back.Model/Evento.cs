using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tupenca_back.Model
{
    public class Evento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime FechaInicial { get; set; }

        [Required]
        public DateTime FechaFinal { get; set; }

        public int EquipoLocalId { get; set; }
        [ForeignKey("EquipoLocalId")]
        public virtual Equipo? EquipoLocal { get; set; }

        public int EquipoVisitanteId { get; set; }
        [ForeignKey("EquipoVisitanteId")]
        public virtual Equipo? EquipoVisitante { get; set; }
    }
}
