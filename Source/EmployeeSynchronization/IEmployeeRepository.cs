using System.Collections.Generic;

namespace Affecto.PositiveFeedback.EmployeeSynchronization
{
    public interface IEmployeeRepository
    {
        IReadOnlyCollection<IEmployee> GetEmployees();
    }
}
