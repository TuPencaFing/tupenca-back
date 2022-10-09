using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void Save();
    }
}
