using MediaReviewClassLibrary.Models.Enitites;

namespace MediaReviewClassLibrary.Utlis
{
    public interface ISessionManager
    {
        void SaveUserToStorage(UserDetail user);
        UserDetail RetriveUserFromStorage();
        void RemoveUserFromStorage();
        void SetApplicationTheme(string theme);
        string GetApplicationTheme();
        void StoreAccentColor(string getThemeName);
        string GetAccentColor();
    }
}
