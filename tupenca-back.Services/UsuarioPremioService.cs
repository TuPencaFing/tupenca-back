using System;
using tupenca_back.DataAccess.Repository;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;
using tupenca_back.Services.Exceptions;

namespace tupenca_back.Services
{
	public class UsuarioPremioService
	{
		private readonly IUsuarioPremioRepository _usuarioPremioRepository;

        public UsuarioPremioService(IUsuarioPremioRepository usuarioPremioRepository)
        {
            _usuarioPremioRepository = usuarioPremioRepository;
        }

        public IEnumerable<UsuarioPremio> GetUsuariosPremio(int idUsuario, int idPenca)
        {
            if (idUsuario != 0 && idPenca != 0)
            {
                var usuarioPremio = GetUsuarioPremioByUsuarioAndPenca(idUsuario, idPenca);
                var res = new List<UsuarioPremio>();

                if (usuarioPremio != null)
                    res.Add(usuarioPremio);

                return res;
            }
                
            else if (idUsuario != 0)
                return GetUsuariosPremioByUsuario(idUsuario);

            else if (idPenca != 0)
                return GetUsuariosPremioByPenca(idPenca);

            else return _usuarioPremioRepository.GetAll();
        }

        public UsuarioPremio? GetUsuarioPremioById(int id) => _usuarioPremioRepository.GetFirstOrDefault(up => up.Id == id);

        public IEnumerable<UsuarioPremio> GetUsuariosPremioByUsuario(int idUsuario) => _usuarioPremioRepository.FindByCondition(up => up.IdUsuario == idUsuario);

        public IEnumerable<UsuarioPremio> GetUsuariosPremioByPenca(int idPenca) => _usuarioPremioRepository.FindByCondition(up => up.IdPenca == idPenca);

        public UsuarioPremio? GetUsuarioPremioByUsuarioAndPenca(int idUsuario, int idPenca) => _usuarioPremioRepository.GetFirstOrDefault(up => up.IdUsuario == idUsuario && up.IdPenca == idPenca);


        public void UpdateUsuarioPremio(UsuarioPremio usuarioPremio)
        {
            if (usuarioPremio != null)
            {
                _usuarioPremioRepository.Update(usuarioPremio);
                _usuarioPremioRepository.Save();
            }
        }

        public void RemoveUsuarioPremio(UsuarioPremio usuarioPremio)
        {
            if (usuarioPremio != null)
            {
                _usuarioPremioRepository.Remove(usuarioPremio);
                _usuarioPremioRepository.Save();
            }
        }

        public UsuarioPremio? AddDatosFacturacion(int id, UsuarioPremio usuarioPremio)
        {
            var usuarioPremioToUpate = GetUsuarioPremioById(id);

            if (usuarioPremioToUpate == null)
            {
                throw new NotFoundException("El premio del usuario no existe");
            }

            usuarioPremioToUpate.Banco = usuarioPremio.Banco;
            usuarioPremioToUpate.CuentaBancaria = usuarioPremio.CuentaBancaria;

            UpdateUsuarioPremio(usuarioPremioToUpate);

            return usuarioPremioToUpate;
        }

        public UsuarioPremio? ReclamarPremio(int id)
        {
            var usuarioPremioToUpate = GetUsuarioPremioById(id);

            if (usuarioPremioToUpate == null)
            {
                throw new NotFoundException("El premio del usuario no existe");
            }

            usuarioPremioToUpate.Reclamado = true;

            UpdateUsuarioPremio(usuarioPremioToUpate);

            return usuarioPremioToUpate;
        }

        public UsuarioPremio? PagarPremio(int id)
        {
            var usuarioPremioToUpate = GetUsuarioPremioById(id);

            if (usuarioPremioToUpate == null)
            {
                throw new NotFoundException("El premio del usuario no existe");
            }

            usuarioPremioToUpate.PendientePago = false;

            UpdateUsuarioPremio(usuarioPremioToUpate);

            return usuarioPremioToUpate;
        }

    }
}

