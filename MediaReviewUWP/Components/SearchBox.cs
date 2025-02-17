using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace MediaReviewUWP.Components
{
    public class SearchBox : TextBox
    {
        public event Action SearchButtonClicked;

        public SearchBox()
        {
            this.Style = Application.Current.Resources["SearchBoxStyle"] as Style;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var searchButton = GetTemplateChild("SearchButton") as Button;
            if (searchButton != null)
            {
                searchButton.Click -= SearchButton_Click;
                searchButton.Click += SearchButton_Click;
            }
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchButtonClicked?.Invoke();
        }
    }
}