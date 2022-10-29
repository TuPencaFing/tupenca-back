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
        public Usuario Usuario { get; set; }
        public int PencaId { get; set; }
        public Penca Penca { get; set; }

        public int score { get; set; }
        public bool habilitado { get; set; }
    }
}
