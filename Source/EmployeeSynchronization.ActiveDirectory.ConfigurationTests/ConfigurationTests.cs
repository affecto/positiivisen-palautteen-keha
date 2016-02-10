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
        public void QueryFilter()
        {
            Assert.AreEqual("(abc=xyz)", sut.QueryFilter);
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