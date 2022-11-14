using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;

namespace tupenca_back.Controllers.Dto
{
    public class EmpresaCountDto
    {

        public IEnumerable<Empresa>? Empresas { get; set; }

        public int CantEmpresas { get; set; }

    }
}
