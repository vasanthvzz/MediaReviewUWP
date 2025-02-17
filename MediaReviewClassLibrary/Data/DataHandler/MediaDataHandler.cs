using MediaReviewClassLibrary.Data.Contract;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler
{
    public class MediaDataHandler : IMediaDataHandler
    {
        private IDatabaseAdapter _databaseAdapter = MediaReviewDIServiceProvider.GetRequiredService<IDatabaseAdapter>();

        public async Task<Media> CreateMedia(Media media)
        {
            if (IsMediaExist(media.Title, media.ReleaseDate.Date))
            {
                return null;
            }
            await _databaseAdapter.InsertOrReplaceAsync<Media>(media);
            return media;
        }

        public bool IsMediaExist(string title, DateTimeOffset releaseDate)
        {
            title = title.Trim().ToLower();
            var mediaList =  _databaseAdapter.GetTableQuery<Media>().AsQueryable().Where(t =>t.Title.ToLower() == title && t.ReleaseDate.Date == releaseDate.Date).ToList();
            return mediaList.Count > 0;
        }

        public async Task<Media> GetMediaById(long id)
        {
            return await _databaseAdapter.FindAsync<Media>(id);
        }

        public Task<List<Media>> GetAllMedia()
        {
            return _databaseAdapter.GetTableAsync<Media>();
        }

        //public async Task<bool> IsMediaExist(string title, DateTime releaseDate)
        //{
        //    return await _databaseAdapter.GetAsyncTableQuery<Media>().
        //        Where(media => string.Equals(media.Title, title, StringComparison.OrdinalIgnoreCase) && media.ReleaseDate.Date == releaseDate.Date).CountAsync() > 0;
        //}

        public async Task<List<Media>> GetAllMedia(long currentCount, long requiredCount)
        {
            return await _databaseAdapter.GetAsyncTableQuery<Media>()
                .OrderByDescending(media => media.ReleaseDate)
                .Skip((int)currentCount)
                .Take((int)requiredCount)
                .ToListAsync();
        }

        public async Task<List<Media>> SearchMediaByName(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return new List<Media>();

            string searchPattern = $"%{searchString}%";

            string query = $"SELECT * FROM media WHERE LOWER(Title) LIKE LOWER('{searchPattern}');";

            var result = await _databaseAdapter.ExecuteQuery<Media>(query);
            return result;
        }

        public async Task AddMedia(Media media)
        {
            await _databaseAdapter.InsertOrReplaceAsync(media);
        }
    }
}