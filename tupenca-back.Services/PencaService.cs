﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using tupenca_back.DataAccess;
using tupenca_back.DataAccess.Repository;
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
        private readonly IUsuarioPencaRepository _usuariopencaRepository;
        private readonly CampeonatoService _campeonatoService;
        private readonly PremioService _premioService;
        private readonly EmpresaService _empresaService;
        private readonly UsuarioService _usuarioService;
        private readonly PrediccionService _prediccionService;
        private readonly ImagesService _imagesService;


        public PencaService(ILogger<PencaService> logger,
                            IPencaCompartidaRepository pencaCompartidaRepository,
                            IPencaEmpresaRepository pencaEmpresaRepository,
                            IUsuarioPencaRepository usuariopencaRepository,
                            CampeonatoService campeonatoService,
                            PremioService premioService,
                            EmpresaService empresaService,
                            UsuarioService usuarioService,
                            PrediccionService prediccionService,
                            ImagesService imagesService,
                            IPersonaRepository personaRepository)
        {
            _logger = logger;
            _pencaCompartidaRepository = pencaCompartidaRepository;
            _pencaEmpresaRepository = pencaEmpresaRepository;
            _usuariopencaRepository = usuariopencaRepository;
            _campeonatoService = campeonatoService;
            _premioService = premioService;
            _empresaService = empresaService;
            _usuarioService = usuarioService;
            _prediccionService = prediccionService;
            _imagesService = imagesService;
        }


        public IEnumerable<PencaCompartida> GetPencaCompartidas() => _pencaCompartidaRepository.GetPencaCompartidas();

        public IEnumerable<PencaEmpresa> GetPencaEmpresas() => _pencaEmpresaRepository.GetPencaEmpresas();

        public IEnumerable<PencaEmpresa> GetPencaEmpresasByEmpresa(int empresaId) => _pencaEmpresaRepository.GetPencaEmpresasByEmpresa(empresaId);

        public PencaCompartida? findPencaCompartidaById(int? id) => _pencaCompartidaRepository.GetFirst(p => p.Id == id);

        public PencaEmpresa? findPencaEmpresaById(int? id) => _pencaEmpresaRepository.GetFirst(p => p.Id == id);

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

        public void AddPencaEmpresa(PencaEmpresa pencaEmpresa,int idEmpresa)
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


            var empresa = _empresaService.getEmpresaById(idEmpresa);

            if (empresa == null)
                throw new NotFoundException("La Empresa no existe");

            pencaEmpresa.Empresa = empresa;


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
            pencaToUpdate.PremiosEntregados = pencaCompartida.PremiosEntregados;

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
            pencaToUpdate.PremiosEntregados = pencaEmpresa.PremiosEntregados;

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

        public IEnumerable<PencaCompartida> GetPencasCompartidasByUsuario(int userId)
        {
            var usuario = _usuarioService.find(userId);
            if (usuario != null)
            {
                return _usuariopencaRepository.GetUsuarioPencasCompartidas(userId);
            }
            else throw new NotFoundException("Usuario no exsite");

        }

        public IEnumerable<PencaCompartida>? SearchPencasCompartidasByUsuario(int userId, string searchString)
        {
            var usuario = _usuarioService.find(userId);
            if (usuario != null)
            {
                return _usuariopencaRepository.SearchUsuarioPencasCompartidas(userId, searchString);
            }
            else throw new NotFoundException("Usuario no exsite");

        }

        public IEnumerable<PencaCompartida> GetPencasCompartidasNoJoinedByUsuario(int userId)
        {
            var usuario = _usuarioService.find(userId);
            if (usuario != null)
            {
                return _usuariopencaRepository.GetUsuarioPencasCompartidasNoJoined(userId);
            }
            else throw new NotFoundException("Usuario no exsite");

        }

        public IEnumerable<PencaCompartida>? SerchPencasCompartidasNoJoinedByUsuario(int userId, string searchString)
        {
            var usuario = _usuarioService.find(userId);
            if (usuario != null)
            {
                return _usuariopencaRepository.SearchUsuarioPencasCompartidasNoJoined(userId , searchString);
            }
            else throw new NotFoundException("Usuario no exsite");

        }

        public void AddUsuarioToPencaCompartida(int userId, int pencaId)
        {
            var usuario = _usuarioService.find(userId);
            if (usuario == null)
                throw new NotFoundException("El usuario no existe");

            var pencaToUpdate = findPencaCompartidaById(pencaId);

            if (pencaToUpdate == null)
                throw new NotFoundException("La Penca no existe");

            UsuarioPenca usuariopenca = new UsuarioPenca();
            usuariopenca.PencaId = pencaId;
            usuariopenca.UsuarioId = userId;
            usuariopenca.habilitado = false;
            _usuariopencaRepository.Add(usuariopenca);
            _usuariopencaRepository.Save();

            var costo = pencaToUpdate.CostEntry;
            var pozoactual = pencaToUpdate.Pozo;
            pencaToUpdate.Pozo = pozoactual + costo;
            _pencaCompartidaRepository.Update(pencaToUpdate);
            _pencaCompartidaRepository.Save();
        }

        public void AddUsuarioToPencaEmpresa(int userId, int pencaId)
        {
            var usuario = _usuarioService.find(userId);
            var pencaToUpdate = findPencaEmpresaById(pencaId);

            if (pencaToUpdate == null)
                throw new NotFoundException("La Penca no existe");

            UsuarioPenca usuariopenca = new UsuarioPenca();
            usuariopenca.PencaId = pencaId;
            usuariopenca.UsuarioId = userId;
            usuariopenca.habilitado = false;
            _usuariopencaRepository.Add(usuariopenca);
            _usuariopencaRepository.Save();
        }

        public int GetCantPencaEmpresas(int id)
        {
            return _pencaEmpresaRepository.GetCantPencaEmpresas(id);

        }


        public void SaveImagen(int id, IFormFile file, bool esPencaCompartida)
        {
            if (esPencaCompartida)
            {
                var penca = findPencaCompartidaById(id);

                string image = _imagesService.uploadImage(file.FileName, file.OpenReadStream());

                penca.Image = image;

                UpdatePencaCompartida(id, penca);
            }
            else
            {
                var penca = findPencaEmpresaById(id);

                string image = _imagesService.uploadImage(file.FileName, file.OpenReadStream());

                penca.Image = image;

                UpdatePencaEmpresa(id, penca);
            }

        }


        public void HabilitarUsuario (int pencaId, int usuarioId)
        {
            _usuariopencaRepository.HabilitarUsuario(pencaId, usuarioId);
        }

        public void RechazarUsuario(int pencaId, int usuarioId)
        {
            _usuariopencaRepository.RechazarUsuario(pencaId, usuarioId);
        }
        

        public IEnumerable<PencaEmpresa> GetPencasFromEmpresaByUsuario(string TenantCode, int usuarioId)
        {
            var usuario = _usuarioService.find(usuarioId);
            if (usuario != null)
            {
                return _usuariopencaRepository.GetUsuarioPencasEmpresa(TenantCode, usuarioId);
            }
            else throw new NotFoundException("Usuario no exsite");
        }


        public Penca? GetPencaById(int id)
        {
            var penca = findPencaCompartidaById(id);

            if (penca != null)
            {
                return penca;
            }
            else
            {
                return findPencaEmpresaById(id);
            }
        }


        public int CantPencasActivas()
        {

            int cantActivasEmpresa = _pencaEmpresaRepository.GetCantActivas();

            int catActivasCompartido = _pencaCompartidaRepository.GetCantActivas();

            return cantActivasEmpresa + catActivasCompartido;
        }


        public decimal GananciasPencasCompartidas()
        {
            decimal gananciaPorPencaCompartida = 0;

            var pencasCompartidas = GetPencaCompartidas();

            foreach (var pencaCompartida in pencasCompartidas)
            {
                decimal gananciaPenca = (pencaCompartida.Pozo * (pencaCompartida.Commission / 100));

                gananciaPorPencaCompartida += gananciaPenca;
            }


            return Math.Round(gananciaPorPencaCompartida, 2);
        }

        public IEnumerable<EventoPrediccion> GetInfoEventosByPencaUsuario(int PencaId, int userId)
        {
            return _pencaCompartidaRepository.GetInfoEventosByPencaUsuario(PencaId, userId);
        }

        public IEnumerable<EventoPrediccion> GetInfoEventosByPencaUsuarioFinalizados(int PencaId, int userId)
        {
            return _pencaCompartidaRepository.GetInfoEventosByPencaUsuarioFinalizados(PencaId, userId);
        }
        

        public IEnumerable<PencaCompartida> GetPencasHot()
        {
            return _pencaCompartidaRepository.GetPencasHot();
        }

        public bool chekAuthUserEmpresa(string TenantCode, int userId)
        {
            if (_usuarioService.find(userId) == null)
            {
                return false;
            }
            return _empresaService.chekAuthUserEmpresa(TenantCode, userId);
        }

        public IEnumerable<UsuariosPencaEmpresa>? GetUsuariosPencaEmpresa(int id)
        {
            return _usuariopencaRepository.GetUsuariosPencaEmpresa(id);
        }

    }
}

