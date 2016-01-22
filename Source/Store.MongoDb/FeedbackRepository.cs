using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.PositiveFeedback.Application;
using MongoDB.Driver;

namespace Affecto.PositiveFeedback.Store.MongoDb
{
    internal class FeedbackRepository : IFeedbackRepository
    {
        private readonly IMongoCollection<Employee> employees;
        
        public FeedbackRepository(ICollection<Employee> databaseCollection)
        {
            if (databaseCollection == null)
            {
                throw new ArgumentNullException(nameof(databaseCollection));
            }
            employees = databaseCollection.Load();
        }
        
        public bool HasEmployee(Guid id)
        {
            return employees.Find(e => e.Id.Equals(id)).Any();
        }

        public void AddEmployee(Guid id, string name, string location, string organization)
        {
            ValidateIdAndName(id, name);
            var document = new Employee
            {
                Id = id,
                Name = name,
                Location = location,
                Organization = organization
            };
            employees.InsertOne(document);
        }

        public void UpdateEmployee(Guid id, string name, string location, string organization)
        {
            ValidateIdAndName(id, name);
            UpdateDefinition<Employee> update = Builders<Employee>.Update.Set(e => e.Name, name).Set(e => e.Location, location).Set(e => e.Organization, organization);
            employees.UpdateOne(e => e.Id.Equals(id), update);
        }

        public IEnumerable<Application.Employee> GetEmployees()
        {
            return employees.Find(FilterDefinition<Employee>.Empty).ToEnumerable().Select(e => new Application.Employee(e.Id, e.Name, e.TextFeedback));
        }

        public Application.Employee GetEmployee(Guid id)
        {
            Employee employee = employees.Find(e => e.Id.Equals(id)).SingleOrDefault();
            return employee != null ? new Application.Employee(employee.Id, employee.Name) : null;
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