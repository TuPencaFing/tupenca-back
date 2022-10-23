using System;
using AutoMapper;
using tupenca_back.Controllers.Dto;
using tupenca_back.Model;

namespace tupenca_back.Profiles
{
    public class EventoProfile : Profile
    {
        public EventoProfile()
        {
            CreateMap<Evento, EventoDto>();
            CreateMap<EventoDto, Evento>();
        }
    }
}

