using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.ActiveDirectoryService;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly IActiveDirectoryService activeDirectoryService;

        public EmployeeRepository(IActiveDirectoryService activeDirectoryService)
        {
            if (activeDirectoryService == null)
            {
                throw new ArgumentNullException(nameof(activeDirectoryService));
            }
            this.activeDirectoryService = activeDirectoryService;
        }

        public IReadOnlyCollection<IEmployee> GetEmployees()
        {
            IEnumerable<IPrincipal> principals = activeDirectoryService.GetGroupMembers("", true, new[] { "extensionAttribute8" });

            return principals
                .Select(p => new Employee
                {
                    Id = p.NativeGuid,
                    Name = p.DisplayName
                })
                .ToList();
        }
    }
}