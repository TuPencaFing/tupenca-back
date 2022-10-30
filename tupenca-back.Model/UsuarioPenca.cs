using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tupenca_back.Model
{
    public class UsuarioPenca
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }
        public int PencaId { get; set; }
        public virtual Penca? Penca { get; set; }

        public int score { get; set; } = 0;
        public bool habilitado { get; set; } = false;
    }
}
