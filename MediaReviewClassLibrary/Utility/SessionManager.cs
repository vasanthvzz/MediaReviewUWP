using CommonClassLibrary;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Diagnostics;
using Windows.Storage;

namespace MediaReviewClassLibrary.Data
{
    public static class SessionManager
    {
        private static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public static UserDetail User { get; set; }

        static SessionManager()
        {
            RetrieveUserFromStorage();
        }

        public static void SaveUserToStorage(UserDetail user)
        {
            localSettings.Values["userId"] = user.UserId.ToString();
        }
        
        public static void RetrieveUserFromStorage()
        {
            if (IsUserExist())
            {
                long.TryParse(localSettings.Values["userId"].ToString(), out long userId);
                GetUserDetailRequest request = new GetUserDetailRequest(userId);
                GetUserDetailUseCase uc = new GetUserDetailUseCase(request, new GetUserDetailPresenterCallback());
                uc.Execute();
            }
        }

        private static bool IsUserExist()
        {
            return localSettings.Values["userId"] != null;
        }

        public static void RemoveUserFromStorage()
        {
            localSettings.Values["userId"] = null;
        }

        public static string GetApplicationTheme()
        {
            return (string)localSettings.Values["theme"];
        }

        public static void SetApplicationTheme(string theme)
        {
            if (string.Equals(theme, "dark", System.StringComparison.OrdinalIgnoreCase) || string.Equals(theme, "light", System.StringComparison.OrdinalIgnoreCase))
            {
                localSettings.Values["theme"] = theme.ToLower();
            }
        }

        public static void StoreAccentColor(string getThemeName)
        {
            localSettings.Values["Accent"] = getThemeName.ToLower();
        }

        public static string GetAccentColor()
        {
            return (string)localSettings.Values["Accent"];
        }

        private class GetUserDetailPresenterCallback : IGetUserDetailPresenterCallback
        {
            public void OnFailure(Exception exception)
            {
                Debug.WriteLine(exception);
            }

            public void OnSuccess(ZResponse<GetUserDetailResponse> response)
            {
                User = response?.Data?.User;
            }
        }
    }
}