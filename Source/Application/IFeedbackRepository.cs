using System;
using System.Collections.Generic;

namespace Affecto.PositiveFeedback.Application
{
    public interface IFeedbackRepository
    {
        bool HasEmployee(Guid id);
        void AddEmployee(Guid id, string name, string location, string organization);
        void UpdateEmployee(Guid id, string name, string location, string organization);
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployee(Guid id);
        void AddTextFeedback(Guid employeeId, string feedback);
    }
}