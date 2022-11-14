using System;
using AutoMapper;
using tupenca_back.Controllers.Dto;
using tupenca_back.Model;

namespace tupenca_back.Profiles
{
    public class ResultadoProfile : Profile
    {
        public ResultadoProfile()
        {
            CreateMap<Resultado, ResultadoDto>();
            CreateMap<ResultadoDto, Resultado>();
        }
    }
}

