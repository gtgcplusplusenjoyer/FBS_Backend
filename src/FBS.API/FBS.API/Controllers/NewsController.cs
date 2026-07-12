using FBS.Application.Dto.News;
using FBS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FBS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
         
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetLatestNews(
            [FromQuery] GetLatestNewsRequest request,
            CancellationToken cancellationToken = default)
        {
            var news = await _newsService.GetLatestNewsAsync(request.Count, cancellationToken);
            return Ok(new { data = news });
        }
         
        [HttpPost("sync")]
        [Authorize]
        public async Task<IActionResult> SyncNews(CancellationToken cancellationToken)
        {
            await _newsService.SyncNewsFromExternalSourceAsync(cancellationToken);
            return Ok(new { message = "Синхронизация новостей запущена." });
        }
    }
}