using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory.ConfigurationTests
{
    [TestClass]
    public class ConfigurationTests
    {
        private readonly IConfiguration sut = Configuration.Settings;

        [TestMethod]
        public void DomainPath()
        {
            Assert.AreEqual("google.fi", sut.DomainPath);
        }

        [TestMethod]
        public void Groups()
        {
            List<string> groups = sut.Groups.ToList();

            Assert.AreEqual(4, groups.Count);
            Assert.IsTrue(groups.Contains("main-list"));
            Assert.IsTrue(groups.Contains("board-of-directors"));
            Assert.IsTrue(groups.Contains("sanitation"));
            Assert.IsTrue(groups.Contains("testers"));
        }

        [TestMethod]
        public void PictureUrlProperty()
        {
            Assert.AreEqual("picture", sut.PictureUrlProperty);
        }

        [TestMethod]
        public void LocationProperty()
        {
            Assert.AreEqual("Turku", sut.LocationProperty);
        }

        [TestMethod]
        public void OrganizationProperty()
        {
            Assert.AreEqual("IT", sut.OrganizationProperty);
        }

        [TestMethod]
        public void SubOrganizationProperty()
        {
            Assert.AreEqual("Coding", sut.SubOrganizationProperty);
        }
    }
}
