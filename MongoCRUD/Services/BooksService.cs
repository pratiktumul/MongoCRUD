using Microsoft.Extensions.Options;
using MongoCRUD.Configurations;
using MongoCRUD.Models;
using MongoDB.Driver;

namespace MongoCRUD.Services
{
    public class BooksService : IBooksService
    {
        private readonly IMongoCollection<Books> _books;
        private readonly BooksConfiguration _settings;

        public BooksService(IOptions<BooksConfiguration> settings)
        {
            _settings = settings.Value;
            var _client = new MongoClient(_settings.ConnectionString);
            var _databaseName = _client.GetDatabase(_settings.DatabaseName);
            _books = _databaseName.GetCollection<Books>(_settings.BooksCollectionName);
        }

        public async Task CreateAsync(Books book) => await _books.InsertOneAsync(book);


        public async Task DeleteAsync(string id) => await _books.DeleteOneAsync(x => x.Id == id);

        public async Task<List<Books>> GetAllAsync() => await _books.Find(_ => true).ToListAsync();


        public async Task<Books> GetByIdAsync(string id) => await _books.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(string id, Books book) => await _books.ReplaceOneAsync(x => x.Id == id, book);
    }
}
