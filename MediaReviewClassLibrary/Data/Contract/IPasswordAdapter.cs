namespace MediaReviewClassLibrary.Data.Contract
{
    public interface IPasswordAdapter
    {
        void AddUser(string userName, string password);

        bool ValidateUser(string userName, string password);

        bool IsUserExist(string userName);
    }
}