using Music.Library.Service.Entities;

namespace Music.Library.Service.Repositories
{
    public interface IItemsRepository
    {
        Task CreateAsync(Album entity);
        Task<IReadOnlyCollection<Album>> GetAllAsync();
        Task<Album> GetAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Album entity);
    }
}
