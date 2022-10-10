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

                
        [Required]
        public int EquipoLocalId { get; set; }
        [Required]
        [ForeignKey("EquipoLocalId")]
        public Equipo EquipoLocal { get; set; }


        [ForeignKey("EquipoVisitanteId")]
        [Required]
        public int EquipoVisitanteId { get; set; }
        [Required]
        public Equipo EquipoVisitante { get; set; }

        public int ResultadoId { get; set; }
        public Resultado Resultado { get; set; }

    }
}
