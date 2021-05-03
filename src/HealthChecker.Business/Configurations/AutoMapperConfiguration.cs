using AutoMapper;
using HealthChecker.Business.ViewModels;
using HealthChecker.Persistence.Dtos;
using HealthChecker.Persistence.Entities;

namespace HealthChecker.Business.Configurations
{
    /// <summary>
    /// Configurations for AutoMapper.
    /// </summary>
    public class AutoMapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            return new MapperConfiguration(x => x.AddProfile<AutoMapperMappings>());
        }
    }

    /// <summary>
    /// Profile for AutoMapper
    /// </summary>
    public class AutoMapperMappings : Profile
    {
        public AutoMapperMappings()
        {
            CreateMap<HealthCheck, HealthCheckDto>();
            CreateMap<Target, TargetDto>();

            CreateMap<HealthCheckDto, HealthCheck>();
            CreateMap<TargetDto, Target>();

            CreateMap<HealthCheckDto, HealthCheckViewModel>();
            CreateMap<HealthCheckViewModel, HealthCheckDto>();
        }
    }
}
