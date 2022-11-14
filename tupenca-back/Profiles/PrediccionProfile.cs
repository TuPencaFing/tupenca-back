using System;
using AutoMapper;
using tupenca_back.Controllers.Dto;
using tupenca_back.Model;

namespace tupenca_back.Profiles
{
    public class PrediccionProfile : Profile
    {
        public PrediccionProfile()
        {
            CreateMap<Prediccion, PrediccionDto>();
            CreateMap<PrediccionDto, Prediccion>();
        }
    }
}

