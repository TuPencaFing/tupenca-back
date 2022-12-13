using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace tupenca_back.Model
{
    public class Usuario : Persona
    {
        [JsonIgnore]
        public List<Empresa>? Empresas { get; set; }

        [JsonIgnore]
        public List<UsuarioPenca>? UsuariosPencas { get; set; }

    }
}
