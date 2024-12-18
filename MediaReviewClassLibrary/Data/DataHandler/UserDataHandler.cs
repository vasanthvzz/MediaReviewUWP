using MediaReviewClassLibrary.Data.Contract;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewClassLibrary.Utlis;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler
{
    public class UserDataHandler : IUserDataHandler
    {
        private IDatabaseAdapter _databaseAdapter = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IDatabaseAdapter>();
        private IPasswordAdapter _passwordAdapter = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IPasswordAdapter>();

        public async Task<UserDetail> CreateUser(string username, string password,string profilePicture)
        {
            //Generate unique id for user id
            long userId = IdentityManager.GenerateUniqueId();

            //Check whether the user name is already taken
            if (await IsUserExist(username))
            {
                return null;
            }
            else
            {
                UserDetail user = new UserDetail(userId, username,profilePicture);
                await _databaseAdapter.InsertAsync<UserDetail>(user);
                _passwordAdapter.AddUser(username, password);
                return user;
            }
        }

        public async Task<UserDetail> GetUserById(long id)
        {
            return await _databaseAdapter.FindAsync<UserDetail>(id);

        }

        public async Task<UserDetail> GetUserByName(string username)
        {
            var userDetails = await _databaseAdapter.GetTableAsync<UserDetail>();
            UserDetail userDetail = userDetails.FirstOrDefault(user => user.UserName == username);
            return userDetail;
        }

        public async Task<bool> IsUserExist(string username)
        {
            var userDetails = await _databaseAdapter.GetTableAsync<UserDetail>();
            return userDetails.Any(user => user.UserName == username);
        }

        public async Task<bool> IsValidCredential(string username, string password)
        {
            if (await IsUserExist(username))
            {
                return _passwordAdapter.ValidateUser(username, password);
            }
            return false;
        }
    }
}
