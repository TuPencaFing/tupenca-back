using System.ComponentModel.DataAnnotations.Schema;
using tupenca_back.Model;


namespace tupenca_back.Services.Dto
{
	public class EventoResultado
	{

        public int Id { get; set; }

        public string? Image { get; set; }

        public DateTime FechaInicial { get; set; }

        public virtual Equipo? EquipoLocal { get; set; }

        public virtual Equipo? EquipoVisitante { get; set; }

        public bool IsEmpateValid { get; set; } = true;

        public bool IsPuntajeEquipoValid { get; set; } = true;

        public bool tieneResultado { get; set; } = false;
    }
}

