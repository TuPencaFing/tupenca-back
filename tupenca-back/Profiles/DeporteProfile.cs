using System;
using AutoMapper;
using tupenca_back.Controllers.Dto;
using tupenca_back.Model;

namespace tupenca_back.Profiles
{
    public class DeporteProfile : Profile
    {
        public DeporteProfile()
        {
            CreateMap<Deporte, DeporteDto>();
            CreateMap<DeporteDto, Deporte>();
        }
    }
}

