using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;

namespace MediaReviewClassLibrary.Models
{
    public class AddMediaBObj
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string TileImage { get; set; }
        public string PosterImage { get; set; }
        public int Runtime { get; set; }
        public DateTimeOffset ReleaseDate { get; set; }
        public List<Genre> GenreList { get; set; }

        public AddMediaBObj(string title, string description, string tileImage = "", string posterImage = "", int runtime = 0, DateTimeOffset releaseDate = default, List<Genre> genreList = null)
        {
            Title = title;
            Description = description;
            TileImage = tileImage;
            PosterImage = posterImage;
            Runtime = runtime;
            ReleaseDate = releaseDate;
            GenreList = genreList;
        }

        //public AddMediaBObj(string title, string description, string tileImage, string posterImage, int runtime, DateTimeOffset releaseDate, List<Genre> genreList)
        //{
        //    Title = title;
        //    Description = description;
        //    TileImage = tileImage;
        //    PosterImage = posterImage;
        //    Runtime = runtime;
        //    ReleaseDate = releaseDate;
        //    GenreList = genreList;
        //}
    }
}