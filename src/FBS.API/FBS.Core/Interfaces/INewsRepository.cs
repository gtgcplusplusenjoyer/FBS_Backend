using FBS.Core.Entities;

namespace FBS.Core.Interfaces
{
    public interface INewsRepository
    {
        Task AddRangeAsync(IEnumerable<News> newsItem, CancellationToken cancellationToken);
        Task<List<News>> GetLatestNewsAsync(int count, CancellationToken cancellationToken);
        Task<bool> ExistsByTitleAsync(string title, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
