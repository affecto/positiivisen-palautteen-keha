using System;
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
            sut.AddEmployee(Guid.Empty, "Bridges", "Jeff", "Actor", "LA", "Management", "Directors", new byte[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeWithAnEmptyLastNameCannotBeAdded()
        {
            sut.AddEmployee(Guid.NewGuid(), string.Empty, "Jeff", "Actor", "LA", "Management", "Directors", new byte[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeWithNullLastNameCannotBeAdded()
        {
            sut.AddEmployee(Guid.NewGuid(), null, "Jeff", "Actor", "LA", "Management", "Directors", new byte[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeWithAnEmptyFirstNameCannotBeAdded()
        {
            sut.AddEmployee(Guid.NewGuid(), "Bridges", string.Empty, "Actor", "LA", "Management", "Directors", new byte[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeWithNullFirstNameCannotBeAdded()
        {
            sut.AddEmployee(Guid.NewGuid(), "Bridges", null, "Actor", "LA", "Management", "Directors", new byte[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeWithAnEmptyIdCannotBeUpdated()
        {
            sut.UpdateEmployee(Guid.Empty, "Bridges", "Jeff", "Actor", "LA", "Management", "Directors", new byte[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeWithAnEmptyNameCannotBeUpdated()
        {
            sut.UpdateEmployee(Guid.NewGuid(), string.Empty, "Jeff", "Actor", "LA", "Management", "Directors", new byte[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmployeeWithNullNameCannotBeUpdated()
        {
            sut.UpdateEmployee(Guid.NewGuid(), null, "Jeff", "Actor", "LA", "Management", "Directors", new byte[0]);
        }

        [TestMethod]
        public void AddEmployee()
        {
            Guid id = Guid.NewGuid();
            const string lastName = "LeBlanc";
            const string firstName = "Matt";
            const string title = "comedian";
            const string organization = "cleaning";
            const string subOrganization = "floor";
            const string location = "London";

            sut.AddEmployee(id, lastName, firstName, title, location, organization, subOrganization, new byte[0]);

            employees
                .Received(1)
                .InsertOne(
                    Arg.Is<Employee>(
                        e => e.Id.Equals(id) && e.LastName.Equals(lastName) && e.FirstName.Equals(firstName) && e.Title.Equals(title)
                            && e.Location.Equals(location) && e.Organization.Equals(organization) && e.SubOrganization.Equals(subOrganization)));
        }

        [TestMethod]
        public void AddedEmployeesAreActive()
        {
            sut.AddEmployee(Guid.NewGuid(), "LeBlanc", "Matt", "comedian", "Madrid", "bosses", string.Empty, new byte[0]);

            employees.Received(1).InsertOne(Arg.Is<Employee>(e => e.Active));
        }

        [TestMethod]
        public void EmployeeWithNoPictureIsAdded()
        {
            sut.AddEmployee(Guid.NewGuid(), "LeBlanc", "Matt", "comedian", "Madrid", "bosses", string.Empty, null);

            employees.Received(1).InsertOne(Arg.Is<Employee>(e => e.PictureFileId.Equals(ObjectId.Empty)));
        }

        [TestMethod]
        public void EmployeeWithPictureIsAdded()
        {
            byte[] picture = new byte[0];
            Guid id = Guid.NewGuid();
            ObjectId pictureId = new ObjectId();

            binaryFiles.UploadFromBytes(id.ToString(), picture).Returns(pictureId);

            sut.AddEmployee(id, "LeBlanc", "Matt", "comedian", "Madrid", "bosses", string.Empty, picture);
            
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
            sut.UpdateEmployee(Guid.NewGuid(), "Hamill", "Mark", "skywalker", "Köln", "Management", string.Empty, new byte[0]);

            binaryFiles.DidNotReceive().Delete(Arg.Any<ObjectId>());
        }

        [TestMethod]
        public void UpdatingEmployeeWithNoNewPictureUploadsNoFile()
        {
            sut.UpdateEmployee(Guid.NewGuid(), "Hamill", "Mark", "skywalker", "Köln", "Management", string.Empty, null);

            binaryFiles.DidNotReceive().UploadFromBytes(Arg.Any<string>(), Arg.Any<byte[]>());
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
