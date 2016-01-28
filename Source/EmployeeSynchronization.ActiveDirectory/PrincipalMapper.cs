using System;
using Affecto.ActiveDirectoryService;
using Affecto.Mapping;
using Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory.EmployeePicture;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory
{
    internal class PrincipalMapper : IMapper<IPrincipal,Employee>
    {
        private readonly IConfiguration configuration;
        private readonly PictureHandler pictureHandler;

        public PrincipalMapper(IConfiguration configuration, PictureHandler pictureHandler)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            if (pictureHandler == null)
            {
                throw new ArgumentNullException(nameof(pictureHandler));
            }
            this.configuration = configuration;
            this.pictureHandler = pictureHandler;
        }

        public Employee Map(IPrincipal source)
        {
            string pictureUrl = source.AdditionalProperties.ContainsKey(configuration.PictureUrlProperty) ?
                source.AdditionalProperties[configuration.PictureUrlProperty] as string : null;
            byte[] picture = pictureHandler.DownloadAndResizePicture(pictureUrl);

            return new Employee
            {
                Id = source.NativeGuid,
                Name = source.DisplayName,
                Picture = picture
            };
        }
    }
}
