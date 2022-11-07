using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tupenca_back.Model
{
    public class UsuarioScore
    {
        public int ID { get; set; }

        public string? UserName { get; set; }

        public int? TotalScore { get; set; } = 0;
    }
}
