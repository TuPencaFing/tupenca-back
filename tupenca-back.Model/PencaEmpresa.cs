using System;
using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Model
{
    public class PencaEmpresa : Penca
    {

        [Required]
        public Empresa? Empresa { get; set; }

        [Required]
        public Plan? Plan { get; set; }


    }
}

