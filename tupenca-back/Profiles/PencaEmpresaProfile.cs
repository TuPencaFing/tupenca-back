using System;
using AutoMapper;
using tupenca_back.Controllers.Dto;
using tupenca_back.Model;

namespace tupenca_back.Profiles
{
    public class PencaEmpresaProfile : Profile
    {
        public PencaEmpresaProfile()
        {
            CreateMap<PencaEmpresa, PencaEmpresaDto>();
            CreateMap<PencaEmpresaDto, PencaEmpresa>();
        }
    }
}

