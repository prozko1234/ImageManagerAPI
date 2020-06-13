using ImageManager.EntityFramework.Models;
using ImageManager.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageManager.Services.Repositories.AccountRepository
{
    public interface IAccountRepository
    {
        void RegisterUser(string email, string login, string password);
        void RegisterAdmin(string email, string login, string password);
        void AddUser(User user);
        List<User> GetAllProfiles();
        UserDTO LoginProfile(string username, string password);
    }
}
