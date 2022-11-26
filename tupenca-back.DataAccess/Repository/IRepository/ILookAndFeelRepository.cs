using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface ILookAndFeelRepository : IRepository<LookAndFeel>
    {
        void Save();
    }
}
