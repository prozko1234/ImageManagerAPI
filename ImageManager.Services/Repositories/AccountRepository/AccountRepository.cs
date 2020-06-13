using System;
using System.Collections.Generic;
using ImageManager.EntityFramework;
using ImageManager.EntityFramework.Models;
using System.Linq;
using ImageManager.Logic.Hashing;
using ImageManager.Services.DTOModels;

namespace ImageManager.Services.Repositories.AccountRepository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationContext _applicationContext;

        public AccountRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void AddUser(User user)
        {
                _applicationContext.Add(user);
                _applicationContext.SaveChanges();
        }

        public void RegisterUser(string email, string login, string password)
        {
            var pass = Hashing.HashPassword(password);
            User user = new User
            {
                Login = login,
                Email = email,
                Password = pass.HashedPassword,
                Salt = pass.Salt,
                Role = UserRoles.RegularUser
            };
            AddUser(user);
        }

        public void RegisterAdmin(string email, string login, string password)
        {
            var pass = Hashing.HashPassword(password);
            User user = new User
            {
                Login = login,
                Email = email,
                Password = pass.HashedPassword,
                Salt = pass.Salt,
                Role = UserRoles.Admin
            };
            AddUser(user);
        }

        public List<User> GetAllProfiles()
        {
            throw new NotImplementedException();
        }

        public UserDTO LoginProfile(string username, string password)
        {
            var user = _applicationContext.Users.FirstOrDefault(x => x.Login == username);
            if (user == null) return null;
            if (Hashing.CheckPassword(password, user.Password, user.Salt))
            {
                return new UserDTO(){
                    Login = user.Login,
                    Email = user.Email,
                    Role = user.Role};
            }
            else return null;
        }
    }
}
