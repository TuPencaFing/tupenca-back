using System;
using AutoMapper;
using tupenca_back.Controllers.Dto;
using tupenca_back.Model;

namespace tupenca_back.Profiles
{
    public class CampeonatoProfile : Profile
    {
        public CampeonatoProfile()
        {
            CreateMap<Campeonato, CampeonatoDto>();
            CreateMap<CampeonatoDto, Campeonato>();
        }
    }
}

