using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IPrediccionRepository : IRepository<Prediccion>
    {

        public Prediccion GetPrediccionByUsuarioEvento(int usuarioId, int eventoId, int pencaId);

        public void UpdateScore(int eventoId, Resultado resultado);

        public IEnumerable<UsuarioScore> GetUsuariosByPenca(int id);

        void Save();
    }
}
