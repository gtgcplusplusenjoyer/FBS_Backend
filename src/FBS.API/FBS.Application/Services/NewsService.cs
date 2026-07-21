using AutoMapper;
using FBS.Application.Dto.News;
using FBS.Application.Interfaces;
using FBS.Core.Entities;
using FBS.Core.Interfaces;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace FBS.Application.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<NewsService> _logger;
        private readonly HttpClient _httpClient;
        public NewsService(
        INewsRepository newsRepository,
        IMapper mapper,
        ILogger<NewsService> logger,
        IHttpClientFactory httpClientFactory)
        {
            _repository = newsRepository;
            _mapper = mapper;
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");
        }

        public async Task<List<NewsResponseDto>> GetLatestNewsAsync(int count, CancellationToken cancellationToken)
        {
            var newsItems = await _repository.GetLatestNewsAsync(count, cancellationToken);

            return _mapper.Map<List<NewsResponseDto>>(newsItems);
        }
        public async Task SyncNewsFromExternalSourceAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Начинается синхронизация новостей с внешнего сайта.");

            try
            {
                var parsedNews = await ParseNewsFromWebsiteAsync(cancellationToken);
                if (!parsedNews.Any())
                {
                    _logger.LogWarning("Новости не найдены на сайте.");
                    return;
                }

                int addedCount = 0;
                foreach (var parsedItem in parsedNews)
                {
                    if (await _repository.ExistsByTitleAsync(parsedItem.Title, cancellationToken))
                    {
                        _logger.LogDebug($"Новость '{parsedItem.Title}' уже существует в БД.");
                        continue;
                    }

                    var newsEntity = new News
                    {
                        Id = Guid.NewGuid(),
                        Title = parsedItem.Title,
                        ImageUrl = parsedItem.ImageUrl,
                        PublishedAt = DateTime.UtcNow
                    };

                    await _repository.AddRangeAsync(new[] { newsEntity });
                    addedCount++;
                }

                if (addedCount > 0)
                {
                    await _repository.SaveChangesAsync(cancellationToken);
                    _logger.LogInformation("Синхронизация завершена. Добавлено {AddedCount} новостей.", addedCount);
                }
                else
                {
                    _logger.LogInformation("Новых новостей для добавления не найдено.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Критическая ошибка при синхронизации новостей.");
            }
        }

        private async Task<List<ParsedNewsDto>> ParseNewsFromWebsiteAsync(CancellationToken cancellationToken)
        {
            var result = new List<ParsedNewsDto>();
            var url = "https://bodybuilding-and-fitness.ru/news";

            try
            {
                _logger.LogInformation("Загрузка страницы {Url}", url);
                var response = await _httpClient.GetAsync(url, cancellationToken);
                response.EnsureSuccessStatusCode();
                var html = await response.Content.ReadAsStringAsync(cancellationToken);

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var titleNodes = doc.DocumentNode.SelectNodes("//h2 | //h3 | //a[contains(@class, 'post-title')] | //a[contains(@class, 'entry-title')]");

                if (titleNodes == null || !titleNodes.Any())
                {
                    _logger.LogWarning("Заголовки новостей не найдены на странице. Проверьте XPath селекторы.");
                    return result;
                }

                _logger.LogInformation("Найдено {Count} потенциальных заголовков новостей", titleNodes.Count);

                foreach (var node in titleNodes)
                {
                    try
                    {
                        var title = node.InnerText.Trim();
                        if (string.IsNullOrEmpty(title) || title.Length < 3)
                            continue;

                        var parent = node.ParentNode;
                        var imageNode = parent?.SelectSingleNode(".//img");

                        var imageUrl = imageNode?.GetAttributeValue("data-src", null)
                                       ?? imageNode?.GetAttributeValue("src", "")
                                       ?? "";

                        if (string.IsNullOrEmpty(imageUrl) && parent != null)
                        {
                            var container = parent.ParentNode;
                            imageNode = container?.SelectSingleNode(".//img");
                            imageUrl = imageNode?.GetAttributeValue("data-src", null)
                                       ?? imageNode?.GetAttributeValue("src", "")
                                       ?? "";
                        }

                        if (!string.IsNullOrEmpty(imageUrl) && !imageUrl.StartsWith("http"))
                        {
                            if (imageUrl.StartsWith("//"))
                                imageUrl = "https:" + imageUrl;
                            else if (imageUrl.StartsWith("/"))
                                imageUrl = "https://bodybuilding-and-fitness.ru" + imageUrl;
                            else
                                imageUrl = "https://bodybuilding-and-fitness.ru/" + imageUrl;
                        }

                        result.Add(new ParsedNewsDto(title, imageUrl));
                        _logger.LogDebug("Распарсена новость: {Title}", title);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Ошибка при парсинге отдельной новости.");
                    }
                }

                _logger.LogInformation("Успешно распарсено {Count} новостей", result.Count);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Ошибка HTTP при загрузке страницы {Url}", url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при парсинге страницы {Url}", url);
            }

            return result;
        }

    }
}
