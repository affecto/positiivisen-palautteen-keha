﻿using System;
using System.Collections.Generic;

namespace Affecto.PositiveFeedback.Application
{
    public interface IFeedbackRepository
    {
        bool HasEmployee(Guid id);
        void AddEmployee(Guid id, string name, string location, string organization, byte[] picture);
        void UpdateEmployee(Guid id, string name, string location, string organization, byte[] picture);
        IEnumerable<Employee> GetActiveEmployees();
        IEnumerable<Employee> GetActiveEmployeesWithFeedback();
        Employee GetEmployee(Guid id);
        void AddTextFeedback(Guid employeeId, string feedback);
        void DeactivateEmployee(Guid id);
    }
}