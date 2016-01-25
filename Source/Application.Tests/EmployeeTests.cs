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
            sut = new Employee(Guid.Empty, "Teppo", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NameCannotBeEmpty()
        {
            sut = new Employee(Guid.NewGuid(), string.Empty, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NameCannotBeNull()
        {
            sut = new Employee(Guid.NewGuid(), null, null);
        }

        [TestMethod]
        public void TextFeedbackIsEmptyByDefault()
        {
            sut = new Employee(Guid.NewGuid(), "Teppo", null);

            Assert.IsNotNull(sut.TextFeedback);
            Assert.AreEqual(0, sut.TextFeedback.Count);
        }

        [TestMethod]
        public void TextFeedbackIsInitialized()
        {
            sut = new Employee(Guid.NewGuid(), "Teppo", null, new List<string> { "Nice", "Good job" });

            Assert.IsNotNull(sut.TextFeedback);
            Assert.AreEqual(2, sut.TextFeedback.Count);
        }
    }
}