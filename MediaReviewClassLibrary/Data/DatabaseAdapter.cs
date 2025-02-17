using MediaReviewClassLibrary.Data.Contract;
using MediaReviewClassLibrary.Models.Enitites;
using SQLite;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace MediaReviewClassLibrary.Data
{
    public class DatabaseAdapter : IDatabaseAdapter
    {
        private static SQLiteAsyncConnection _dbAsync;
        private static SQLiteConnection _db;

        static DatabaseAdapter()
        {
            string folderPath = ApplicationData.Current.LocalFolder.Path;
            string databasePath = Path.Combine(folderPath, "media_db.db");
            Debug.WriteLine($"Database Path: {databasePath}");
            _dbAsync = new SQLiteAsyncConnection(databasePath);
            _db = new SQLiteConnection(databasePath);
            _dbAsync.CreateTableAsync<FolloweeMapper>();
            _dbAsync.CreateTableAsync<Genre>();
            _dbAsync.CreateTableAsync<GenreMapper>();
            _dbAsync.CreateTableAsync<Media>();
            _dbAsync.CreateTableAsync<PersonalMedia>();
            _dbAsync.CreateTableAsync<Rating>();
            _dbAsync.CreateTableAsync<Review>();
            _dbAsync.CreateTableAsync<UserDetail>();
        }

        public async Task<int> DeleteAsync<T>(long id) where T : new()
        {
            return await _dbAsync.DeleteAsync<T>(id);
        }

        public async Task<T> FindAsync<T>(long id) where T : new()
        {
            return await _dbAsync.FindAsync<T>(id);
        }



        public async Task<int> DeleteAsync<T>(T obj) where T : new()
        {
            return await _dbAsync.DeleteAsync<T>(obj);
        }

        public AsyncTableQuery<T> GetAsyncTableQuery<T>() where T : new()
        {
            return _dbAsync.Table<T>();
        }

        public TableQuery<T> GetTableQuery<T>() where T : new()
        {
            return _db.Table<T>();
        }

        public async Task<List<T>> GetTableAsync<T>() where T : new()
        {
            return await _dbAsync.Table<T>().ToListAsync();
        }

        public async Task<int> InsertAsync<T>(T obj) where T : new()
        {
            return await _dbAsync.InsertAsync(obj);
        }

        public async Task<int> InsertOrReplaceAsync<T>(T obj) where T : new()
        {
            return await _dbAsync.InsertOrReplaceAsync(obj);
        }

        public async Task<List<T>> ExecuteQuery<T>(string query, params object[] args) where T : new()
        {
            return await _dbAsync.QueryAsync<T>(query, args);
        }

        public async Task<int> UpdateAsync<T>(T obj) where T : new()
        {
            return await _dbAsync.UpdateAsync(obj);
        }
    }
}