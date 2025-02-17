using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace MediaReviewUWP.ViewObject
{
    public class MediaDetailVObj : INotifyPropertyChanged
    {
        public long MediaId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string HomePageUrl { get; set; }
        public string ReleaseDate { get; set; }
        public string Runtime { get; set; }
        public string TagLine { get; set; }

        public PersonalMedia UserPersonalMedia { get; set; }
        public Rating UserRating { get; set; }
        public List<Genre> GenreList { get; set; }

        private string _posterPath;

        public string PosterPath
        {
            get => _posterPath;
            set
            {
                if (_posterPath != value)
                {
                    _posterPath = value;
                    OnPropertyChanged(nameof(PosterPath));
                };
            }
        }

        private float _mediaRating;

        public float MediaRating
        {
            get => _mediaRating;
            set
            {
                if (_mediaRating != value)
                {
                    _mediaRating = value;
                    UpdateMediaRatingString();
                    OnPropertyChanged(nameof(MediaRating));
                };
            }
        }

        private long _ratedUserCount;

        public long RatedUserCount
        {
            get => _ratedUserCount;
            set
            {
                if (_ratedUserCount != value)
                {
                    _ratedUserCount = value;
                    UpdateMediaRatingString();
                    OnPropertyChanged(nameof(RatedUserCount));
                }
            }
        }

        private string _mediaRatingString;

        public string MediaRatingString
        {
            get => _mediaRatingString;
            set
            {
                if (_mediaRatingString != value)
                {
                    _mediaRatingString = value;
                    OnPropertyChanged(nameof(MediaRatingString));
                }
            }
        }

        public MediaDetailVObj(MediaDetailBObj media)
        {
            MediaId = media.MediaDetail.MediaId;
            Title = media.MediaDetail.Title;
            Description = media.MediaDetail.Description;
            ImagePath = media.MediaDetail.ImagePath;
            PosterPath = media.MediaDetail.PosterPath;
            HomePageUrl = media.MediaDetail.HomepageUrl;
            ReleaseDate = media.MediaDetail.ReleaseDate.ToString("dd MMM yyyy");
            var loader = new ResourceLoader();
            Runtime = media.MediaDetail.Runtime <= 0 ? "" : media.MediaDetail.Runtime + " " + loader.GetString("Minutes");
            UserPersonalMedia = media.UserPersonalMedia;
            UserRating = media.UserRating;
            GenreList = media.Genres;
            MediaRating = media.MediaRating;
            RatedUserCount = media.RatedUsers;
        }

        private void UpdateMediaRatingString()
        {
            MediaRatingString = GetMediaRatingString();
        }

        private string GetMediaRatingString()
        {
            return MediaRating == 0 ? "" : (MediaRating % 1 == 0 ? $"{(int)MediaRating}" : $"{MediaRating:F1}") + "/5";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}