using System;
using AutoMapper;
using tupenca_back.Controllers.Dto;
using tupenca_back.Model;

namespace tupenca_back.Profiles
{
    public class PremioProfile : Profile
    {
        public PremioProfile()
        {
            CreateMap<Premio, PremioDto>();
            CreateMap<PremioDto, Premio>();
        }
    }
}

