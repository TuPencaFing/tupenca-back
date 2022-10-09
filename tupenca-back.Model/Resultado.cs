

using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Model
{
    public enum TipoResultado
    {
        Empate,
        VictoriaEquipoLocal,
        VictoriaEquipoVisitante
    }

    public class Resultado
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public TipoResultado resultado { get; set; }

        public int PuntajeEquipoLocal { get; set; }

        public int PuntajeEquipoVisitante { get; set; }

    }
}
