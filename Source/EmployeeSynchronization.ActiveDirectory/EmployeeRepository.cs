using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.ActiveDirectoryService;
using Affecto.Mapping;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly IActiveDirectoryService activeDirectoryService;
        private readonly IConfiguration configuration;
        private readonly IMapper<IPrincipal, Employee> principalMapper;

        public EmployeeRepository(IActiveDirectoryService activeDirectoryService, IConfiguration configuration, IMapper<IPrincipal, Employee> principalMapper)
        {
            if (activeDirectoryService == null)
            {
                throw new ArgumentNullException(nameof(activeDirectoryService));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            if (principalMapper == null)
            {
                throw new ArgumentNullException(nameof(principalMapper));
            }

            this.activeDirectoryService = activeDirectoryService;
            this.configuration = configuration;
            this.principalMapper = principalMapper;
        }

        public IReadOnlyCollection<IEmployee> GetEmployees()
        {
            var principals = new List<IPrincipal>();
            var additionalProperties = new[]
            {
                configuration.PictureUrlProperty,
                configuration.LocationProperty,
                configuration.OrganizationProperty,
                configuration.SubOrganizationProperty
            };

            foreach (string group in configuration.Groups)
            {
                IEnumerable<IPrincipal> groupPrincipals = activeDirectoryService
                    .GetGroupMembers(group, true, additionalProperties)
                    .Where(p => principals.All(e => e.NativeGuid != p.NativeGuid));
                principals.AddRange(groupPrincipals);
            }

            return principalMapper.Map(principals).ToList();
        }
    }
}