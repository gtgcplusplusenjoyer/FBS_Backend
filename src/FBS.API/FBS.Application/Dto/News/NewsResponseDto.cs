namespace FBS.Application.Dto.News
{
    public record NewsResponseDto(Guid Id, string Title, string ImageUrl, DateTime PublishedAt);
}
