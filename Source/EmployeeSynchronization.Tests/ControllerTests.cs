﻿using System;
using System.Collections.Generic;
using Affecto.PositiveFeedback.Application;
using Affecto.PositiveFeedback.EmployeeSynchronization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace EmployeeSynchronization.Tests
{
    [TestClass]
    public class ControllerTests
    {
        private Controller sut;
        private IFeedbackRepository feedbackRepository;
        private IEmployeeRepository employeeRepository;

        [TestInitialize]
        public void Setup()
        {
            feedbackRepository = Substitute.For<IFeedbackRepository>();
            employeeRepository = Substitute.For<IEmployeeRepository>();
            sut = new Controller(feedbackRepository, employeeRepository);
        }

        [TestMethod]
        public void NewEmployeesAreAddedToFeedbackRepository()
        {
            Guid newEmployee1Id = Guid.NewGuid();
            const string newEmployee1Name = "Peter";
            const string newEmployee1Location = "NY";
            const string newEmployee1Organization = "HR";
            byte[] newEmployee1Picture = new byte[0];

            Guid newEmployee2Id = Guid.NewGuid();
            const string newEmployee2Name = "James";
            const string newEmployee2Location = "LA";
            const string newEmployee2Organization = "IT";
            byte[] newEmployee2Picture = new byte[0];

            Guid oldEmployeeId = Guid.NewGuid();
            const string oldEmployeeName = "John";
            const string oldEmployeeLocation = "NJ";
            const string oldEmployeeOrganization = "Sanitation";
            byte[] oldEmployeePicture = new byte[0];

            feedbackRepository.HasEmployee(newEmployee1Id).Returns(false);
            feedbackRepository.HasEmployee(newEmployee2Id).Returns(false);
            feedbackRepository.HasEmployee(oldEmployeeId).Returns(true);

            IEmployee newEmployee1 = CreateEmployeeMock(newEmployee1Id, newEmployee1Name, newEmployee1Location, newEmployee1Organization, newEmployee1Picture);
            IEmployee newEmployee2 = CreateEmployeeMock(newEmployee2Id, newEmployee2Name, newEmployee2Location, newEmployee2Organization, newEmployee2Picture);
            IEmployee oldEmployee = CreateEmployeeMock(oldEmployeeId, oldEmployeeName, oldEmployeeLocation, oldEmployeeOrganization, oldEmployeePicture);
            employeeRepository.GetEmployees().Returns(new List<IEmployee> { newEmployee1, newEmployee2, oldEmployee });

            Employee oldActiveEmployee = new Employee(oldEmployeeId, oldEmployeeName, null, new List<string>());
            feedbackRepository.GetActiveEmployees().Returns(new List<Employee> { oldActiveEmployee });

            sut.Synchronize();

            feedbackRepository.Received(1).AddEmployee(newEmployee1Id, newEmployee1Name, newEmployee1Location, newEmployee1Organization, newEmployee1Picture);
            feedbackRepository.Received(1).AddEmployee(newEmployee2Id, newEmployee2Name, newEmployee2Location, newEmployee2Organization, newEmployee2Picture);
            feedbackRepository.DidNotReceive().AddEmployee(oldEmployeeId, oldEmployeeName, oldEmployeeLocation, oldEmployeeOrganization, oldEmployeePicture);
        }

        [TestMethod]
        public void OldEmployeesAreUpdatedInFeedbackRepository()
        {
            Guid newEmployeeId = Guid.NewGuid();
            const string newEmployeeName = "Peter";
            const string newEmployeeLocation = "NY";
            const string newEmployeeOrganization = "HR";
            byte[] newEmployeePicture = new byte[0];

            Guid oldEmployee1Id = Guid.NewGuid();
            const string oldEmployee1Name = "James";
            const string oldEmployee1Location = "LA";
            const string oldEmployee1Organization = "IT";
            byte[] oldEmployee1Picture = new byte[0];

            Guid oldEmployee2Id = Guid.NewGuid();
            const string oldEmployee2Name = "John";
            const string oldEmployee2Location = "NJ";
            const string oldEmployee2Organization = "Sanitation";
            byte[] oldEmployee2Picture = new byte[0];

            feedbackRepository.HasEmployee(newEmployeeId).Returns(false);
            feedbackRepository.HasEmployee(oldEmployee1Id).Returns(true);
            feedbackRepository.HasEmployee(oldEmployee2Id).Returns(true);

            IEmployee newEmployee = CreateEmployeeMock(newEmployeeId, newEmployeeName, newEmployeeLocation, newEmployeeOrganization, newEmployeePicture);
            IEmployee oldEmployee1 = CreateEmployeeMock(oldEmployee1Id, oldEmployee1Name, oldEmployee1Location, oldEmployee1Organization, oldEmployee1Picture);
            IEmployee oldEmployee2 = CreateEmployeeMock(oldEmployee2Id, oldEmployee2Name, oldEmployee2Location, oldEmployee2Organization, oldEmployee2Picture);
            employeeRepository.GetEmployees().Returns(new List<IEmployee> { newEmployee, oldEmployee1, oldEmployee2 });

            Employee oldActiveEmployee1 = new Employee(oldEmployee1Id, oldEmployee1Name, null, new List<string>());
            Employee oldActiveEmployee2 = new Employee(oldEmployee2Id, oldEmployee2Name, null, new List<string>());
            feedbackRepository.GetActiveEmployees().Returns(new List<Employee> { oldActiveEmployee1, oldActiveEmployee2 });

            sut.Synchronize();

            feedbackRepository.Received(1).UpdateEmployee(oldEmployee1Id, oldEmployee1Name, oldEmployee1Location, oldEmployee1Organization, oldEmployee1Picture);
            feedbackRepository.Received(1).UpdateEmployee(oldEmployee2Id, oldEmployee2Name, oldEmployee2Location, oldEmployee2Organization, oldEmployee2Picture);
            feedbackRepository.DidNotReceive().UpdateEmployee(newEmployeeId, newEmployeeName, newEmployeeLocation, newEmployeeOrganization, newEmployeePicture);
        }

        [TestMethod]
        public void RemovedEmployeesAreDeactivatedInFeedbackRepository()
        {
            Guid oldEmployeeId = Guid.NewGuid();
            const string oldEmployeeName = "John";
            const string oldEmployeeLocation = "NJ";
            const string oldEmployeeOrganization = "Sanitation";
            byte[] oldEmployeePicture = new byte[0];

            Guid oldRemovedEmployee1Id = Guid.NewGuid();
            const string oldRemovedEmployee1Name = "Mike";

            Guid oldRemovedEmployee2Id = Guid.NewGuid();
            const string oldRemovedEmployee2Name = "Peter";

            feedbackRepository.HasEmployee(oldEmployeeId).Returns(true);
            feedbackRepository.HasEmployee(oldRemovedEmployee1Id).Returns(true);
            feedbackRepository.HasEmployee(oldRemovedEmployee2Id).Returns(true);

            IEmployee oldEmployee = CreateEmployeeMock(oldEmployeeId, oldEmployeeName, oldEmployeeLocation, oldEmployeeOrganization, oldEmployeePicture);
            employeeRepository.GetEmployees().Returns(new List<IEmployee> { oldEmployee });

            Employee oldActiveEmployee1 = new Employee(oldEmployeeId, oldEmployeeName, null, new List<string>());
            Employee oldActiveEmployee2 = new Employee(oldRemovedEmployee1Id, oldRemovedEmployee1Name, null, new List<string>());
            Employee oldActiveEmployee3 = new Employee(oldRemovedEmployee2Id, oldRemovedEmployee2Name, null, new List<string>());
            feedbackRepository.GetActiveEmployees().Returns(new List<Employee> { oldActiveEmployee1, oldActiveEmployee2, oldActiveEmployee3 });

            sut.Synchronize();

            feedbackRepository.Received(1).DeactivateEmployee(oldRemovedEmployee1Id);
            feedbackRepository.Received(1).DeactivateEmployee(oldRemovedEmployee1Id);
            feedbackRepository.DidNotReceive().DeactivateEmployee(oldEmployeeId);
        }

        [TestMethod]
        public void DeactivatedEmployeesCanBeActivatedAgain()
        {
            Guid employeeId = Guid.NewGuid();
            const string employeeName = "John";
            const string employeeLocation = "NJ";
            const string employeeOrganization = "Sanitation";
            byte[] employeePicture = new byte[0];

            feedbackRepository.HasEmployee(employeeId).Returns(true);

            IEmployee employee = CreateEmployeeMock(employeeId, employeeName, employeeLocation, employeeOrganization, employeePicture);
            employeeRepository.GetEmployees().Returns(new List<IEmployee> { employee });

            feedbackRepository.GetActiveEmployees().Returns(new List<Employee>());

            sut.Synchronize();

            feedbackRepository.Received(1).UpdateEmployee(employeeId, employeeName, employeeLocation, employeeOrganization, employeePicture);
        }

        private static IEmployee CreateEmployeeMock(Guid id, string name, string location, string organization, byte[] picture)
        {
            var employee = Substitute.For<IEmployee>();
            employee.Id.Returns(id);
            employee.Name.Returns(name);
            employee.Location.Returns(location);
            employee.Organization.Returns(organization);
            employee.Picture.Returns(picture);
            return employee;
        }
    }
}
