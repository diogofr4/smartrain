using AutoMapper;
using Domain.Entities;
using Infrastructure.Entities;
using SensorService.Models;
using UserService.Models;

namespace SensorService.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<PlantEntity, Plant>();

            CreateMap<TaskEntity, UserService.Models.Task>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StartDateTime, opt => opt.MapFrom(src => src.StartDateTime))
                .ForMember(dest => dest.EndDateTime, opt => opt.MapFrom(src => src.EndDateTime))
                .ForMember(dest => dest.ResponsibleUser, opt => opt.MapFrom(src => src.ResponsibleUser));

            CreateMap<UserService.Models.Task, TaskEntity>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                 .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                 .ForMember(dest => dest.StartDateTime, opt => opt.MapFrom(src => src.StartDateTime))
                 .ForMember(dest => dest.EndDateTime, opt => opt.MapFrom(src => src.EndDateTime))
                 .ForMember(dest => dest.ResponsibleUser, opt => opt.MapFrom(src => src.ResponsibleUser));

            CreateMap<MeasurementEntity, Measurement>();

            CreateMap<IrrigationConfigEntity, IrrigationConfig>();

            CreateMap<ChatGroupsEntity, ChatGroups>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<ChatGroups, ChatGroupsEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<GroupParticipantsEntity, GroupParticipants>()
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId))
                .ForMember(dest => dest.ParticipantName, opt => opt.MapFrom(src => src.ParticipantName));

            CreateMap<GroupParticipants, GroupParticipantsEntity>()
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId))
                .ForMember(dest => dest.ParticipantName, opt => opt.MapFrom(src => src.ParticipantName));

            CreateMap<GroupMessagesEntity, GroupMessages>()
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId))
                .ForMember(dest => dest.ParticipantName, opt => opt.MapFrom(src => src.ParticipantName))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.MessageDate, opt => opt.MapFrom(src => src.MessageDate));

            CreateMap<GroupMessages, GroupMessagesEntity>()
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId))
                .ForMember(dest => dest.ParticipantName, opt => opt.MapFrom(src => src.ParticipantName))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.MessageDate, opt => opt.MapFrom(src => src.MessageDate)); ;

            CreateMap<CreatePlantModel, PlantEntity>()
                .ForMember(dest => dest.IrrigationConfig, opt => opt.MapFrom(src => SetIrrigationConfigEntity(src)))
                .ForMember(dest => dest.SensorId, opt => opt.MapFrom(src => src.SensorId))
                .ForMember(dest => dest.PlantName, opt => opt.MapFrom(src => src.PlantName))
                .ForMember(dest => dest.Measurements, opt => opt.MapFrom(src => new List<MeasurementEntity>()))
                .ForMember(dest => dest.PlantId, opt => opt.MapFrom(src => src.PlantId));
        }

        private IrrigationConfigEntity SetIrrigationConfigEntity(CreatePlantModel model)
        {
            var irrigationConfig = new IrrigationConfigEntity()
            {
                HumidityMin = (int)model.MinHumidity,
                TemperatureMax = (int)model.MaxTemperature,
            };

            return irrigationConfig;
        }
    }
}
