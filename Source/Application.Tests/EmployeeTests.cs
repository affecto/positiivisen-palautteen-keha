using System;
using Affecto.PositiveFeedback.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.Tests
{
    [TestClass]
    public class EmployeeTests
    {
        private Employee sut;

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IdCannotBeEmpty()
        {
            sut = new Employee(Guid.Empty, "Teppo");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NameCannotBeEmpty()
        {
            sut = new Employee(Guid.NewGuid(), string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NameCannotBeNull()
        {
            sut = new Employee(Guid.NewGuid(), null);
        }
    }
}
