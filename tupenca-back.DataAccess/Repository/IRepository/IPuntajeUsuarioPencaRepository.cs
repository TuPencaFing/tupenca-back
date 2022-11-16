using System;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
	public interface IPuntajeUsuarioPencaRepository : IRepository<PuntajeUsuarioPenca>
    {

        IEnumerable<PuntajeUsuarioPenca> GetAllByPenca(int pencaId);

        IEnumerable<PuntajeUsuarioPenca> GetAllByUsuario(int usuarioId);

        void Save();

    }
}

