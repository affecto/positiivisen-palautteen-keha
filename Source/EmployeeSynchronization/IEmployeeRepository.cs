using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affecto.PositiveFeedback.EmployeeSynchronization
{
    public interface IEmployeeRepository
    {
        IEnumerable<IEmployee> GetEmployees();
    }
}
