using System;
using AutoMapper;
using tupenca_back.Controllers.Dto;
using tupenca_back.Model;

namespace tupenca_back.Profiles
{
	public class PuntajeUsuarioPencaProfile : Profile
	{
		public PuntajeUsuarioPencaProfile()
		{
            CreateMap<PuntajeUsuarioPenca, PuntajeUsuarioPencaDto>();
            CreateMap<PuntajeUsuarioPencaDto, PuntajeUsuarioPenca>();
        }
	}
}

