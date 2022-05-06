using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        public void ListAllUsers();
        public void GetUserById(string id);
        public void CreateUser();
        public void UpdateUser(string id);
        public void DeleteUser(string id);
    }
}
