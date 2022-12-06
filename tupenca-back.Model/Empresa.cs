using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Model
{
    public class Empresa
    {
        public int Id { get; set; }

        public string? Image { get; set; }

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

        [Required]
        public int PlanId { get; set; }
        public virtual Plan? Plan { get; set; }

        [Required]
        [RegularExpression(@"^[0-9a-zA-Z]+$", ErrorMessage = "Debe contener unicamente letras o numeros")]
        public string TenantCode { get; set; }

        public bool Habilitado { get; set; } = false;

        public List<Funcionario> Funcionarios { get; set; }

        public List<Usuario> Usuarios { get; set; }

        public List<PencaEmpresa> Pencas { get; set; }
    }
}
