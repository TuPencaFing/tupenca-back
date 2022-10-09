

using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Model
{
    public class Resultado
    {
        [Key]
        public int Id { get; set; }

        public enum TipoResultado
        {
            Empate,
            VictoriaEquipoLocal,
            VictoriaEquipoVisitante
        }

        [Required]
        public TipoResultado resultado { get; set; }

        public int PuntajeEquipoLocal { get; set; }

        public int PuntajeEquipoVisitante { get; set; }

    }
}
