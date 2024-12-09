using MediaReviewClassLibrary.Data.Contract;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Models.Constants;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler
{
    public class PersonalMediaDataHandler : IPersonalMediaDataHandler
    {

        private IDatabaseAdapter _databaseAdapter = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IDatabaseAdapter>();

        public async Task<PersonalMedia> GetPersonalMedia(long mediaId, long userId)
        {
            return await  _databaseAdapter.GetTableQuery<PersonalMedia>()
                .Where(personalMedia => personalMedia.MediaId == mediaId && personalMedia.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<PersonalMedia>> GetPersonalisedMedia(long userId ,PersonalMediaType personalMediaType)
        {
            switch (personalMediaType)
            {
                case PersonalMediaType.FAVOURITE:
                    {
                        return await _databaseAdapter.GetTableQuery<PersonalMedia>()
                .Where(personalMedia => personalMedia.UserId == userId && personalMedia.IsFavourite)
                    .ToListAsync();
                    }
                case PersonalMediaType.WATCHLIST:
                    {
                        return await _databaseAdapter.GetTableQuery<PersonalMedia>()
                .Where(personalMedia => personalMedia.UserId == userId && personalMedia.WatchList)
                    .ToListAsync();
                    }
                case PersonalMediaType.HAS_WATCHED:
                    {
                        return await _databaseAdapter.GetTableQuery<PersonalMedia>()
                .Where(personalMedia => personalMedia.UserId == userId && personalMedia.HasWatched)
                    .ToListAsync();
                    }
                default:
                    {
                        throw new Exception("Invalid personalised media");
                    }
            }
            
        }

        public async Task<PersonalMedia> UpdatePersonalMedia(PersonalMedia personalMedia)
        {
            var existingRecord = await GetPersonalMedia(personalMedia.MediaId, personalMedia.UserId);

            if (existingRecord == null)
            { 
                await _databaseAdapter.InsertAsync(personalMedia);
            }
            else
            {
                var customQuery = "UPDATE personal_media SET is_favourite = ?, has_watched = ?, watchlist = ? WHERE media_id = ? AND user_id = ?";
                await _databaseAdapter.ExecuteQuery<PersonalMedia>(customQuery, personalMedia.IsFavourite, personalMedia.HasWatched, personalMedia.WatchList, personalMedia.MediaId, personalMedia.UserId);
            }
            return await GetPersonalMedia(personalMedia.MediaId, personalMedia.UserId);
        }

        public async Task<long> RemoveFromFavourite(long userId, long mediaId)
        {
            var existingRecord = await GetPersonalMedia(mediaId, userId);

            if (existingRecord == null)
            {
                await _databaseAdapter.InsertAsync(new PersonalMedia(userId,mediaId));
            }
            else
            {
                var customQuery = "UPDATE personal_media SET is_favourite = ? WHERE media_id = ? AND user_id = ?";
                await _databaseAdapter.ExecuteQuery<PersonalMedia>(customQuery, false ,mediaId, userId);
            }
            return mediaId;
        }

        public async Task<long> RemoveFromWatchList(long userId, long mediaId)
        {
            var existingRecord = await GetPersonalMedia(mediaId, userId);

            if (existingRecord == null)
            {
                await _databaseAdapter.InsertAsync(new PersonalMedia(userId, mediaId));
            }
            else
            {
                var customQuery = "UPDATE personal_media SET watchlist = ? WHERE media_id = ? AND user_id = ?";
                await _databaseAdapter.ExecuteQuery<PersonalMedia>(customQuery, false, mediaId, userId);
            }
            return mediaId;
        }

        public async Task<long> RemoveFromHasWatched(long userId, long mediaId)
        {
            var existingRecord = await GetPersonalMedia(mediaId, userId);

            if (existingRecord == null)
            {
                await _databaseAdapter.InsertAsync(new PersonalMedia(userId, mediaId));
            }
            else
            {
                var customQuery = "UPDATE personal_media SET has_watched = ? WHERE media_id = ? AND user_id = ?";
                await _databaseAdapter.ExecuteQuery<PersonalMedia>(customQuery, false, mediaId, userId);
            }
            return mediaId;
        }
    }
}
