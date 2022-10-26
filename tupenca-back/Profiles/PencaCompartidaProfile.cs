using System;
using AutoMapper;
using tupenca_back.Controllers.Dto;
using tupenca_back.Model;

namespace tupenca_back.Profiles
{
    public class PencaCompartidaProfile : Profile
    {
        public PencaCompartidaProfile()
        {
            CreateMap<PencaCompartida, PencaCompartidaDto>();
            CreateMap<PencaCompartidaDto, PencaCompartida>();
        }
    }
}

