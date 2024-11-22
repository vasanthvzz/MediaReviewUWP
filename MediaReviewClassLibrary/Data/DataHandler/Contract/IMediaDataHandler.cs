using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler.Contract
{
    public interface IMediaDataHandler
    {
        Task<bool> IsMediaExist(string title, DateTime releaseDate);
        Task<Media> CreateMedia(Media media);
        Task<List<Media>> GetAllMedia();
        Task<Media> GetMediaById(long mediaId);
    }
}
