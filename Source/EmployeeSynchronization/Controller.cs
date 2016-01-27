using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Affecto.PositiveFeedback.Application;

namespace Affecto.PositiveFeedback.EmployeeSynchronization
{
    public class Controller
    {
        private readonly IFeedbackRepository feedbackRepository;
        private readonly IEmployeeRepository employeeRepository;

        public Controller(IFeedbackRepository feedbackRepository, IEmployeeRepository employeeRepository)
        {
            if (feedbackRepository == null)
            {
                throw new ArgumentNullException(nameof(feedbackRepository));
            }
            if (employeeRepository == null)
            {
                throw new ArgumentNullException(nameof(employeeRepository));
            }

            this.feedbackRepository = feedbackRepository;
            this.employeeRepository = employeeRepository;
        }

        public void Synchronize()
        {
            IReadOnlyCollection<IEmployee> employees = employeeRepository.GetEmployees();

            foreach (Employee employeeWithFeedback in feedbackRepository.GetActiveEmployees().Where(emplWithFeedback => !employees.Any(e => e.Id.Equals(emplWithFeedback.Id))))
            {
                feedbackRepository.DeactivateEmployee(employeeWithFeedback.Id);
            }

            foreach (IEmployee employee in employees)
            {
                AddOrUpdateEmployee(employee.Id, employee.Name, employee.Location, employee.Organization, employee.Picture);
            }
        }

        private void AddOrUpdateEmployee(Guid id, string name, string location, string organization, byte[] picture)
        {
            if (feedbackRepository.HasEmployee(id))
            {
                feedbackRepository.UpdateEmployee(id, name, location, organization, picture);
            }
            else
            {
                feedbackRepository.AddEmployee(id, name, location, organization, picture);
            }
        }
    }
}
