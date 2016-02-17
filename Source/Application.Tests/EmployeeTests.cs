using System;
using System.Collections.Generic;
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
            sut = new Employee(Guid.Empty, "Testaaja", "Teppo", "Devaaja", "Turku");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LastNameCannotBeEmpty()
        {
            sut = new Employee(Guid.NewGuid(), string.Empty, "Teppo", "Devaaja", "Turku");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LastNameCannotBeNull()
        {
            sut = new Employee(Guid.NewGuid(), null, "Teppo", "Devaaja", "Turku");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FirstNameCannotBeEmpty()
        {
            sut = new Employee(Guid.NewGuid(), "Testaaja", string.Empty, "Devaaja", "Turku");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FirstNameCannotBeNull()
        {
            sut = new Employee(Guid.NewGuid(), "Testaaja", null, "Devaaja", "Turku");
        }

        [TestMethod]
        public void TextFeedbackIsEmptyByDefault()
        {
            sut = new Employee(Guid.NewGuid(), "Testaaja", "Teppo", "Devaaja", "Turku");

            Assert.IsNotNull(sut.TextFeedback);
            Assert.AreEqual(0, sut.TextFeedback.Count);
        }

        [TestMethod]
        public void TextFeedbackIsInitialized()
        {
            sut = new Employee(Guid.NewGuid(), "Testaaja", "Teppo", "Devaaja", "Turku", new List<string> { "Nice", "Good job" });

            Assert.IsNotNull(sut.TextFeedback);
            Assert.AreEqual(2, sut.TextFeedback.Count);
        }
    }
}