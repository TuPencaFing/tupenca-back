﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository
{
    public class UsuarioPencaRepository : Repository<UsuarioPenca>, IUsuarioPencaRepository

    {
        private AppDbContext _appDbContext;
        public UsuarioPencaRepository(AppDbContext db) : base(db)
        {
            _appDbContext = db;
        }

        public IEnumerable<UsuarioPenca> GetUsuarioPencas(int id)
        {
            return _appDbContext.UsuariosPencas
                .Where(p => p.UsuarioId == id && p.habilitado == true)
                .Include(penca => penca.Penca)
                .Include(usuario => usuario.Usuario)
                .ToList();
        }

        public IEnumerable<PencaCompartida> GetUsuarioPencasCompartidas(int id)
        {
            return _appDbContext.UsuariosPencas
                .Where(p => p.UsuarioId == id && p.habilitado == true)
                .Select(p => p.Penca)
                .Join(_appDbContext.PencaCompartidas, penca => penca.Id, p => p.Id, (penca,p) => p)
                .ToList();

        }

        public IEnumerable<PencaCompartida> GetUsuarioPencasCompartidasNoJoined(int id)
        {
            return _appDbContext.PencaCompartidas
                .Where(pc => !_appDbContext.UsuariosPencas
                    .Any(p => p.Id == pc.Id)
                ).ToList();                           
        }

        public IEnumerable<PencaEmpresa> GetUsuarioPencasEmpresa(int id)
        {
            return _appDbContext.UsuariosPencas
                .Where(p => p.UsuarioId == id && p.habilitado == true)
                .Select(p => p.Penca)
                .Join(_appDbContext.PencaEmpresas, penca => penca.Id, p => p.Id, (penca, p) => p)
                .ToList();
        }


        public IEnumerable<Evento> GetEventosProximosPencasCompartidas(int id)
        {
            
            var today = DateTime.Now;
            return _appDbContext.UsuariosPencas
                .Where(p => p.UsuarioId == id && p.habilitado == true)
                .Select(p => p.Penca)
                .Join(_appDbContext.PencaCompartidas, penca => penca.Id, p => p.Id, (penca, p) => p)
                .SelectMany(p => p.Campeonato.Eventos)
                .Where(evento => evento.FechaInicial > today & evento.FechaInicial < today.AddDays(7))
                .OrderBy(evento => evento.FechaInicial)
                .Distinct()
                .Include(evento => evento.EquipoLocal)
                .Include(evento => evento.EquipoVisitante)
                .ToList();
        }


        public IEnumerable<Evento> GetEventosProximosPencas(int id)
        {
            var today = DateTime.Now;
            return _appDbContext.UsuariosPencas
                .Where(p => p.UsuarioId == id && p.habilitado == true)
                .Select(p => p.Penca)
                .SelectMany(p => p.Campeonato.Eventos)
                .Where(evento => evento.FechaInicial > today & evento.FechaInicial < today.AddDays(7))
                .OrderBy(evento => evento.FechaInicial)
                .Distinct()
                .Include(evento => evento.EquipoLocal)
                .Include(evento => evento.EquipoVisitante)
                .ToList();
        }


        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}