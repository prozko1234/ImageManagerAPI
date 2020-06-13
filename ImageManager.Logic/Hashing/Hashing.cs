using ImageManager.EntityFramework.Models;
using ImageManager.Logic.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ImageManager.Logic.Hashing
{
    public static class Hashing
    {
        public static HashingObject HashPassword(string password)
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
            HashingObject data = new HashingObject { HashedPassword= hashed, Salt = Convert.ToBase64String(salt) };
            return data;
        }

        public static bool CheckPassword(string password, string hashedPassword, string stringSalt)
        {
            var salt = Convert.FromBase64String(stringSalt);

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            if (hashed == hashedPassword)
                return true;
            else return false;
        }
    }
}
