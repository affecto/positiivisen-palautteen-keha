using System;
using System.Collections.Generic;

namespace Affecto.PositiveFeedback.Store.MongoDb
{
    internal class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> TextFeedback { get; set; }
    }
}