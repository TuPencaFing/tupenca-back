using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IForoRepository : IRepository<Foro>
    {

        IEnumerable<Foro> getMessagesByPenca(int pencaId);

        void Save();
    }
}
