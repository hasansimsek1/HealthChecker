using AutoMapper;
using HealthChecker.Data.Implementations.Dtos;
using HealthChecker.Data.Implementations.Entities;

namespace HealthChecker.Data.Configurations
{
    /// <summary>
    /// Configurations for AutoMapper.
    /// </summary>
    public class AutoMapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            var autoMapperConfig = new MapperConfiguration(x => x.AddProfile<AutoMapperMappings>());
            return autoMapperConfig;
        }
    }

    /// <summary>
    /// Profile for AutoMapper
    /// </summary>
    public class AutoMapperMappings : Profile
    {
        public AutoMapperMappings()
        {
            CreateMap<CheckResult, CheckResultDto>();
            CreateMap<TargetApp, TargetAppDto>();

            CreateMap<CheckResultDto, CheckResult>();
            CreateMap<TargetAppDto, TargetApp>();
        }
    }
}
