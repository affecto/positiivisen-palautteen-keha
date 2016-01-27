using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Affecto.ActiveDirectoryService;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly IActiveDirectoryService activeDirectoryService;
        private readonly IConfiguration configuration;

        public EmployeeRepository(IActiveDirectoryService activeDirectoryService, IConfiguration configuration)
        {
            if (activeDirectoryService == null)
            {
                throw new ArgumentNullException(nameof(activeDirectoryService));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            this.activeDirectoryService = activeDirectoryService;
            this.configuration = configuration;
        }

        public IReadOnlyCollection<IEmployee> GetEmployees()
        {
            var principals = new List<IPrincipal>();

            foreach (string group in configuration.Groups)
            {
                IEnumerable<IPrincipal> groupPrincipals = activeDirectoryService
                    .GetGroupMembers(group, true, new[] { configuration.PictureProperty })
                    .Where(p => principals.All(e => e.NativeGuid != p.NativeGuid));
                principals.AddRange(groupPrincipals);
            }

            var employees = new List<IEmployee>();

            foreach (IPrincipal principal in principals)
            {
                byte[] picture = null;

                if (principal.AdditionalProperties.ContainsKey(configuration.PictureProperty))
                {
                    string pictureUrl = principal.AdditionalProperties[configuration.PictureProperty] as string;
                    if (!string.IsNullOrWhiteSpace(pictureUrl))
                    {
                        using (Stream stream = GetEmployeePicture(principal.AdditionalProperties[configuration.PictureProperty].ToString()))
                        {
                            if (stream != null)
                            {
                                EmployeePicture originalPicture = new EmployeePicture(stream);
                                using (MemoryStream resizedPicture = originalPicture.GetResizedPicture(200, 300))
                                {
                                    picture = resizedPicture.ToArray();
                                }
                            }
                        }
                    }
                }

                employees.Add(new Employee
                {
                    Id = principal.NativeGuid,
                    Name = principal.DisplayName,
                    Picture = picture
                });
            }

            return employees;
        }

        private static Stream GetEmployeePicture(string pictureUrl)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.UseDefaultCredentials = true;
                    byte[] data = webClient.DownloadData(pictureUrl);
                    return new MemoryStream(data);
                }
            }
            catch (Exception)
                {
                return null;
            }
        }
    }
}