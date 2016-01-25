using System;
using System.Collections.Generic;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        public IReadOnlyCollection<IEmployee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee { Id = Guid.Parse("780AE7D2-880C-42BE-BFB4-342678D06AB0"), Name = "Antti Affectolainen", Location = "Turku", Organization = "Affecto" },
                new Employee { Id = Guid.Parse("8166B820-5509-4169-AA89-1E1CD719ADFB"), Name = "Katja Karttakeskuslainen", Location = "Helsinki", Organization = "Karttakeskus" }
            };
        }
    }
}
