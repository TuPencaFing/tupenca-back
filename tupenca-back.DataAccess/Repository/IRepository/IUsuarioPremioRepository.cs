using System;
using tupenca_back.DataAccess.Migrations;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
	public interface IUsuarioPremioRepository : IRepository<UsuarioPremio>
    {

        void Save();
        UsuarioPremio? GetUsuarioPremioById(int id);
        UsuarioPremio? GetUsuarioPremioByUsuarioAndPenca(int idUsuario, int idPenca);
    }
}

