using System;
using AutoMapper;
using tupenca_back.Controllers.Dto;
using tupenca_back.Model;

namespace tupenca_back.Profiles
{
    public class EquipoProfile : Profile
    {
        public EquipoProfile()
        {
            CreateMap<EquipoDto, Equipo>();
            CreateMap<Equipo, EquipoDto> ();
        }
    }
}

