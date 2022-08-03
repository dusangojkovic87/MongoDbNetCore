
using System.ComponentModel;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbNetCore.DbSettings;
using MongoDbNetCore.Entities;

namespace MongoDbNetCore.Services
{
    public class BookService
    {
        private IMongoCollection<Book> _bookscollection;
        public BookService(IOptions<BookStoreDatabaseSettings> bookStoreDbSettings)
        {
            var mongoClient = new MongoClient(bookStoreDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(bookStoreDbSettings.Value.DatabaseName);
            _bookscollection = mongoDatabase.GetCollection<Book>(bookStoreDbSettings.Value.BooksCollectionName);
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            return await _bookscollection.Find(x => true).ToListAsync();
        }

        public async Task RemoveAsync(string Id)
        {
            await _bookscollection.DeleteOneAsync(x => x.Id == Id);
        }
    }
}