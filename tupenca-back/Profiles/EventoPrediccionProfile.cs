using System;
using AutoMapper;
using tupenca_back.Controllers.Dto;
using tupenca_back.Model;

namespace tupenca_back.Profiles
{
    public class EventoPrediccionProfile : Profile
    {
        public EventoPrediccionProfile()
        {
            CreateMap<EventoPrediccion, EventoPrediccionDto>();
            CreateMap<EventoPrediccionDto, EventoPrediccion>();
        }
    }
}

