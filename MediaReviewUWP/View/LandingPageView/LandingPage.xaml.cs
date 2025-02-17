using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MediaReviewUWP.View.LandingPageView
{
    public sealed partial class LandingPage : Page
    {
        public static event Action LoginSuccess;

        public LandingPage()
        {
            this.InitializeComponent();
        }

        private void ApplicationInfoUserControl_SigninButtonClick()
        {
            if (MainContentPresenter != null)
            {
                MainContentPresenter.ContentTemplate = (DataTemplate)Resources["LoginControlComponent"];
            }
        }

        private void LoginControl_CreateAccountClicked()
        {
            if (MainContentPresenter != null)
            {
                MainContentPresenter.ContentTemplate = (DataTemplate)Resources["SignUpControlComponent"];
            }
        }

        private void ValidationCompleted()
        {
            LoginSuccess?.Invoke();
        }
    }
}