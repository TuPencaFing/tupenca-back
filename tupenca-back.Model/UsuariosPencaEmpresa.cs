using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace tupenca_back.Model
{
    public class UsuariosPencaEmpresa
    {
        public int Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public bool habilitado { get; set; }

    }
}
