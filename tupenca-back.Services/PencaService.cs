using System;
using Microsoft.Extensions.Logging;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;
using tupenca_back.Services.Exceptions;

namespace tupenca_back.Services
{
    public class PencaService
    {

        private readonly ILogger<PencaService> _logger;
        private readonly IPencaCompartidaRepository _pencaCompartidaRepository;
        private readonly IPencaEmpresaRepository _pencaEmpresaRepository;
        private readonly CampeonatoService _campeonatoService;

        public PencaService(ILogger<PencaService> logger,
                            IPencaCompartidaRepository pencaCompartidaRepository,
                            IPencaEmpresaRepository pencaEmpresaRepository,
                            CampeonatoService campeonatoService)
        {
            _logger = logger;
            _pencaCompartidaRepository = pencaCompartidaRepository;
            _pencaEmpresaRepository = pencaEmpresaRepository;
            _campeonatoService = campeonatoService;
        }


        public IEnumerable<PencaCompartida> GetPencaCompartidas() => _pencaCompartidaRepository.GetPencaCompartidas();

        public IEnumerable<PencaEmpresa> GetPencaEmpresas() => _pencaEmpresaRepository.GetPencaEmpresas();

        public PencaCompartida? findPencaCompartidaById(int? id) => _pencaCompartidaRepository.GetFirstOrDefault(p => p.Id == id);

        public PencaEmpresa? findPencaEmpresaById(int? id) => _pencaEmpresaRepository.GetFirstOrDefault(p => p.Id == id);


        public void AddPencaCompartida(PencaCompartida pencaCompartida)
        {

            //var campeonato = _campeonatoService.findCampeonatoById(pencaCompartida.Campeonato.Id);

            //if (campeonato == null)
            //{
            //    throw new NotFoundException("El Campeonato no existe");
            //}

            _pencaCompartidaRepository.Add(pencaCompartida);
            _pencaCompartidaRepository.Save();
        }
    }
}

