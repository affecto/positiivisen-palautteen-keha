using System;
using Affecto.ActiveDirectoryService;
using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory
{
    internal class PrincipalMapper : OneWayMapper<IPrincipal,Employee>
    {
        private readonly IConfiguration configuration;

        public PrincipalMapper(IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            this.configuration = configuration;
        }

        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IPrincipal, Employee>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.NativeGuid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.Picture, opt => opt.MapFrom(src => (byte[]) src.AdditionalProperties[configuration.PictureProperty]))

                // todo
                .ForMember(dest => dest.Location, opt => opt.UseValue("City"))
                .ForMember(dest => dest.Organization, opt => opt.UseValue("IT"));
        }
    }
}
