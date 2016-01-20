using System;
using System.Collections.Generic;

namespace Affecto.PositiveFeedback.Application
{
    public class Employee
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyCollection<string> TextFeedback { get; private set; }

        public Employee(Guid id, string name)
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
        }
    }
}