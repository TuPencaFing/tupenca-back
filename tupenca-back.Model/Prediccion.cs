using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Model
{
    public class Prediccion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public TipoResultado prediccion { get; set; }

        public int? PuntajeEquipoLocal { get; set; }

        public int? PuntajeEquipoVisitante { get; set; }

        [Required]
        public int EventoId { get; set; }

    }
}
