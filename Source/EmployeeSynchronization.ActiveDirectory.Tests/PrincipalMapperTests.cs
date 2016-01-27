using System;
using System.Collections.Generic;
using Affecto.ActiveDirectoryService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory.Tests
{
    [TestClass]
    public class PrincipalMapperTests
    {
        private PrincipalMapper sut;
        private IConfiguration configuration;
        private IPrincipal source;
        private Employee destination;

        [TestInitialize]
        public void Setup()
        {
            source = Substitute.For<IPrincipal>();
            configuration = Substitute.For<IConfiguration>();
            sut = new PrincipalMapper(configuration);
        }

        [TestMethod]
        public void IdIsMapped()
        {
            Guid id = Guid.NewGuid();
            source.NativeGuid.Returns(id);

            destination = sut.Map(source);

            Assert.AreEqual(id, destination.Id);
        }

        [TestMethod]
        public void NameIsMapped()
        {
            const string name = "Karl Jenkins";
            source.DisplayName.Returns(name);

            destination = sut.Map(source);

            Assert.AreEqual(name, destination.Name);
        }

        [TestMethod]
        public void PictureIsMapped()
        {
            const string pictureProperty = "pic";
            byte[] picture = new byte[0];
            configuration.PictureProperty.Returns(pictureProperty);
            source.AdditionalProperties.Returns(new Dictionary<string, object> { { pictureProperty, picture } });

            destination = sut.Map(source);

            Assert.AreSame(picture, destination.Picture);
        }

        //todo: location and organization tests
    }
}
