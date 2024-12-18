using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.View.HomePageView;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;

using System;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;


namespace MediaReviewUWP.View.WelcomePageView
{
    public sealed partial class LoginUserControl : UserControl
    {
        private ILoginUserViewModel _viewModel;
        public LoginUserControl()
        {
            this.InitializeComponent();
            //_viewModel = new LoginUserViewModel(this);
            //_viewModel.Dispatcher = Dispatcher;
        }

        public async void LoginFailure()
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                ErrorText.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                ErrorText.Text = "Password Missmatch";
            });
        }

        public async void UsernameNotFound()
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                ErrorText.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                ErrorText.Text = "User not Found";
            });

        }

        public async void LoginSuccess(UserDetail user)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
            {
                NavigateToHomePage(user);
            });
        }

        private void NavigateToHomePage(UserDetail user)
        {
            var parentFrame = Window.Current.Content as Frame;
            if (parentFrame != null)
            {
                parentFrame.Navigate(typeof(HomePage));
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTB.Text;
            string password = PasswordPB.Password;

            if (username != null && password != null)
            {
                _viewModel.LoginUser(username, password);
            }
        }

        private void ShowToastNotification(string title, string stringContent)
        {
            ToastNotifier ToastNotifier = ToastNotificationManager.CreateToastNotifier();
            Windows.Data.Xml.Dom.XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            Windows.Data.Xml.Dom.XmlNodeList toastNodeList = toastXml.GetElementsByTagName("text");
            toastNodeList.Item(0).AppendChild(toastXml.CreateTextNode(title));
            toastNodeList.Item(1).AppendChild(toastXml.CreateTextNode(stringContent));
            Windows.Data.Xml.Dom.IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            Windows.Data.Xml.Dom.XmlElement audio = toastXml.CreateElement("audio");
            audio.SetAttribute("src", "ms-winsoundevent:Notification.SMS");

            ToastNotification toast = new ToastNotification(toastXml);
            toast.ExpirationTime = DateTime.Now.AddSeconds(4);
            ToastNotifier.Show(toast);
        }
    }
}
