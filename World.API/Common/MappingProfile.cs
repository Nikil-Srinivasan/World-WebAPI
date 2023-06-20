using AutoMapper;
using World.API.DTO.Country;
using World.API.DTO.State;
using World.API.Models;

namespace World.API.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //     Destination, Source
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();

            CreateMap<States, CreateStatesDto>().ReverseMap();
            CreateMap<States, StatesDto>().ReverseMap();
            CreateMap<States, UpdateStatesDto>().ReverseMap();
        }
    }
}
