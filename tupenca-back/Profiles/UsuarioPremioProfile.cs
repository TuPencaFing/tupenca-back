using System;
using AutoMapper;
using tupenca_back.Controllers.Dto;
using tupenca_back.Model;

namespace tupenca_back.Profiles
{
	public class UsuarioPremioProfile : Profile
    {
		public UsuarioPremioProfile()
		{
            CreateMap<UsuarioPremio, UsuarioPremioDto>();
            CreateMap<UsuarioPremioDto, UsuarioPremio>();
        }
	}
}

