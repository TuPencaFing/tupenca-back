using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.Services
{
    public class UserService
    {
        private readonly IUserRepository _db;
        public UserService(IUserRepository db)
        {
            _db = db;
        }
        public void deleteUser(User usr)
        {
            if (usr != null)
            {
                _db.Remove(usr);
                _db.Save();
            }
        }
        public User? findUser(int? id)
        {
            return _db.GetFirstOrDefault(u=> u.Id == id);
        }
        public IEnumerable<User> getUsers()
        {

            return _db.GetAll();
        }
        public void editUser(User usr)
        {
            if (usr != null)
            {
                _db.Update(usr);
                _db.Save();
            }
        }
        public void addUser(User usr)
        {
            if (usr != null)
            {
                _db.Add(usr);
                _db.Save();
            }
        }

    }
}
