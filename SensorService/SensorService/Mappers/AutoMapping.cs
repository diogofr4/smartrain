using AutoMapper;
using Infrastructure.Entities;
using SensorService.Models;

namespace SensorService.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Measurement, MeasurementEntity>()
                .ForMember(dest => dest.SensorId, opt => opt.MapFrom(src => src.SensorId))
                .ForMember(dest => dest.Luminosity, opt => opt.MapFrom(src => src.Luminosity))
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Humidity))
                .ForMember(dest => dest.Rain, opt => opt.MapFrom(src => src.Rain))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Temperature))
                .ForMember(dest => dest.ReadingDateTime, opt => opt.MapFrom(src => src.ReadingDateTime));
        }
    }
}
