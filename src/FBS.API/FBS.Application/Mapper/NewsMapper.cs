using AutoMapper;
using FBS.Application.Dto.News;
using FBS.Core.Entities;

namespace FBS.Application.Mapper
{
    public class NewsMapper : Profile
    {
        public NewsMapper()
        {
            CreateMap<News, NewsResponseDto>();
        }
    }
}
