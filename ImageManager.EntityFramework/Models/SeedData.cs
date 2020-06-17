using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ImageManager.EntityFramework.Models
{
    public class SeedData
    {
        public string HashedPassword { get; set; }
        public string Salt { get; set; }

        public void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationContext>>()))
            {
                if (context.Users.Any())
                {
                    return;   // alredy exists
                }
                var user = HashPassword("user");
                var admin = HashPassword("admin");
                context.Users.AddRange(
                    new User
                    {
                        Login = "admin",
                        Password = admin.HashedPassword,
                        Salt = admin.Salt,
                        Email = "admin@mail.com",
                        Role = UserRoles.Admin
                    },
                    new User
                    {
                        Login = "user",
                        Password = user.HashedPassword,
                        Salt = user.Salt,
                        Email = "user@mail.com",
                        Role = UserRoles.RegularUser
                    }
                );
                context.SaveChanges();
            }
        }

        public SeedData HashPassword(string password)
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
            return new SeedData { HashedPassword = hashed, Salt = Convert.ToBase64String(salt) };
        }
    }
}

