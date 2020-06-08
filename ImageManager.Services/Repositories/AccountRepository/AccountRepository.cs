using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ImageManager.EntityFramework;
using ImageManager.EntityFramework.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

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

        public string HashPassword(string password)
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        public void RegisterUser(string email, string login, string password)
        {
            User user = new User
            {
                Login = login,
                Email = email,
                Password = HashPassword(password),
                Role = UserRoles.RegularUser
            };
            AddUser(user);
        }

        public void WriteUserToDatabase()
        {
            throw new NotImplementedException();
        }

    }
}
