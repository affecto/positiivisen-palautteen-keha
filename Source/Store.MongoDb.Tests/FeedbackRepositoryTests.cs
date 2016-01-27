using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using NSubstitute;

namespace Affecto.PositiveFeedback.Store.MongoDb.Tests
{
    [TestClass]
    public class FeedbackRepositoryTests
    {
        private FeedbackRepository sut;
        private IMongoCollection<Employee> employees;
        private IGridFSBucket binaryFiles;

        [TestInitialize]
        public void Setup()
        {
            ICollection<Employee> dbEmployees = Substitute.For<ICollection<Employee>>();
            employees = Substitute.For<IMongoCollection<Employee>>();
            binaryFiles = Substitute.For<IGridFSBucket>();
            dbEmployees.Load().Returns(employees);
            dbEmployees.CreateGridFSBucket().Returns(binaryFiles);
            sut = new FeedbackRepository(dbEmployees);
        }

        [TestMethod]
        public void EmptyTextFeedbackIsNotAdded()
        {
            sut.AddTextFeedback(Guid.NewGuid(), string.Empty);

            employees.DidNotReceive().UpdateOne(Arg.Any<FilterDefinition<Employee>>(), Arg.Any<UpdateDefinition<Employee>>());
        }

        [TestMethod]
        public void WhiteSpaceTextFeedbackIsNotAdded()
        {
            sut.AddTextFeedback(Guid.NewGuid(), " ");

            employees.DidNotReceive().UpdateOne(Arg.Any<FilterDefinition<Employee>>(), Arg.Any<UpdateDefinition<Employee>>());
        }

        [TestMethod]
        public void NullTextFeedbackIsNotAdded()
        {
            sut.AddTextFeedback(Guid.NewGuid(), null);

            employees.DidNotReceive().UpdateOne(Arg.Any<FilterDefinition<Employee>>(), Arg.Any<UpdateDefinition<Employee>>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeWithAnEmptyIdCannotBeAdded()
        {
            sut.AddEmployee(Guid.Empty, "Jeff", "LA", "Management", new MemoryStream());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeWithAnEmptyNameCannotBeAdded()
        {
            sut.AddEmployee(Guid.NewGuid(), string.Empty, "LA", "Management", new MemoryStream());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeWithNullNameCannotBeAdded()
        {
            sut.AddEmployee(Guid.NewGuid(), null, "LA", "Management", new MemoryStream());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeWithAnEmptyIdCannotBeUpdated()
        {
            sut.UpdateEmployee(Guid.Empty, "Jeff", "LA", "Management", new MemoryStream());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeWithAnEmptyNameCannotBeUpdated()
        {
            sut.UpdateEmployee(Guid.NewGuid(), string.Empty, "LA", "Management", new MemoryStream());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeWithNullNameCannotBeUpdated()
        {
            sut.UpdateEmployee(Guid.NewGuid(), null, "LA", "Management", new MemoryStream());
        }

        [TestMethod]
        public void AddEmployee()
        {
            Guid id = Guid.NewGuid();
            const string name = "Matt";
            const string organization = "cleaning";
            const string location = "London";

            sut.AddEmployee(id, name, location, organization, new MemoryStream());

            employees.Received(1).InsertOne(Arg.Is<Employee>(e => e.Id.Equals(id) && e.Name.Equals(name) && e.Location.Equals(location) && e.Organization.Equals(organization)));
        }

        [TestMethod]
        public void AddedEmployeesAreActive()
        {
            sut.AddEmployee(Guid.NewGuid(), "Matt", "bosses", "Madrid", new MemoryStream());

            employees.Received(1).InsertOne(Arg.Is<Employee>(e => e.Active));
        }

        [TestMethod]
        public void EmployeeWithNoPictureIsAdded()
        {
            sut.AddEmployee(Guid.NewGuid(), "Matt", "bosses", "Madrid", null);

            employees.Received(1).InsertOne(Arg.Is<Employee>(e => e.PictureFileId.Equals(ObjectId.Empty)));
        }

        [TestMethod]
        public void EmployeeWithPictureIsAdded()
        {
            Stream picture = new MemoryStream();
            Guid id = Guid.NewGuid();
            ObjectId pictureId = new ObjectId();

            binaryFiles.UploadFromStream(id.ToString(), picture).Returns(pictureId);

            sut.AddEmployee(id, "Matt", "bosses", "Madrid", picture);
            
            employees.Received(1).InsertOne(Arg.Is<Employee>(e => e.PictureFileId.Equals(pictureId)));
        }

        [TestMethod]
        public void GetEmployeeWhenEmployeeIsNotFound()
        {
            Application.Employee result = sut.GetEmployee(Guid.NewGuid());

            Assert.IsNull(result);
        }

        [TestMethod]
        public void OldPictureIsNotDeletedWhenUpdatingEmployeeWithNoPicture()
        {
            sut.UpdateEmployee(Guid.NewGuid(), "Mark", "Köln", "Management", new MemoryStream());

            binaryFiles.DidNotReceive().Delete(Arg.Any<ObjectId>());
        }

        [TestMethod]
        public void UpdatingEmployeeWithNoNewPictureUploadsNoFile()
        {
            sut.UpdateEmployee(Guid.NewGuid(), "Mark", "Köln", "Management", null);

            binaryFiles.DidNotReceive().UploadFromStream(Arg.Any<string>(), Arg.Any<Stream>());
        }

        [TestMethod]
        public void GetEmployeePicture()
        {
            Guid id = Guid.NewGuid();

            sut.GetEmployeePicture(id);

            binaryFiles.Received(1).DownloadToStreamByName(id.ToString(), Arg.Any<Stream>());
        }
    }
}
