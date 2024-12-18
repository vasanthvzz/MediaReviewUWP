using MediaReviewClassLibrary.Data.Contract;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler
{
    public class MediaDataHandler : IMediaDataHandler
    {
        private IDatabaseAdapter _databaseAdapter = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IDatabaseAdapter>();

        public async Task<Media> CreateMedia(Media media)
        {
            if (await IsMediaExist(media.Title, media.ReleaseDate))
            {
                return null;
            }
            await _databaseAdapter.InsertOrReplaceAsync<Media>(media);
            return media;
        }

        public async Task<Media> GetMediaById(long id)
        {
            return await _databaseAdapter.FindAsync<Media>(id);
        }

        public Task<List<Media>> GetAllMedia()
        {
            return _databaseAdapter.GetTableAsync<Media>();
        }

        public async Task<bool> IsMediaExist(string title, DateTime releaseDate)
        {
            return await _databaseAdapter.GetTableQuery<Media>().
                Where(media => media.Title == title && media.ReleaseDate == releaseDate).CountAsync() > 0;
        }

        public async Task<List<Media>> GetAllMedia(long currentCount, long requiredCount)
        {
            return await _databaseAdapter.GetTableQuery<Media>()
                .Skip((int)currentCount)
                .Take((int)requiredCount)
                .ToListAsync();
        }
    }
}
