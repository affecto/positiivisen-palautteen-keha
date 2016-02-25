using System;
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
            const string newEmployee1LastName = "North";
            const string newEmployee1FirstName = "Peter";
            const string newEmployee1Title = "CIO";
            const string newEmployee1Location = "NY";
            const string newEmployee1Organization = "HR";
            const string newEmployee1SubOrganization = "subHR";
            byte[] newEmployee1Picture = new byte[0];

            Guid newEmployee2Id = Guid.NewGuid();
            const string newEmployee2LastName = "Dean";
            const string newEmployee2FirstName = "James";
            const string newEmployee2Title = "CTO";
            const string newEmployee2Location = "LA";
            const string newEmployee2Organization = "IT";
            const string newEmployee2SubOrganization = "Installing";
            byte[] newEmployee2Picture = new byte[0];

            Guid oldEmployeeId = Guid.NewGuid();
            const string oldEmployeeLastName = "Travolta";
            const string oldEmployeeFirstName = "John";
            const string oldEmployeeTitle = "CFO";
            const string oldEmployeeLocation = "NJ";
            const string oldEmployeeOrganization = "Sanitation";
            const string oldEmployeeSubOrganization = "Vacuum cleaning";
            byte[] oldEmployeePicture = new byte[0];

            feedbackRepository.HasEmployee(newEmployee1Id).Returns(false);
            feedbackRepository.HasEmployee(newEmployee2Id).Returns(false);
            feedbackRepository.HasEmployee(oldEmployeeId).Returns(true);

            IEmployee newEmployee1 = CreateEmployeeMock(newEmployee1Id, newEmployee1LastName, newEmployee1FirstName, newEmployee1Title, newEmployee1Location, newEmployee1Organization, newEmployee1SubOrganization,
                newEmployee1Picture);
            IEmployee newEmployee2 = CreateEmployeeMock(newEmployee2Id, newEmployee2LastName, newEmployee2FirstName, newEmployee2Title, newEmployee2Location, newEmployee2Organization, newEmployee2SubOrganization,
                newEmployee2Picture);
            IEmployee oldEmployee = CreateEmployeeMock(oldEmployeeId, oldEmployeeLastName, oldEmployeeFirstName, oldEmployeeTitle, oldEmployeeLocation, oldEmployeeOrganization, oldEmployeeSubOrganization,
                oldEmployeePicture);
            employeeRepository.GetEmployees().Returns(new List<IEmployee> { newEmployee1, newEmployee2, oldEmployee });

            Employee oldActiveEmployee = new Employee(oldEmployeeId, oldEmployeeLastName, oldEmployeeFirstName, oldEmployeeTitle, oldEmployeeLocation, false, new List<string>());
            feedbackRepository.GetActiveEmployees().Returns(new List<Employee> { oldActiveEmployee });

            sut.Synchronize();

            feedbackRepository.Received(1).AddEmployee(newEmployee1Id, newEmployee1LastName, newEmployee1FirstName, newEmployee1Title, newEmployee1Location, newEmployee1Organization, newEmployee1SubOrganization,
                newEmployee1Picture);
            feedbackRepository.Received(1).AddEmployee(newEmployee2Id, newEmployee2LastName, newEmployee2FirstName, newEmployee2Title, newEmployee2Location, newEmployee2Organization, newEmployee2SubOrganization,
                newEmployee2Picture);
            feedbackRepository.DidNotReceive().AddEmployee(oldEmployeeId, oldEmployeeLastName, oldEmployeeFirstName, oldEmployeeTitle, oldEmployeeLocation, oldEmployeeOrganization, oldEmployeeSubOrganization,
                oldEmployeePicture);
        }

        [TestMethod]
        public void OldEmployeesAreUpdatedInFeedbackRepository()
        {
            Guid newEmployeeId = Guid.NewGuid();
            const string newEmployeeLastName = "North";
            const string newEmployeeFirstName = "Peter";
            const string newEmployeeTitle = "CIO";
            const string newEmployeeLocation = "NY";
            const string newEmployeeOrganization = "HR";
            const string newEmployeeSubOrganization = "People";
            byte[] newEmployeePicture = new byte[0];

            Guid oldEmployee1Id = Guid.NewGuid();
            const string oldEmployee1LastName = "Dean";
            const string oldEmployee1FirstName = "James";
            const string oldEmployee1Title = "CTO";
            const string oldEmployee1Location = "LA";
            const string oldEmployee1Organization = "IT";
            const string oldEmployee1SubOrganization = "Code";
            byte[] oldEmployee1Picture = new byte[0];

            Guid oldEmployee2Id = Guid.NewGuid();
            const string oldEmployee2LastName = "Travolta";
            const string oldEmployee2FirstName = "John";
            const string oldEmployee2Title = "CFO";
            const string oldEmployee2Location = "NJ";
            const string oldEmployee2Organization = "Sanitation";
            const string oldEmployee2SubOrganization = "Cleaning";
            byte[] oldEmployee2Picture = new byte[0];

            feedbackRepository.HasEmployee(newEmployeeId).Returns(false);
            feedbackRepository.HasEmployee(oldEmployee1Id).Returns(true);
            feedbackRepository.HasEmployee(oldEmployee2Id).Returns(true);

            IEmployee newEmployee = CreateEmployeeMock(newEmployeeId, newEmployeeLastName, newEmployeeFirstName, newEmployeeTitle, newEmployeeLocation, newEmployeeOrganization, newEmployeeSubOrganization, 
                newEmployeePicture);
            IEmployee oldEmployee1 = CreateEmployeeMock(oldEmployee1Id, oldEmployee1LastName, oldEmployee1FirstName, oldEmployee1Title, oldEmployee1Location, oldEmployee1Organization, oldEmployee1SubOrganization,
                oldEmployee1Picture);
            IEmployee oldEmployee2 = CreateEmployeeMock(oldEmployee2Id, oldEmployee2LastName, oldEmployee2FirstName, oldEmployee2Title, oldEmployee2Location, oldEmployee2Organization, oldEmployee2SubOrganization,
                oldEmployee2Picture);
            employeeRepository.GetEmployees().Returns(new List<IEmployee> { newEmployee, oldEmployee1, oldEmployee2 });

            Employee oldActiveEmployee1 = new Employee(oldEmployee1Id, oldEmployee1LastName, oldEmployee1FirstName, oldEmployee1Title, oldEmployee1Location, false, new List<string>());
            Employee oldActiveEmployee2 = new Employee(oldEmployee2Id, oldEmployee2LastName, oldEmployee2FirstName, oldEmployee2Title, oldEmployee2Location, false, new List<string>());
            feedbackRepository.GetActiveEmployees().Returns(new List<Employee> { oldActiveEmployee1, oldActiveEmployee2 });

            sut.Synchronize();

            feedbackRepository.Received(1).UpdateEmployee(oldEmployee1Id, oldEmployee1LastName, oldEmployee1FirstName, oldEmployee1Title, oldEmployee1Location, oldEmployee1Organization, oldEmployee1SubOrganization,
                oldEmployee1Picture);
            feedbackRepository.Received(1).UpdateEmployee(oldEmployee2Id, oldEmployee2LastName, oldEmployee2FirstName, oldEmployee2Title, oldEmployee2Location, oldEmployee2Organization, oldEmployee2SubOrganization,
                oldEmployee2Picture);
            feedbackRepository.DidNotReceive().UpdateEmployee(newEmployeeId, newEmployeeLastName, newEmployeeFirstName, newEmployeeTitle, newEmployeeLocation, newEmployeeOrganization, newEmployeeSubOrganization,
                newEmployeePicture);
        }

        [TestMethod]
        public void RemovedEmployeesAreDeactivatedInFeedbackRepository()
        {
            Guid oldEmployeeId = Guid.NewGuid();
            const string oldEmployeeLastName = "Travolta";
            const string oldEmployeeFirstName = "John";
            const string oldEmployeeTitle = "CFO";
            const string oldEmployeeLocation = "NJ";
            const string oldEmployeeOrganization = "Sanitation";
            const string oldEmployeeSubOrganization = "Cleaning";
            byte[] oldEmployeePicture = new byte[0];

            Guid oldRemovedEmployee1Id = Guid.NewGuid();
            const string oldRemovedEmployee1LastName = "Tyson";
            const string oldRemovedEmployee1FirstName = "Mike";
            const string oldRemovedEmployee1Title = "WBA";
            const string oldRemovedEmployee1Location = "LA";

            Guid oldRemovedEmployee2Id = Guid.NewGuid();
            const string oldRemovedEmployee2LastName = "North";
            const string oldRemovedEmployee2FirstName = "Peter";
            const string oldRemovedEmployee2Title = "P";
            const string oldRemovedEmployee2Location = "NY";

            feedbackRepository.HasEmployee(oldEmployeeId).Returns(true);
            feedbackRepository.HasEmployee(oldRemovedEmployee1Id).Returns(true);
            feedbackRepository.HasEmployee(oldRemovedEmployee2Id).Returns(true);

            IEmployee oldEmployee = CreateEmployeeMock(oldEmployeeId, oldEmployeeLastName, oldEmployeeFirstName, oldEmployeeTitle, oldEmployeeLocation, oldEmployeeOrganization, oldEmployeeSubOrganization, oldEmployeePicture);
            employeeRepository.GetEmployees().Returns(new List<IEmployee> { oldEmployee });

            Employee oldActiveEmployee1 = new Employee(oldEmployeeId, oldEmployeeLastName, oldEmployeeFirstName, oldEmployeeTitle, oldEmployeeLocation, false, new List<string>());
            Employee oldActiveEmployee2 = new Employee(oldRemovedEmployee1Id, oldRemovedEmployee1LastName, oldRemovedEmployee1FirstName, oldRemovedEmployee1Title, oldRemovedEmployee1Location, false, new List<string>());
            Employee oldActiveEmployee3 = new Employee(oldRemovedEmployee2Id, oldRemovedEmployee2LastName, oldRemovedEmployee2FirstName, oldRemovedEmployee2Title, oldRemovedEmployee2Location, false, new List<string>());
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
            const string employeeLastName = "Travolta";
            const string employeeFirstName = "John";
            const string employeeTitle = "CFO";
            const string employeeLocation = "NJ";
            const string employeeOrganization = "Sanitation";
            const string employeeSubOrganization = "Cleaning";
            byte[] employeePicture = new byte[0];

            feedbackRepository.HasEmployee(employeeId).Returns(true);

            IEmployee employee = CreateEmployeeMock(employeeId, employeeLastName, employeeFirstName, employeeTitle, employeeLocation, employeeOrganization, employeeSubOrganization, employeePicture);
            employeeRepository.GetEmployees().Returns(new List<IEmployee> { employee });

            feedbackRepository.GetActiveEmployees().Returns(new List<Employee>());

            sut.Synchronize();

            feedbackRepository.Received(1).UpdateEmployee(employeeId, employeeLastName, employeeFirstName, employeeTitle, employeeLocation, employeeOrganization, employeeSubOrganization, employeePicture);
        }

        private static IEmployee CreateEmployeeMock(Guid id, string lastName, string firstName, string title, string location, string organization, string subOrganization, byte[] picture)
        {
            var employee = Substitute.For<IEmployee>();
            employee.Id.Returns(id);
            employee.LastName.Returns(lastName);
            employee.FirstName.Returns(firstName);
            employee.Title.Returns(title);
            employee.Location.Returns(location);
            employee.Organization.Returns(organization);
            employee.SubOrganization.Returns(subOrganization);
            employee.Picture.Returns(picture);
            return employee;
        }
    }
}