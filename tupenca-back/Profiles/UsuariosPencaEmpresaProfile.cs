using System;
using AutoMapper;
using tupenca_back.Controllers.Dto;
using tupenca_back.Model;

namespace tupenca_back.Profiles
{
    public class UsuariosPencaEmpresaProfile : Profile
    {
        public UsuariosPencaEmpresaProfile()
        {
            CreateMap<UsuariosPencaEmpresa, UsuariosPencaEmpresaDto>();
            CreateMap<UsuariosPencaEmpresaDto, UsuariosPencaEmpresa>();
        }
    }
}

