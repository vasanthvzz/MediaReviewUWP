using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.Contract
{
    public interface IDatabaseAdapter
    {
        Task<List<T>> GetTableAsync<T>() where T : new();
        Task<int> InsertAsync<T>(T obj) where T : new();
        Task<int> InsertOrReplaceAsync<T>(T obj) where T : new();
        Task<int> DeleteAsync<T>(long id) where T : new();
        Task<int> UpdateAsync<T>(T obj) where T : new();
        AsyncTableQuery<T> GetTableQuery<T>() where T : new();
        Task<int> DeleteAsync<T>(T obj) where T : new();
        Task<T> FindAsync<T>(long id) where T : new();
        Task<List<T>> ExecuteQuery<T>(string query, params object[] args) where T : new();
    }
}