using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Affecto.Mapping;
using Affecto.PositiveFeedback.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Affecto.PositiveFeedback.Api.Tests
{
    [TestClass]
    public class FeedbackControllerTests
    {
        private FeedbackController sut;
        private MapperFactory mapperFactory;
        private IFeedbackRepository repository;

        [TestInitialize]
        public void Setup()
        {
            repository = Substitute.For<IFeedbackRepository>();
            mapperFactory = Substitute.For<MapperFactory>();
            sut = new FeedbackController(repository, mapperFactory);
        }

        [TestMethod]
        public void GetEmployeesWithFeedback()
        {
            IMapper<Application.Employee, Employee> employeeMapper = CreateEmployeeMapperMock();
            var appEmployee = new Application.Employee(Guid.NewGuid(), "name", null);
            var apiEmployee = new Employee();
            employeeMapper.Map(appEmployee).Returns(apiEmployee);
            repository.GetActiveEmployeesWithFeedback().Returns(new List<Application.Employee> { appEmployee });

            var result = sut.GetEmployeesWithFeedback() as OkNegotiatedContentResult<IEnumerable<Employee>>;

            Assert.IsNotNull(result);
            Assert.AreSame(apiEmployee, result.Content.Single());
        }

        [TestMethod]
        public void GetEmployees()
        {
            IMapper<Application.Employee, Employee> employeeMapper = CreateEmployeeMapperMock();
            var appEmployee = new Application.Employee(Guid.NewGuid(), "name", null);
            var apiEmployee = new Employee();
            employeeMapper.Map(appEmployee).Returns(apiEmployee);
            repository.GetActiveEmployees().Returns(new List<Application.Employee> { appEmployee });

            var result = sut.GetEmployees() as OkNegotiatedContentResult<IEnumerable<Employee>>;

            Assert.IsNotNull(result);
            Assert.AreSame(apiEmployee, result.Content.Single());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IdCannotBeEmptyWhenGettingEmployee()
        {
            sut.GetEmployee(Guid.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeIdCannotBeEmptyWhenGivingFeedback()
        {
            sut.GiveEmployeeTextFeedback(Guid.Empty, "good guy!");
        }

        [TestMethod]
        public void GetEmployee()
        {
            Guid id = Guid.NewGuid();
            IMapper<Application.Employee, Employee> employeeMapper = CreateEmployeeMapperMock();
            var appEmployee = new Application.Employee(id, "name", null);
            var apiEmployee = new Employee();
            employeeMapper.Map(appEmployee).Returns(apiEmployee);
            repository.GetEmployee(id).Returns(appEmployee);

            var result = sut.GetEmployee(id) as OkNegotiatedContentResult<Employee>;

            Assert.IsNotNull(result);
            Assert.AreSame(apiEmployee, result.Content);
        }

        [TestMethod]
        public void GiveFeedback()
        {
            Guid employeeId = Guid.NewGuid();
            const string feedback = "Good job!";

            sut.GiveEmployeeTextFeedback(employeeId, feedback);

            repository.Received(1).AddTextFeedback(employeeId, feedback);
        }

        private IMapper<Application.Employee, Employee> CreateEmployeeMapperMock()
        {
            IMapper<Application.Employee, Employee> mapper = Substitute.For<IMapper<Application.Employee, Employee>>();
            mapperFactory.CreateEmployeeMapper().Returns(mapper);
            return mapper;
        }
    }
}
