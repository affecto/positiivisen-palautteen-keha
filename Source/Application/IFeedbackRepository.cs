using System;
using System.Collections.Generic;
using System.IO;

namespace Affecto.PositiveFeedback.Application
{
    public interface IFeedbackRepository
    {
        bool HasEmployee(Guid id);
        void AddEmployee(Guid id, string lastName, string firstName, string title, string location, string organization, string subOrganization, byte[] picture);
        void UpdateEmployee(Guid id, string lastName, string firstName, string title, string location, string organization, string subOrganization, byte[] picture);
        IReadOnlyCollection<Employee> GetActiveEmployees();
        IReadOnlyCollection<Employee> SearchActiveEmployees(string searchCriteria);
        IReadOnlyCollection<Employee> GetActiveEmployeesWithFeedback();
        IReadOnlyCollection<Employee> GetShuffledActiveEmployeesWithSingleFeedback();
        Employee GetEmployee(Guid id);
        MemoryStream GetEmployeePicture(Guid employeeId);
        void AddTextFeedback(Guid employeeId, string feedback);
        void DeactivateEmployee(Guid id);
    }
}