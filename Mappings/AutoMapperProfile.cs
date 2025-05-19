using AutoMapper;
using FootballerStatsApi.Dtos;
using FootballerStatsApi.Models;

namespace FootballerStatsApi.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Footballer mappings
            CreateMap<Footballer, FootballerDto>().ReverseMap();
            CreateMap<AddPlayerDto, Footballer>();
            CreateMap<UpdatePlayerDto, Footballer>();
            CreateMap<Footballer, FootballerBasicDto>();

            CreateMap<AddMatchStatisticDto, MatchStatistic>();
            CreateMap<MatchStatistic, MatchStatisticDto>();
            CreateMap<UpdateMatchStatisticDto, MatchStatistic>();
        }
    }
}
