using AutoMapper;
using DTO;
using Microsoft.Extensions.Logging.Abstractions;
using WebUI.Models;

namespace WebUI.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ContactDTO, ContactViewModel>().ReverseMap();
                cfg.CreateMap<PhoneDTO, PhoneViewModel>().ReverseMap();
            }, NullLoggerFactory.Instance);

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
