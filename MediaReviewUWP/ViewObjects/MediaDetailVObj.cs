using MediaReviewClassLibrary.Models.Enitites;

namespace MediaReviewUWP.ViewObjects
{
    public class MediaDetailVObj
    {
        public long MediaId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string PosterPath { get; set; }
        public string HomePageUrl { get; set; }
        public string ReleaseDate { get; set; }
        public string Runtime { get; set; }
        public string TagLine { get; set; }
        public PersonalMedia UserPersonalMedia {  get; set; }
        public Rating UserRating { get; set; }

        public MediaDetailVObj(Media media, PersonalMedia userPersonalMedia, Rating userRating)
        {
            MediaId = media.MediaId;
            Title = media.Title;
            Description = media.Description;
            ImagePath = media.ImagePath;
            PosterPath = media.PosterPath;
            HomePageUrl = media.HomepageUrl;
            ReleaseDate = media.ReleaseDate.ToString("dd MMM yyyy");
            Runtime = media.Runtime <= 0 ? "" : media.Runtime + " mins";
            TagLine = media.Tagline;
            UserPersonalMedia = userPersonalMedia;
            UserRating = userRating;
        }
    }
}
