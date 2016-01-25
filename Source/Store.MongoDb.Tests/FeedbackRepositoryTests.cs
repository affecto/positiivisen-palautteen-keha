using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using NSubstitute;

namespace Affecto.PositiveFeedback.Store.MongoDb.Tests
{
    [TestClass]
    public class FeedbackRepositoryTests
    {
        private FeedbackRepository sut;
        private IMongoCollection<Employee> employees;

        [TestInitialize]
        public void Setup()
        {
            ICollection<Employee> dbEmployees = Substitute.For<ICollection<Employee>>();
            employees = Substitute.For<IMongoCollection<Employee>>();
            dbEmployees.Load().Returns(employees);
            sut = new FeedbackRepository(dbEmployees);
        }

        [TestMethod]
        public void EmptyTextFeedbackIsNotAdded()
        {
            sut.AddTextFeedback(Guid.NewGuid(), string.Empty);

            employees.DidNotReceive().UpdateOne(Arg.Any<FilterDefinition<Employee>>(), Arg.Any<UpdateDefinition<Employee>>());
        }

        [TestMethod]
        public void WhiteSpaceTextFeedbackIsNotAdded()
        {
            sut.AddTextFeedback(Guid.NewGuid(), " ");

            employees.DidNotReceive().UpdateOne(Arg.Any<FilterDefinition<Employee>>(), Arg.Any<UpdateDefinition<Employee>>());
        }

        [TestMethod]
        public void NullTextFeedbackIsNotAdded()
        {
            sut.AddTextFeedback(Guid.NewGuid(), null);

            employees.DidNotReceive().UpdateOne(Arg.Any<FilterDefinition<Employee>>(), Arg.Any<UpdateDefinition<Employee>>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeWithAnEmptyIdCannotBeAdded()
        {
            sut.AddEmployee(Guid.Empty, "Jeff", "LA", "Management");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeWithAnEmptyNameCannotBeAdded()
        {
            sut.AddEmployee(Guid.NewGuid(), string.Empty, "LA", "Management");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeWithNullNameCannotBeAdded()
        {
            sut.AddEmployee(Guid.NewGuid(), null, "LA", "Management");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeWithAnEmptyIdCannotBeUpdated()
        {
            sut.UpdateEmployee(Guid.Empty, "Jeff", "LA", "Management");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeWithAnEmptyNameCannotBeUpdated()
        {
            sut.UpdateEmployee(Guid.NewGuid(), string.Empty, "LA", "Management");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeWithNullNameCannotBeUpdated()
        {
            sut.UpdateEmployee(Guid.NewGuid(), null, "LA", "Management");
        }

        [TestMethod]
        public void AddEmployee()
        {
            Guid id = Guid.NewGuid();
            const string name = "Matt";
            const string organization = "cleaning";
            const string location = "London";

            sut.AddEmployee(id, name, location, organization);

            employees.Received(1).InsertOne(Arg.Is<Employee>(e => e.Id.Equals(id) && e.Name.Equals(name) && e.Location.Equals(location) && e.Organization.Equals(organization)));
        }

        [TestMethod]
        public void AddedEmployeesAreActive()
        {
            sut.AddEmployee(Guid.NewGuid(), "Matt", "bosses", "Madrid");

            employees.Received(1).InsertOne(Arg.Is<Employee>(e => e.Active));
        }

        [TestMethod]
        public void GetEmployeeWhenEmployeeIsNotFound()
        {
            Application.Employee result = sut.GetEmployee(Guid.NewGuid());

            Assert.IsNull(result);
        }
    }
}
