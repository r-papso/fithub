namespace Fithub.API.Interfaces
{
    public interface IHashService
    {
        public string GenerateSalt();

        public string CryptPassword(string password, string salt);
    }
}
