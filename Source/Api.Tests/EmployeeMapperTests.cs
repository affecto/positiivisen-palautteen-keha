using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.PositiveFeedback.Api.Tests
{
    [TestClass]
    public class EmployeeMapperTests
    {
        private EmployeeMapper sut;
        private Application.Employee source;
        private Employee destination;

        [TestInitialize]
        public void Setup()
        {
            sut = new EmployeeMapper();
        }

        [TestMethod]
        public void IdIsMapped()
        {
            Guid id = Guid.NewGuid();
            source = new Application.Employee(id, "lastName", "firstName", "title", null);

            destination = sut.Map(source);

            Assert.AreEqual(id, destination.Id);
        }

        [TestMethod]
        public void LastNameIsMapped()
        {
            const string name = "Testaaja";
            source = new Application.Employee(Guid.NewGuid(), name, "firstName", "title", null);

            destination = sut.Map(source);

            Assert.AreEqual(name, destination.LastName);
        }

        [TestMethod]
        public void FirstNameIsMapped()
        {
            const string name = "Teppo";
            source = new Application.Employee(Guid.NewGuid(), "lastName", name, "title", null, false);

            destination = sut.Map(source);

            Assert.AreEqual(name, destination.FirstName);
        }

        [TestMethod]
        public void TitleIsMapped()
        {
            const string title = "Devaaja";
            source = new Application.Employee(Guid.NewGuid(), "lastName", "firstName", title, null, false);

            destination = sut.Map(source);

            Assert.AreEqual(title, destination.Title);
        }

        [TestMethod]
        public void LocationIsMapped()
        {
            const string location = "Turku";
            source = new Application.Employee(Guid.NewGuid(), "lastName", "firstName", "title", location, false);

            destination = sut.Map(source);

            Assert.AreEqual(location, destination.Location);
        }

        [TestMethod]
        public void HasPictureIsMapped()
        {
            const bool hasPicture = true;
            source = new Application.Employee(Guid.NewGuid(), "lastName", "firstName", "title", null, hasPicture);

            destination = sut.Map(source);

            Assert.AreEqual(hasPicture, destination.HasPicture);
        }
    }
}
