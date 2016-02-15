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
            sut = new Employee(Guid.Empty, "Teppo", "Turku");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NameCannotBeEmpty()
        {
            sut = new Employee(Guid.NewGuid(), string.Empty, "Turku");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NameCannotBeNull()
        {
            sut = new Employee(Guid.NewGuid(), null, "Turku");
        }

        [TestMethod]
        public void TextFeedbackIsEmptyByDefault()
        {
            sut = new Employee(Guid.NewGuid(), "Teppo", "Turku");

            Assert.IsNotNull(sut.TextFeedback);
            Assert.AreEqual(0, sut.TextFeedback.Count);
        }

        [TestMethod]
        public void TextFeedbackIsInitialized()
        {
            sut = new Employee(Guid.NewGuid(), "Teppo", "Turku", new List<string> { "Nice", "Good job" });

            Assert.IsNotNull(sut.TextFeedback);
            Assert.AreEqual(2, sut.TextFeedback.Count);
        }
    }
}