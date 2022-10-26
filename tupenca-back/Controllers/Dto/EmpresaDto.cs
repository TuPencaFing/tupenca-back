using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Controllers.Dto
{
    public class EmpresaDto
    {

        public int Id { get; set; }

        [Display(Name = "Razon Social")]
        public string? Razonsocial { get; set; }

        [RegularExpression(@"^[0-9]*", ErrorMessage = "Debe contener unicamente numeros.")]
        [MaxLength(12)]
        [MinLength(12)]
        public string? RUT { get; set; }
    }
}
