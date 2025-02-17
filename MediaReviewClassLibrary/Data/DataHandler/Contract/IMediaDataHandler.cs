using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler.Contract
{
    public interface IMediaDataHandler
    {
        bool IsMediaExist(string title, DateTimeOffset releaseDate);

        Task<Media> CreateMedia(Media media);

        Task<List<Media>> GetAllMedia(long currentCount, long requiredCount);

        Task<Media> GetMediaById(long mediaId);

        Task<List<Media>> SearchMediaByName(string searchString);

        Task AddMedia(Media media);
    }
}