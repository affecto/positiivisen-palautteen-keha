using System;
using System.Collections.Generic;
using Affecto.ActiveDirectoryService;
using Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory.EmployeePicture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory.Tests
{
    [TestClass]
    public class PrincipalMapperTests
    {
        private const string PictureProperty = "pic";
        private const string LocationProperty = "location";
        private const string OrganizationProperty = "org";
        private const string SubOrganizationProperty = "suborg";

        private PrincipalMapper sut;
        private IConfiguration configuration;
        private PictureHandler pictureHandler;
        private IPrincipal source;
        private Employee destination;

        [TestInitialize]
        public void Setup()
        {
            source = Substitute.For<IPrincipal>();
            pictureHandler = Substitute.For<PictureHandler>();
            SetupConfiguration();
            sut = new PrincipalMapper(configuration, pictureHandler);
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
            const string pictureUrl = @"\\server.fi\folder\pic.jpg";
            byte[] picture = new byte[0];

            source.AdditionalProperties.ContainsKey(PictureProperty).Returns(true);
            source.AdditionalProperties.Returns(new Dictionary<string, object> { { PictureProperty, pictureUrl } });
            pictureHandler.DownloadAndResizePicture(pictureUrl).Returns(picture);

            destination = sut.Map(source);

            Assert.AreSame(picture, destination.Picture);
        }

        [TestMethod]
        public void LocationIsMapped()
        {
            const string location = "Turku";
            source.AdditionalProperties.ContainsKey(LocationProperty).Returns(true);
            source.AdditionalProperties.Returns(new Dictionary<string, object> { { LocationProperty, location } });

            destination = sut.Map(source);

            Assert.AreEqual(location, destination.Location);
        }

        [TestMethod]
        public void OrganizationIsMapped()
        {
            const string organization = "IT";
            source.AdditionalProperties.ContainsKey(OrganizationProperty).Returns(true);
            source.AdditionalProperties.Returns(new Dictionary<string, object> { { OrganizationProperty, organization } });

            destination = sut.Map(source);

            Assert.AreEqual(organization, destination.Organization);
        }

        [TestMethod]
        public void SubOrganizationIsMapped()
        {
            const string subOrganization = "Testing";
            source.AdditionalProperties.ContainsKey(SubOrganizationProperty).Returns(true);
            source.AdditionalProperties.Returns(new Dictionary<string, object> { { SubOrganizationProperty, subOrganization } });

            destination = sut.Map(source);

            Assert.AreEqual(subOrganization, destination.SubOrganization);
        }

        [TestMethod]
        public void AdditionalPropertiesAreNotFound()
        {
            byte[] picture = null;
            pictureHandler.DownloadAndResizePicture(null).Returns(picture);

            destination = sut.Map(source);

            Assert.IsNull(destination.Location);
            Assert.IsNull(destination.Organization);
            Assert.IsNull(destination.SubOrganization);
            Assert.IsNull(destination.Picture);
        }

        private void SetupConfiguration()
        {
            configuration = Substitute.For<IConfiguration>();
            configuration.PictureUrlProperty.Returns(PictureProperty);
            configuration.LocationProperty.Returns(LocationProperty);
            configuration.OrganizationProperty.Returns(OrganizationProperty);
            configuration.SubOrganizationProperty.Returns(SubOrganizationProperty);
        }
    }
}
