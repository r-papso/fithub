using Fithub.API.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Fithub.API.Services
{
    public class HashService : IHashService
    {
        private const int SaltLength = 32;

        public string CryptPassword(string password, string salt)
        {
            using var sha = new SHA512Managed();
            var bytes = Encoding.UTF8.GetBytes(password + salt);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public string GenerateSalt()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[SaltLength];
            rng.GetNonZeroBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
