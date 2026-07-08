using FBS.Core.Entities;
using FBS.Core.Interfaces;
using FBS.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FBS.Infrastructure.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly FbsDbContext _context;
        private readonly DbSet<News> _news;
        public NewsRepository(FbsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _news = _context.Set<News>();
        }

        public async Task AddRangeAsync(IEnumerable<News> newsItem)
        {
            await _news.AddRangeAsync(newsItem);
        }

        public async Task<bool> ExistsByTitleAsync(string title, CancellationToken cancellationToken)
        {
            return await _news.AnyAsync(t=>t.Title == title, cancellationToken);
        }

        public async Task<List<News>> GetLatestNewsAsync(int count, CancellationToken cancellationToken)
        {
            return await _news
                .OrderByDescending(t => t.PublishedAt)
                .Take(count)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
