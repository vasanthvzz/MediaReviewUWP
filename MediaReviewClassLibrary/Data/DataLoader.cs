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
            //LoadData1();
            //LoadData2();
            //LoadData3();
        }


        private static void LoadData3()
        {
            long mediaId = 83483;
            string title = "Moana 2";
            string imagePath = "https://static.wikia.nocookie.net/onepiece/images/3/37/Volume_4000000000.png/revision/latest?cb=20220629185455";
            string homepageUrl = "";
            string description = "After receiving an unexpected call from her wayfinding ancestors, Moana must journey to the far seas of Oceania and into dangerous, long-lost waters for an adventure unlike anything she's ever faced.";
            string posterPath = "https://preview.redd.it/new-posters-for-moana-2-coming-to-theaters-november-27-v0-kzbz3qdsfixd1.jpg?width=640&crop=smart&auto=webp&s=e033768af59f296ed23de34881faa4d5cc468986";
            DateTime releaseDate = DateTime.Parse("11/27/2024 ");
            int runtime = 100;
            string tagline = "";

            Media media = new Media(mediaId, title, description, imagePath, posterPath, homepageUrl, releaseDate, runtime, tagline);
            _dh.CreateMedia(media);
        }

        private static void LoadData2()
        {
            long mediaId = 86785;
            string title = "Arcane";
            string imagePath = "https://m.media-amazon.com/images/M/MV5BOWJhYjdjNWEtMWFmNC00ZjNkLThlZGEtN2NkM2U3NTVmMjZkXkEyXkFqcGc@._V1_.jpg";
            string homepageUrl = "";
            string description = "Amid the stark discord of twin cities Piltover and Zaun, two sisters fight on rival sides of a war between magic technologies and clashing convictions.";
            string posterPath = "https://uploads.alternativanerd.com.br/wp-content/uploads/2021/11/Arcane-Criadores-explicam-como-escolheram-os-campeoes-da-serie.jpg";
            DateTime releaseDate = DateTime.Parse("06/11/2021");
            int runtime = 40;
            string tagline = "";

            Media media = new Media(mediaId, title, description, imagePath, posterPath, homepageUrl, releaseDate, runtime, tagline);
            _dh.CreateMedia(media);
        }

        private static void LoadData1()
        {
            long mediaId = 48239;
            string title = "Interstellar";
            string imagePath = "https://www.tallengestore.com/cdn/shop/products/18_dede6bd2-6a23-41f9-a881-bf86e6fc1d5e.jpg?v=1568967564";
            string homepageUrl = "";
            string description = "When Earth becomes uninhabitable in the future, a farmer and ex-NASA pilot, Joseph Cooper, is tasked to pilot a spacecraft, along with a team of researchers, to find a new planet for humans.";
            string posterPath = "https://variety.com/wp-content/uploads/2014/10/interstellar-6.jpg?w=1000";
            DateTime releaseDate = DateTime.Parse("2014-07-11");
            int runtime = 0;
            string tagline = "169";


            Media media = new Media(mediaId, title, description, imagePath, posterPath, homepageUrl, releaseDate, runtime, tagline);
            _dh.CreateMedia(media);
        }
    }
}
