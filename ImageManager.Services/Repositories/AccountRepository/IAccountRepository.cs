using ImageManager.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageManager.Services.Repositories.AccountRepository
{
    public interface IAccountRepository
    {
        void RegisterUser(string email, string login, string password);
        string HashPassword(string password);
        void WriteUserToDatabase();
        void AddUser(User user);
    }
}
