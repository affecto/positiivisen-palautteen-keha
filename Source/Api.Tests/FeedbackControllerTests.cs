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
        public void GetEmployees()
        {
            IMapper<Application.Employee, Employee> employeeMapper = CreateEmployeeMapperMock();
            var appEmployee = new Application.Employee(Guid.NewGuid(), "name");
            var apiEmployee = new Employee();
            employeeMapper.Map(appEmployee).Returns(apiEmployee);
            repository.GetEmployees().Returns(new List<Application.Employee> { appEmployee });

            var result = sut.GetEmployees() as OkNegotiatedContentResult<IEnumerable<Employee>>;

            Assert.IsNotNull(result);
            Assert.AreSame(apiEmployee, result.Content.Single());
        }

        private IMapper<Application.Employee, Employee> CreateEmployeeMapperMock()
        {
            IMapper<Application.Employee, Employee> mapper = Substitute.For<IMapper<Application.Employee, Employee>>();
            mapperFactory.CreateEmployeeMapper().Returns(mapper);
            return mapper;
        }
    }
}
