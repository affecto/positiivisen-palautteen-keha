using System;
using System.Collections.Generic;
using System.IO;

namespace Affecto.PositiveFeedback.Application
{
    public interface IFeedbackRepository
    {
        bool HasEmployee(Guid id);
        void AddEmployee(Guid id, string name, string location, string organization, string subOrganization, byte[] picture);
        void UpdateEmployee(Guid id, string name, string location, string organization, string subOrganization, byte[] picture);
        IReadOnlyCollection<Employee> GetActiveEmployees();
        IReadOnlyCollection<Employee> GetActiveEmployeesWithFeedback();
        Employee GetEmployee(Guid id);
        MemoryStream GetEmployeePicture(Guid employeeId);
        void AddTextFeedback(Guid employeeId, string feedback);
        void DeactivateEmployee(Guid id);
    }
}