using MediaReviewClassLibrary.Data.DataHandler;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Diagnostics;

namespace MediaReviewClassLibrary.Data
{
    public static class DataLoader
    {
        private static IMediaDataHandler _dh = new MediaDataHandler();
        public static void LoadData()
        {
        }

        //long mediaId =  ;
        //string title = "";
        //string imagePath = "";
        //string homepageUrl = ;
        //string description = "";
        //string posterPath = "";
        //DateTime releaseDate = DateTime.Parse("");
        //int runtime = ;
        //string tagline = "";

        private static void LoadData3()
        {
            long mediaId = 10001;
            string title = "One Piece Film Red (2022)";
            string imagePath = "https://static.wikia.nocookie.net/onepiece/images/3/37/Volume_4000000000.png/revision/latest?cb=20220629185455";
            string homepageUrl = "";
            string description = "Uta — the most beloved singer in the world. Her voice, which she sings with while concealing her true identity, has been described as “otherworldly.” She will appear in public for the first time at a live concert. As the venue fills with all kinds of Uta fans — excited pirates, the Navy watching closely, and the Straw Hats led by Luffy who simply came to enjoy her sonorous performance — the voice that the whole world has been waiting for is about to resound.";
            string posterPath = "https://www.hindustantimes.com/ht-img/img/2023/04/19/550x309/one_piece_film_red_1681910427977_1681910453047.jpeg";
        DateTime releaseDate = DateTime.Parse("10/07/2022 ");
        int runtime = 115;
            string tagline = "An almighty voice. With fiery red locks.";

            Media media = new Media(mediaId, title, description, imagePath, posterPath, homepageUrl, releaseDate, runtime, tagline);
            _dh.CreateMedia(media);
        }

        private static void LoadData2()
        {
            long mediaId = 1111;
            string title = "Beast";
            string imagePath = "https://www.emagine-entertainment.com/wp-content/uploads/2022/04/beast-tamil-poster.jpg";
            string homepageUrl = "https://www.netflix.com/in/title/81508241";
            string description = "A former RAW officer, who is among the hostages in a mall taken over by terrorists, has to foil their plans and prevent the government from releasing a dreaded terrorist, who he had helped put in prison at great personal cost.";
            string posterPath = "https://hombalefilms.com/wp-content/uploads/2021/12/Untitled-design-84-1.png";
            DateTime releaseDate = DateTime.Parse("04/13/2022");
            int runtime = 155;
            string tagline = "The beast is yet to come";



            Media media = new Media(mediaId, title, description, imagePath, posterPath, homepageUrl, releaseDate, runtime, tagline);
            _dh.CreateMedia(media);
        }

        private static void LoadData1()
        {
            long mediaId = 2220;
            string title = "Vinland Saga (Season - 1)";
            string imagePath = "/ohfWCHT65P7b3kQvZnoy2BL95MB.jpg";
            string homepageUrl = "http://www.sonypictures.com/movies/ghostrider/";
            string description = "For a thousand years, the Vikings have made quite a name and reputation for themselves as the strongest families with a thirst for violence. Thorfinn, the son of one of the Vikings' greatest warriors, spends his boyhood in a battlefield enhancing his skills in his adventure to redeem his most-desired revenge after his father was murdered."; string posterPath = "/1pyU94dAY7npDQCKuxCSyX9KthN.jpg";
            DateTime releaseDate = DateTime.Parse("2019-07-16");
            int runtime = 0;
            string tagline = "I have no Enemies";


            Media media = new Media(mediaId, title, description, imagePath, posterPath, homepageUrl, releaseDate, runtime, tagline);
            _dh.CreateMedia(media);
        }
    }
}
