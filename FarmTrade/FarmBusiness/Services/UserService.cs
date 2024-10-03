using FarmTradeDataLayer.Repository;
using FarmTradeEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FarmBusiness.Services
{
    public class UserService
    {
        IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // CRUD Service Operations for User:
        public void UpdateUser(User user)
        {
            _userRepository.Updateuser(user);
        }
        public void AddUser(User user)
        {
            _userRepository.Adduser(user);
        }
        public string Login(User user)
        {
            return _userRepository.Login(user);
        }
        public User GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.Getusers();
        }
        public void DeleteUser(int userId)
        {
            _userRepository.DeleteUser(userId);
        }
    }
}
