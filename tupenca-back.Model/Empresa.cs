using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Model
{
    public class Empresa
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Razon Social")]
        public string Razonsocial { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*", ErrorMessage = "Debe contener unicamente numeros.")]
        [MaxLength(12)]
        [MinLength(12)]
        public string RUT { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime FechaCreacion { get; set; }

    }
}
