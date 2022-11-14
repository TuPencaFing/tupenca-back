using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace tupenca_back.Model
{
    public class Evento
    {
        [Key]
        public int Id { get; set; }

        public string? Image { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicial { get; set; }

        public int EquipoLocalId { get; set; }
        [ForeignKey("EquipoLocalId")]
        public virtual Equipo? EquipoLocal { get; set; }

        public int EquipoVisitanteId { get; set; }
        [ForeignKey("EquipoVisitanteId")]
        public virtual Equipo? EquipoVisitante { get; set; }

        [JsonIgnore]
        public List<Campeonato> Campeonatos { get; set; } = new List<Campeonato>();

        public bool IsEmpateValid { get; set; } = true;

        public bool IsPuntajeEquipoValid { get; set; } = true;

    }
}
