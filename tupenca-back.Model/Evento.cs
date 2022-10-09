using System.ComponentModel.DataAnnotations;

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
        public Equipo EquipoLocal { get; set; }

        [Required]
        public Equipo EquipoVisitante { get; set; }

        public Resultado Resultado { get; set; }

    }
}
