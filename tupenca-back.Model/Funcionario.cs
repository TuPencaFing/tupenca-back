using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace tupenca_back.Model
{
    public class Funcionario : Persona
    {
        public Empresa Empresa { get; set; }

        public int EmpresaId { get; set; }
    }
}
