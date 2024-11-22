using MediaReviewClassLibrary.Data.Contract;
using System;
using Windows.Security.Credentials;

namespace MediaReviewClassLibrary.Data
{
    public class PasswordAdapter : IPasswordAdapter
    {
        private PasswordVault _vault;
        public PasswordAdapter()
        {
            _vault = new PasswordVault();
        }

        public void AddUser(string userName, string password)
        {
            PasswordCredential credential = new PasswordCredential()
            {
                Resource = "UserCredential",
                UserName = userName,
                Password = password
            };
            _vault.Add(credential);
        }

        public bool ValidateUser(string userName, string password)
        {
            if (IsUserExist(userName))
            {
                var credential = _vault.Retrieve("UserCredential", userName);
                return credential.Password == password;
            }
            return false;
        }

        public bool IsUserExist(string userName)
        {
            return _vault.Retrieve("UserCredential", userName) != null;
        }
    }
}
