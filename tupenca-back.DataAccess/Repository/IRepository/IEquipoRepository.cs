using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IEquipoRepository : IRepository<Equipo>
    {
        IEnumerable<Equipo> GetEquipos();
        Equipo GetEquipoByID(int equipoId);
        Equipo GetEquipoByName(string nombreEquipo);
        void Create(Equipo obj);
        void Update(Equipo obj);
        void Delete(int equipoID);
    }
}
