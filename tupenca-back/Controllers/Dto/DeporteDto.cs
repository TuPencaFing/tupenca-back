using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Controllers.Dto
{
    public class DeporteDto
    {

        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? ImagenName { get; set; }
    }
}
