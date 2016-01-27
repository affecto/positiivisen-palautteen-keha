using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Affecto.PositiveFeedback.Application;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace Affecto.PositiveFeedback.Store.MongoDb
{
    internal class FeedbackRepository : IFeedbackRepository
    {
        private readonly IMongoCollection<Employee> employees;
        private readonly IGridFSBucket binaryFiles;
        
        public FeedbackRepository(ICollection<Employee> databaseCollection)
        {
            if (databaseCollection == null)
            {
                throw new ArgumentNullException(nameof(databaseCollection));
            }
            employees = databaseCollection.Load();
            binaryFiles = databaseCollection.CreateGridFSBucket();
        }
        
        public bool HasEmployee(Guid id)
        {
            return employees.Find(e => e.Id.Equals(id)).Any();
        }

        public void AddEmployee(Guid id, string name, string location, string organization, Stream picture)
        {
            ValidateIdAndName(id, name);
            ObjectId pictureId = AddEmployeePicture(id, picture);
            var document = new Employee
            {
                Id = id,
                Name = name,
                Location = location,
                Organization = organization,
                Active = true,
                PictureFileId = pictureId
            };
            employees.InsertOne(document);
        }

        public void UpdateEmployee(Guid id, string name, string location, string organization, Stream picture)
        {
            ValidateIdAndName(id, name);
            ObjectId pictureId = UpdateEmployeePicture(id, picture);
            UpdateDefinition<Employee> update = Builders<Employee>.Update.Set(e => e.Name, name).Set(e => e.Location, location)
                .Set(e => e.Organization, organization).Set(e => e.Active, true).Set(e => e.PictureFileId, pictureId);
            employees.UpdateOne(e => e.Id.Equals(id), update);
        }

        public IReadOnlyCollection<Application.Employee> GetActiveEmployees()
        {
            return FindActiveEmployees().Select(e => CreateEmployee(e, false)).ToList();
        }

        public IReadOnlyCollection<Application.Employee> GetActiveEmployeesWithFeedback()
        {
            return FindActiveEmployees().Select(e => CreateEmployee(e, true)).ToList();
        }

        public Application.Employee GetEmployee(Guid id)
        {
            Employee employee = employees.Find(e => e.Id.Equals(id)).SingleOrDefault();
            return employee != null ? CreateEmployee(employee, false) : null;
        }

        public Stream GetEmployeePicture(Guid employeeId)
        {
            Stream pictureStream = new MemoryStream();
            binaryFiles.DownloadToStreamByName(employeeId.ToString(), pictureStream);
            return pictureStream;
        }

        public void AddTextFeedback(Guid employeeId, string feedback)
        {
            if (!string.IsNullOrWhiteSpace(feedback))
            {
                string trimmedFeedback = feedback.Trim();
                if (!IsTextFeedbackAdded(employeeId, trimmedFeedback))
                {
                    UpdateDefinition<Employee> update = Builders<Employee>.Update.AddToSet(e => e.TextFeedback, trimmedFeedback);
                    employees.UpdateOne(e => e.Id.Equals(employeeId), update);
                }
            }
        }

        public void DeactivateEmployee(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                throw new ArgumentException("Id must be defined.");
            }
            UpdateDefinition<Employee> update = Builders<Employee>.Update.Set(e => e.Active, false);
            employees.UpdateOne(e => e.Id.Equals(id), update);
        }

        private IEnumerable<Employee> FindActiveEmployees()
        {
            return employees.Find(e => e.Active).ToEnumerable();
        }

        private Application.Employee CreateEmployee(Employee employee, bool includeFeedback)
        {
            return includeFeedback ? new Application.Employee(employee.Id, employee.Name, employee.TextFeedback) : 
                new Application.Employee(employee.Id, employee.Name);
        }

        private ObjectId UpdateEmployeePicture(Guid employeeId, Stream picture)
        {
            FilterDefinition<GridFSFileInfo> filter = new FilterDefinitionBuilder<GridFSFileInfo>().Where(info => info.Filename.Equals(employeeId.ToString()));
            GridFSFileInfo oldPictureInfo = binaryFiles.Find(filter).SingleOrDefault();

            if (picture == null)
            {
                return oldPictureInfo?.Id ?? ObjectId.Empty;
            }
            if (oldPictureInfo != null)
            {
                binaryFiles.Delete(oldPictureInfo.Id);
            }

            return binaryFiles.UploadFromStream(employeeId.ToString(), picture);
        }

        private ObjectId AddEmployeePicture(Guid employeeId, Stream picture)
        {
            ObjectId pictureReference = ObjectId.Empty;
            if (picture != null)
            {
                pictureReference = binaryFiles.UploadFromStream(employeeId.ToString(), picture);
            }
            return pictureReference;
        }

        private bool IsTextFeedbackAdded(Guid employeeId, string feedback)
        {
            return employees.Find(e => e.Id.Equals(employeeId) && e.TextFeedback.Contains(feedback)).Any();
        }

        private static void ValidateIdAndName(Guid id, string name)
        {
            if (id.Equals(Guid.Empty))
            {
                throw new ArgumentException("Id must be defined.");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name must be defined.");
            }
        }
    }
}