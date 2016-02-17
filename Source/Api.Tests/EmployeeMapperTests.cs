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
            source = new Application.Employee(id, "name", null, false);

            destination = sut.Map(source);

            Assert.AreEqual(id, destination.Id);
        }

        [TestMethod]
        public void NameIsMapped()
        {
            const string name = "Teppo";
            source = new Application.Employee(Guid.NewGuid(), name, null, false);

            destination = sut.Map(source);

            Assert.AreEqual(name, destination.Name);
        }

        [TestMethod]
        public void HasPictureIsMapped()
        {
            const bool hasPicture = true;
            source = new Application.Employee(Guid.NewGuid(), "name", null, hasPicture);

            destination = sut.Map(source);

            Assert.AreEqual(hasPicture, destination.HasPicture);
        }
    }
}
