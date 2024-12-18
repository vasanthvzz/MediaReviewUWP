using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MediaReviewUWP.View.LandingPageView
{
    public sealed partial class LoginControl : UserControl , ILoginUserView
    {
        public event Action CreateAccountClicked;
        public event Action LoginCompleted;
        private ILoginUserViewModel _vm;

        public LoginControl()
        {
            this.InitializeComponent();
            _vm = new LoginUserViewModel(this);
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            CreateAccountClicked?.Invoke();
        }


        private void RevealModeCheckbox_Changed(object sender, RoutedEventArgs e)
        {
            if (revealModeCheckBox.IsChecked == true)
            {
                PasswordTb.PasswordRevealMode = PasswordRevealMode.Visible;
            }
            else
            {
                PasswordTb.PasswordRevealMode = PasswordRevealMode.Hidden;
            }
        }

        private void LoginAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            string userName = UsernameTb.Text;
            string password = PasswordTb.Password;
            _vm.LoginUser(userName,password);
        }

        public async void LoginFailure()
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                InvalidCredentialTT.IsOpen = true;
            });
        }

        public void LoginSuccess()
        {
            LoginCompleted?.Invoke();
        }
    }
}
