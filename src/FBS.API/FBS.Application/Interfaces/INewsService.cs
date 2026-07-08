using FBS.Application.Dto.News;

namespace FBS.Application.Interfaces
{
    public interface INewsService
    {
        Task<List<NewsResponseDto>> GetLatestNewsAsync(int count, CancellationToken cancellationToken);
        Task SyncNewsFromExternalSourceAsync(CancellationToken cancellationToken);
    }
}
