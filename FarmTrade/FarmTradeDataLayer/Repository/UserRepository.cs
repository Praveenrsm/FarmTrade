using FarmTradeEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FarmTradeDataLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        FarmContext _farmcontext;
        public UserRepository(FarmContext context)
        {
            _farmcontext = context;
        }

        public void Adduser(User user)
        {
            #region ADD ADMIN,SUPPLIER
            _farmcontext.users.Add(user);
            _farmcontext.SaveChanges();
            #endregion
        }

        public string Login(User users)
        {
            #region LOGIN 
            List<User> list = new List<User>();
            list = _farmcontext.users.ToList();
            foreach (var user in list)
            {
                if (user.email == users.email && user.password == users.password)
                {
                    string role = user.role;
                    return role;
                }
            }
            return "Invalid";
            #endregion
            //#region LOGIN 
            //var user = _farmcontext.users.FirstOrDefault(u => u.email == users.email);
            //if (user != null && user.password == users.password) // This is assuming passwords are plain text
            //{
            //    return 1; // Successful login
            //}
            //return -1; // Login failed
            //#endregion

        }
        public void Updateuser(User user)
        {
            #region EDIT PROFILE AFTER LOGIN 
            _farmcontext.Entry(user).State = EntityState.Modified;
            _farmcontext.SaveChanges();
            #endregion
        }

        public User GetUserById(int userId)
        {
            #region GET UNIQUE PROFILE 
            var result = _farmcontext.users.ToList();
            var user = result.Where(obj => obj.id == userId).FirstOrDefault();
            return user;
            #endregion

        }

        public IEnumerable<User> Getusers()
        {
            #region GET All USER
            return _farmcontext.users.ToList();
            #endregion
        }
        public void DeleteUser(int userId)
        {
            #region DELETE USER
            var user = _farmcontext.users.Find(userId);
            _farmcontext.users.Remove(user);
            _farmcontext.SaveChanges();
            #endregion
        }
    }
}
