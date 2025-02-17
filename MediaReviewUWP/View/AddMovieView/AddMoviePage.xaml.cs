using CommunityToolkit.Common;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.Utility;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MediaReviewUWP.View.AddMovieView
{
    public sealed partial class AddMoviePage : Page, IAddMovieView
    {
        private IAddMovieViewModel _vm;
        public List<Genre> GenreList { get; set; }
        public ObservableCollection<Genre> SelectedGenre { get; set; }

        public AddMoviePage()
        {
            GenreList = new List<Genre>();
            SelectedGenre = new ObservableCollection<Genre>();
            _vm = new AddMovieViewModel(this);
            this.InitializeComponent();
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            GetMovieDetail();
        }

        private void GetMovieDetail()
        {

            string title = TitleBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(title))
            {
                InvalidMediaTitleTT.IsOpen = true;
                return;
            }

            if (ReleaseDatePicker == null || ReleaseDatePicker.Date == null)
            {
                InvalidMediaReleaseDateTT.IsOpen = true;
                return;
            }
            DateTimeOffset releaseDate = (DateTimeOffset)ReleaseDatePicker.Date;

            var runtimeString = RuntimeBox.Text;
            bool runtimeParsed = Int32.TryParse(runtimeString, out var runtime);
            if (runtimeString.Trim() != "" && runtime > 300)
            {
                InvalidMediaRuntimeTT.IsOpen = true;
                return;
            }

            string description = DescriptionBox.Text.Trim();
            string tileImage = TileImageBox.Text.Trim();
            string posterImage = PosterImageBox.Text.Trim();
            List<Genre> genreList = SelectedGenre.ToList();
            
            AddMediaBObj media = new AddMediaBObj(title, description, tileImage, posterImage, runtime, releaseDate, genreList);
            ResetTextBlocks();
            AddMovie(media);
        }

        private void ResetTextBlocks()
        {
            TitleBox.Text = "";
            DescriptionBox.Text = "";
            TileImageBox.Text = "";
            PosterImageBox.Text = "";
            ReleaseDatePicker.Date = null;
            GenreList.AddRange(SelectedGenre);
            SelectedGenre.Clear();
            SelectedGenresPanel.Items.Clear();
            RuntimeBox.Text = "";
        }
        private void AddMovie(AddMediaBObj media)
        {
            _vm.AddMovie(media);
        }

        private void AddGenre(string genre)
        {
            if (!string.IsNullOrWhiteSpace(genre) && GenreList.Select(g => g.GenreName).Contains(genre) && !SelectedGenre.Select(g => g.GenreName).Contains(genre))
            {
                SelectedGenre.Add(GetGenreByName(genre));
                GenreList.Remove(GetGenreByName(genre));
                var genreContainer = new Border
                {
                    Style = (Style)this.Resources["GenreContainerStyle"]
                };

                var genreGrid = new Grid();
                genreGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                genreGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                var genreTextBlock = new TextBlock
                {
                    Text = genre,
                    Style = (Style)this.Resources["GenreTextStyle"]
                };
                Grid.SetColumn(genreTextBlock, 0);
                var removeButton = new Button
                {
                    Tag = genre,
                    Style = (Style)this.Resources["RemoveButtonStyle"]
                };
                removeButton.Click += RemoveGenreButton_Click;
                Grid.SetColumn(removeButton, 1);
                genreGrid.Children.Add(genreTextBlock);
                genreGrid.Children.Add(removeButton);
                genreContainer.Child = genreGrid;
                SelectedGenresPanel.Items.Add(genreContainer);
                GenreSuggestBox.Text = "";
            }
            //GenreSuggestBox.Text = "";
            GenreSuggestBox.IsSuggestionListOpen = false;
        }

        private Genre GetGenreByName(string name)
        {
            var genreList = GenreList.Union(SelectedGenre);
            return genreList.FirstOrDefault(genre => genre.GenreName == name);
        }

        private void GenreSearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(args.QueryText))
            {
                if (GenreList.Select(genre => genre.GenreName).Contains(args.QueryText))
                {
                    AddGenre(args.QueryText);
                }
            }
        }

        private void RemoveGenreButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string genreName)
            {
                Genre genre = GetGenreByName(genreName);
                if (genre == null)
                {
                    return;
                }
                SelectedGenre.Remove(genre);
                var parent = button.Parent as Grid;
                var border = parent.Parent as Border;
                SelectedGenresPanel.Items.Remove(border);
                GenreList.Add(genre);
            }
        }

        private void GenreSearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = string.IsNullOrWhiteSpace(sender.Text)
                    ? new string[0]
                    : GenreList.Where(genre => genre.GenreName.StartsWith(sender.Text, StringComparison.OrdinalIgnoreCase)).Select(g => g.GenreName).ToArray();
                sender.ItemsSource = suggestions;
            }
            if (sender.Text == " ")
            {
                sender.ItemsSource = GenreList.Select(g => g.GenreName);
            }
        }

        private void Page_Loading(FrameworkElement sender, object args)
        {
            _vm.GetAllGenre();
        }

        public void UpdateGenreList(List<Genre> genreList)
        {
            if (genreList != null)
            {
                GenreList = genreList;
            }
        }

        public void ShowMovieAdditionStatus(Media mediaDetail, bool success)
        {
            _ = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                (success ? MediaAddedSuccess : InvalidMediaDataTT).IsOpen = true;
                if (success)
                {
                    GlobalEventManager.MediaAdded(mediaDetail.MediaId, mediaDetail.ReleaseDate);
                }
            });
        }

        private void RuntimeBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            if (sender is TextBox textBox)
            {
                args.Cancel = !string.IsNullOrEmpty(args.NewText) && !args.NewText.IsNumeric();
            }
        }
    }
}