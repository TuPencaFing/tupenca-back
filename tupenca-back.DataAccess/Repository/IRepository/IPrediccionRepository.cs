using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IPrediccionRepository : IRepository<Prediccion>
    {

        public Prediccion GetPrediccionByUsuarioEvento(int usuarioId, int eventoId);

        void Save();
    }
}
