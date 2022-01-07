using MongoCRUD.Models;

namespace MongoCRUD.Services
{
    public interface IBooksService
    {
        Task<List<Books>> GetAllAsync();
        Task<Books> GetByIdAsync(string id);
        Task CreateAsync(Books book);
        Task UpdateAsync(string id, Books book);
        Task DeleteAsync(string id);
    }
}
