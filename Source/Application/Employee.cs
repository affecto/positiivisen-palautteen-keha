using System;
using System.Collections.Generic;
using System.Linq;

namespace Affecto.PositiveFeedback.Application
{
    public class Employee
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Location { get; private set; }
        public bool HasPicture { get; private set; }
        public IReadOnlyCollection<string> TextFeedback { get; private set; }

        public Employee(Guid id, string name, string location, bool hasPicture, IEnumerable<string> textFeedback = null)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id must be given.", nameof(id));
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name must be given.", nameof(name));
            }

            Id = id;
            Name = name;
            Location = location;
            HasPicture = hasPicture;
            TextFeedback = textFeedback?.ToList() ?? new List<string>();
        }
    }
}