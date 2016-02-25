using System;
using Affecto.ActiveDirectoryService;
using Affecto.Mapping;
using Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory.EmployeePicture;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory
{
    internal class PrincipalMapper : IMapper<IPrincipal, Employee>
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
            string lastName = source.AdditionalProperties.ContainsKey(configuration.LastNameProperty) ?
                source.AdditionalProperties[configuration.LastNameProperty] as string : null;
            string firstName = source.AdditionalProperties.ContainsKey(configuration.FirstNameProperty) ?
                source.AdditionalProperties[configuration.FirstNameProperty] as string : null;
            string title = source.AdditionalProperties.ContainsKey(configuration.TitleProperty) ?
                source.AdditionalProperties[configuration.TitleProperty] as string : null;
            string organization = source.AdditionalProperties.ContainsKey(configuration.OrganizationProperty) ?
                source.AdditionalProperties[configuration.OrganizationProperty] as string : null;
            string subOrganization = source.AdditionalProperties.ContainsKey(configuration.SubOrganizationProperty) ?
                source.AdditionalProperties[configuration.SubOrganizationProperty] as string : null;
            string location = source.AdditionalProperties.ContainsKey(configuration.LocationProperty) ?
                source.AdditionalProperties[configuration.LocationProperty] as string : null;

            string pictureUrl = source.AdditionalProperties.ContainsKey(configuration.PictureUrlProperty) ?
                source.AdditionalProperties[configuration.PictureUrlProperty] as string : null;
            byte[] picture = pictureHandler.DownloadAndResizePicture(pictureUrl);

            return new Employee
            {
                Id = source.NativeGuid,
                LastName = Trim(lastName),
                FirstName = Trim(firstName),
                Title = Trim(title),
                Location = Trim(location),
                Organization = Trim(organization),
                SubOrganization = Trim(subOrganization),
                Picture = picture
            };
        }

        private static string Trim(string value)
        {
            value = value?.Trim();

            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return value;
        }
    }
}