using System;
using System.Collections.Generic;
using Affecto.ActiveDirectoryService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory.Tests
{
    [TestClass]
    public class EmployeeRepositoryTests
    {
        private EmployeeRepository sut;
        private IConfiguration configuration;
        private IActiveDirectoryService activeDirectoryService;

        [TestInitialize]
        public void Setup()
        {
            configuration = Substitute.For<IConfiguration>();
            activeDirectoryService = Substitute.For<IActiveDirectoryService>();
            sut = new EmployeeRepository(activeDirectoryService, configuration);
        }

        [TestMethod]
        public void PrincipalsFromAllGroupsAreCollected()
        {
            const string group1 = "IT";
            const string group2 = "HR";

            IPrincipal group1Member1 = Substitute.For<IPrincipal>();
            IPrincipal group1Member2 = Substitute.For<IPrincipal>();
            IPrincipal group2Member1 = Substitute.For<IPrincipal>();

            group1Member1.NativeGuid.Returns(Guid.NewGuid());
            group1Member2.NativeGuid.Returns(Guid.NewGuid());
            group2Member1.NativeGuid.Returns(Guid.NewGuid());

            configuration.Groups.Returns(new List<string> { group1, group2 });

            activeDirectoryService.GetGroupMembers(group1, true, Arg.Any<ICollection<string>>()).Returns(new List<IPrincipal> { group1Member1, group1Member2 });
            activeDirectoryService.GetGroupMembers(group2, true, Arg.Any<ICollection<string>>()).Returns(new List<IPrincipal> { group2Member1 });

            IReadOnlyCollection<IEmployee> result = sut.GetEmployees();

            Assert.AreEqual(3, result.Count);
        }
    }
}