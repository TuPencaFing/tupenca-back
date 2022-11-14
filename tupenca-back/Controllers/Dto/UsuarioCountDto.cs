using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;

namespace tupenca_back.Controllers.Dto
{
    public class UsuarioCountDto
    {

        public IEnumerable<Usuario>? Usuarios { get; set; }

        public int CantUsuarios { get; set; }

    }
}
