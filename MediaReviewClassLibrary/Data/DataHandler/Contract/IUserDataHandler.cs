using MediaReviewClassLibrary.Models.Enitites;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler.Contract
{
    public interface IUserDataHandler
    {
        Task<UserDetail> CreateUser(string username, string password, string profilePicture);

        Task<bool> IsUserExist(string username);

        Task<UserDetail> GetUserById(long id);

        Task<UserDetail> GetUserByName(string username);

        Task<bool> IsValidCredential(string username, string password);
    }
}