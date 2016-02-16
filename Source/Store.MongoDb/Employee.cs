using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace Affecto.PositiveFeedback.Store.MongoDb
{
    internal class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> TextFeedback { get; set; }
        public string Location { get; set; }
        public string Organization { get; set; }
        public string SubOrganization { get; set; }
        public bool Active { get; set; }
        public ObjectId PictureFileId { get; set; }

        public Employee()
        {
            TextFeedback = new List<string>();
        }
    }
}