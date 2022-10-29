﻿using System;
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
        public List<Prediccion> Predicciones { get; set; }

        public List<Empresa> Empresas { get; set; }

        public List<UsuarioPenca> UsuariosPencas { get; set; }

    }
}
