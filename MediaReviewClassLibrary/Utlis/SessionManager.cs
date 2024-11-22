using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewClassLibrary.Utlis;
using Windows.Storage;

namespace MediaReviewClassLibrary.Data
{
    public class SessionManager : ISessionManager
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        public void SaveUserToStorage(UserDetail user)
        {
            localSettings.Values["userId"] = user.UserId.ToString();
            localSettings.Values["userName"] = user.UserName;
            localSettings.Values["profilePicture"] = user.ProfilePicture;
        }

        public UserDetail RetriveUserFromStorage()
        {
            long userId = -1;
            if (IsUserExist())
            {
                bool parsed = long.TryParse(localSettings.Values["userId"].ToString(), out userId);
                if (parsed)
                {
                    UserDetail user = new UserDetail(userId, localSettings.Values["userName"].ToString(), localSettings.Values["profilePicture"].ToString());
                    return user;
                }
            }
            return null;
        }

        private bool IsUserExist()
        {
            return localSettings.Values["userId"] != null && localSettings.Values["userName"] != null;
        }

        public void RemoveUserFromStorage()
        {
            localSettings.Values["userId"] = null;
            localSettings.Values["userName"] = null;
            localSettings.Values["profilePicture"] = null;
        }

        public string GetApplicationTheme()
        {
            return (string)localSettings.Values["theme"];
        }

        public void SetApplicationTheme(string theme)
        {
            if (theme.ToLower() == "dark" || theme.ToLower() == "light")
            {
                localSettings.Values["theme"] = theme.ToLower();
            }
        }

        public void StoreAccentColor(string getThemeName)
        {
            localSettings.Values["Accent"] = getThemeName.ToLower();
        }

        public string GetAccentColor()
        {
            return (string)localSettings.Values["Accent"];
        }
    }
}
