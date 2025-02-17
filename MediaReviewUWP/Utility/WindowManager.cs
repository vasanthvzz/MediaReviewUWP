using System;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;

namespace MediaReviewUWP.Utility
{
    public static class WindowManager
    {
        public static Window MainWindow { get; set; }
        public static AppWindow SettingsWindow { get; set; }
        public static AppWindow CreateMovieWindow { get; set; }
        public static event Func<Task> RequestRefresh;

        public static void Initialize()
        {
            MainWindow = Window.Current;
        }

        public static void AddSettingsWindow(AppWindow settingsWindow)
        {
            if (SettingsWindow == null)
            {
                SettingsWindow = settingsWindow;
            }
        }

        public static void AddMovieWindow(AppWindow movieWindow)
        {
            if (movieWindow == null)
            {
                CreateMovieWindow = movieWindow;
            }
        }

        public static bool SettingsWindowExist()
        {
            return SettingsWindow != null;
        }

        public static bool CreateMovieWindowExist()
        {
            return CreateMovieWindow != null;
        }

        public static async Task CloseSettingsWindow(CoreDispatcher dispatcher)
        {
            if (SettingsWindow == null)  return;
            var dispatcherQueue = SettingsWindow.DispatcherQueue;

            // Run code on the AppWindow's thread
            dispatcherQueue.TryEnqueue(async () =>
            {
                await SettingsWindow.CloseAsync();
            });
            //if (dispatcher != null)
            //{
            //    await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            //    {
            //        if (SettingsWindow != null)
            //        {
            //            await SettingsWindow.CloseAsync();
            //        }
            //        SettingsWindow = null;
            //    });
            //}
        }

        public static async Task CloseCreateMovieWindow(CoreDispatcher dispatcher)
        {
            if (dispatcher != null)
            {
                await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                if (CreateMovieWindow != null)
                {
                    await CreateMovieWindow.CloseAsync();
                }
                CreateMovieWindow = null;
                });
            }
        }

        public static void LanguageChanged()
        {
            RequestRefresh?.Invoke();
        }

        //public static void AddSettingWindow(WindowType windowName,AppWindow window)
        //{
        //    if (AppWindowDict.ContainsKey(windowName))
        //    {
        //        if(AppWindowDict[windowName] == null)
        //        {
        //            AppWindowDict[windowName] = new List<AppWindow>();
        //        }
        //        AppWindowDict[windowName].Add(window);
        //    }
        //    else
        //    {
        //        AppWindowDict.Add(windowName, new List<AppWindow>() { window } );
        //    }
        //}

        //public static bool IsWindowExist(WindowType windowType)
        //{
        //    if (AppWindowDict.ContainsKey(windowType))
        //    {
        //        return AppWindowDict.GetValueOrDefault(windowType).Any();
        //    }
        //    return false;
        //}

        //public static List<AppWindow> GetAppWindows(WindowType windowType)
        //{
        //    if (AppWindowDict.ContainsKey(windowType))
        //    {
        //        if (AppWindowDict[windowType] == null)
        //        {
        //            AppWindowDict[windowType] = new List<AppWindow>();
        //        }
        //        return AppWindowDict[windowType];
        //    }
        //    else
        //    {
        //        AppWindowDict.Add(windowType, new List<AppWindow>());
        //        return AppWindowDict[windowType];
        //    }
        //}

        //public static List<AppWindow> GetAppWindowList()
        //{
        //    var appWindows = new List<AppWindow>();
        //    foreach (var pair in AppWindowDict)
        //    {
        //        if (AppWindowDict[pair.Key] != null)
        //        {
        //            appWindows.AddRange(AppWindowDict[pair.Key]);
        //        }
        //    }
        //    return appWindows;
        //}

        //public static void RemoveWindow(WindowType windowType, AppWindow window)
        //{
        //    AppWindowDict[windowType].Remove(window);
        //}

        //public async static Task RemoveAllWindows()
        //{
        //    foreach(var key in AppWindowDict.Keys)
        //    {
        //        foreach(var window in AppWindowDict[key])
        //        {
        //            await window.CloseAsync();
        //        }
        //    }
        //    AppWindowDict.Clear();
        //}
    }

    public enum WindowType
    {
        SETTINGS,
        OTHERS
    }
}