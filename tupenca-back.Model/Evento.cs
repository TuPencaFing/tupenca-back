using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tupenca_back.Model
{
    public class Evento
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public DateTime FechaInicial { get; set; }

        [Required]
        public DateTime FechaFinal { get; set; }

        [ForeignKey("Equipo")]
        public int EquipoLocalID { get; set; }

        [ForeignKey("Equipo")]
        public int EquipoVisitanteID { get; set; }

        public int ResultadoID { get; set; }

        [Required]
        public Equipo? EquipoLocal { get; set; }

        [Required]
        public Equipo? EquipoVisitante { get; set; }

        public Resultado Resultado { get; set; }

    }
}
