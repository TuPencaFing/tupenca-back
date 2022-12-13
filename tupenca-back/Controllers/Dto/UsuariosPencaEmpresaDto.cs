using System.ComponentModel.DataAnnotations;


namespace tupenca_back.Controllers.Dto
{
    public class UsuariosPencaEmpresaDto
    {
        public int Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public bool habilitado { get; set; }
    }
}
