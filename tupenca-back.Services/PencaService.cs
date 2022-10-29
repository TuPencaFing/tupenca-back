﻿using System;
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
        private readonly PremioService _premioService;
        private readonly EmpresaService _empresaService;
        private readonly PlanService _planService;

        public PencaService(ILogger<PencaService> logger,
                            IPencaCompartidaRepository pencaCompartidaRepository,
                            IPencaEmpresaRepository pencaEmpresaRepository,
                            CampeonatoService campeonatoService,
                            PremioService premioService,
                            EmpresaService empresaService,
                            PlanService planService)
        {
            _logger = logger;
            _pencaCompartidaRepository = pencaCompartidaRepository;
            _pencaEmpresaRepository = pencaEmpresaRepository;
            _campeonatoService = campeonatoService;
            _premioService = premioService;
            _empresaService = empresaService;
            _planService = planService;
        }


        public IEnumerable<PencaCompartida> GetPencaCompartidas() => _pencaCompartidaRepository.GetPencaCompartidas();

        public IEnumerable<PencaEmpresa> GetPencaEmpresas() => _pencaEmpresaRepository.GetPencaEmpresas();

        public PencaCompartida? findPencaCompartidaById(int? id) => _pencaCompartidaRepository.GetFirstOrDefault(p => p.Id == id);

        public PencaEmpresa? findPencaEmpresaById(int? id) => _pencaEmpresaRepository.GetFirstOrDefault(p => p.Id == id);

        public void AddPencaCompartida(PencaCompartida pencaCompartida)
        {

            var campeonato = _campeonatoService.findCampeonatoById(pencaCompartida.Campeonato.Id);

            if (campeonato == null)
            {
                throw new NotFoundException("El Campeonato no existe");
            }

            pencaCompartida.Campeonato = campeonato;

            var premios = new List<Premio>();

            foreach (Premio premio in pencaCompartida.Premios)
            {
                var premioToAdd = _premioService.FindPremioById(premio.Id);

                if (premioToAdd == null)
                {
                    throw new NotFoundException("El Premio no existe");
                }

                premios.Add(premioToAdd);
            }

            pencaCompartida.Premios = premios;


            _pencaCompartidaRepository.Add(pencaCompartida);
            _pencaCompartidaRepository.Save();
        }

        public void AddPencaEmpresa(PencaEmpresa pencaEmpresa)
        {

            var campeonato = _campeonatoService.findCampeonatoById(pencaEmpresa.Campeonato.Id);

            if (campeonato == null)
            {
                throw new NotFoundException("El Campeonato no existe");
            }

            pencaEmpresa.Campeonato = campeonato;

            var premios = new List<Premio>();

            foreach (Premio premio in pencaEmpresa.Premios)
            {
                var premioToAdd = _premioService.FindPremioById(premio.Id);

                if (premioToAdd == null)
                    throw new NotFoundException("El Premio no existe");

                premios.Add(premioToAdd);
            }

            pencaEmpresa.Premios = premios;


            var empresa = _empresaService.getEmpresaById(pencaEmpresa.Empresa.Id);

            if (empresa == null)
                throw new NotFoundException("La Empresa no existe");

            pencaEmpresa.Empresa = empresa;


            var plan = _planService.FindPlanById(pencaEmpresa.Plan.Id);

            if (plan == null)
                throw new NotFoundException("El Plan no existe");

            pencaEmpresa.Plan = plan;


            _pencaEmpresaRepository.Add(pencaEmpresa);
            _pencaEmpresaRepository.Save();
        }

        public void UpdatePencaCompartida(int id, PencaCompartida pencaCompartida)
        {
            var pencaToUpdate = findPencaCompartidaById(id);

            if (pencaToUpdate == null)
                throw new NotFoundException("La Penca no existe");

            pencaToUpdate.Title = pencaCompartida.Title;
            pencaToUpdate.Description = pencaCompartida.Description;
            pencaToUpdate.Image = pencaCompartida.Image;
            pencaToUpdate.CostEntry = pencaCompartida.CostEntry;
            pencaToUpdate.Commission = pencaCompartida.Commission;

            _pencaCompartidaRepository.Update(pencaToUpdate);
            _pencaCompartidaRepository.Save();
        }

        public void UpdatePencaEmpresa(int id, PencaEmpresa pencaEmpresa)
        {
            var pencaToUpdate = findPencaEmpresaById(id);

            if (pencaToUpdate == null)
                throw new NotFoundException("La Penca no existe");

            pencaToUpdate.Title = pencaEmpresa.Title;
            pencaToUpdate.Description = pencaEmpresa.Description;
            pencaToUpdate.Image = pencaEmpresa.Image;
  
            _pencaEmpresaRepository.Update(pencaToUpdate);
            _pencaEmpresaRepository.Save();
        }

        public void RemovePencaCompartida(int id)
        {
            var penca = findPencaCompartidaById(id);

            if (penca == null)
                throw new NotFoundException("La Penca no existe");

            _pencaCompartidaRepository.Remove(penca);
            _pencaCompartidaRepository.Save();
        }

        public void RemovePencaEmpresa(int id)
        {
            var penca = findPencaEmpresaById(id);

            if (penca == null)
                throw new NotFoundException("La Penca no existe");

            _pencaEmpresaRepository.Remove(penca);
            _pencaEmpresaRepository.Save();
        }

        public bool PencaCompartidaExists(int id)
        {
            return findPencaCompartidaById(id) == null;
        }

        public bool PencaEmpresaExists(int id)
        {
            return findPencaEmpresaById(id) == null;
        }
    }
}
