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

        [TestMethod]
        public void PrincipalsInMultipleGroupsAreNotCollectedTwice()
        {
            const string group1 = "IT";
            const string group2 = "HR";

            IPrincipal group1Member = Substitute.For<IPrincipal>();
            IPrincipal group2Member = Substitute.For<IPrincipal>();
            IPrincipal memberInBothGroups = Substitute.For<IPrincipal>();

            group1Member.NativeGuid.Returns(Guid.NewGuid());
            group2Member.NativeGuid.Returns(Guid.NewGuid());
            memberInBothGroups.NativeGuid.Returns(Guid.NewGuid());

            configuration.Groups.Returns(new List<string> { group1, group2 });

            activeDirectoryService.GetGroupMembers(group1, true, Arg.Any<ICollection<string>>()).Returns(new List<IPrincipal> { group1Member, memberInBothGroups });
            activeDirectoryService.GetGroupMembers(group2, true, Arg.Any<ICollection<string>>()).Returns(new List<IPrincipal> { group2Member, memberInBothGroups });

            IReadOnlyCollection<IEmployee> result = sut.GetEmployees();

            Assert.AreEqual(3, result.Count);
        }
    }
}