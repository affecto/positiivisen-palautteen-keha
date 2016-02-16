using System;
using System.Collections.Generic;
using Affecto.ActiveDirectoryService;
using Affecto.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory.Tests
{
    [TestClass]
    public class EmployeeRepositoryTests
    {
        private const string QueryFilter = "(abc=xyz)";

        private EmployeeRepository sut;
        private IConfiguration configuration;
        private IActiveDirectoryService activeDirectoryService;
        private IMapper<IPrincipal, Employee> principalMapper;

        [TestInitialize]
        public void Setup()
        {
            configuration = Substitute.For<IConfiguration>();
            activeDirectoryService = Substitute.For<IActiveDirectoryService>();
            principalMapper = Substitute.For<IMapper<IPrincipal, Employee>>();
            sut = new EmployeeRepository(activeDirectoryService, configuration, principalMapper);
        }

        [TestMethod]
        public void PrincipalsFromAllGroupsAreCollected()
        {
            IPrincipal member1 = Substitute.For<IPrincipal>();
            IPrincipal member2 = Substitute.For<IPrincipal>();
            IPrincipal member3 = Substitute.For<IPrincipal>();

            member1.NativeGuid.Returns(Guid.NewGuid());
            member2.NativeGuid.Returns(Guid.NewGuid());
            member3.NativeGuid.Returns(Guid.NewGuid());

            configuration.QueryFilter.Returns(QueryFilter);
            activeDirectoryService.SearchPrincipals(QueryFilter, Arg.Any<ICollection<string>>()).Returns(new List<IPrincipal> { member1, member2, member3 });

            IReadOnlyCollection<IEmployee> result = sut.GetEmployees();

            Assert.AreEqual(3, result.Count);
        }
    }
}