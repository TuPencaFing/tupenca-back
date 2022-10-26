using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;

namespace tupenca_back.Controllers.Dto
{
    public class ResultadoDto
    {
        public TipoResultado resultado { get; set; }

        public int? PuntajeEquipoLocal { get; set; }

        public int? PuntajeEquipoVisitante { get; set; }

        public int EventoId { get; set; }

        public int UsuarioId { get; set; }
    }
}
