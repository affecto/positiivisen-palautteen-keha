﻿using System;
using System.Collections.Generic;
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

            foreach (Employee employeeWithFeedback in
                feedbackRepository.GetActiveEmployees().Where(emplWithFeedback => !employees.Any(e => e.Id.Equals(emplWithFeedback.Id))))
            {
                feedbackRepository.DeactivateEmployee(employeeWithFeedback.Id);
            }

            foreach (IEmployee employee in employees)
            {
                AddOrUpdateEmployee(employee.Id, employee.LastName, employee.FirstName, employee.Title, employee.Location, employee.Organization,
                    employee.SubOrganization, employee.Picture);
            }
        }

        private void AddOrUpdateEmployee(Guid id, string lastName, string firstName, string title, string location, string organization, string subOrganization,
            byte[] picture)
        {
            if (feedbackRepository.HasEmployee(id))
            {
                feedbackRepository.UpdateEmployee(id, lastName, firstName, title, location, organization, subOrganization, picture);
            }
            else
            {
                feedbackRepository.AddEmployee(id, lastName, firstName, title, location, organization, subOrganization, picture);
            }
        }
    }
}