using System;
using System.Collections.Generic;
using System.Linq;

namespace Affecto.PositiveFeedback.Application
{
    public class Employee
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public byte[] Picture { get; private set; }
        public IReadOnlyCollection<string> TextFeedback { get; private set; }

        public Employee(Guid id, string name, byte[] picture, IEnumerable<string> textFeedback = null)
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
            Picture = picture;
            TextFeedback = textFeedback?.ToList() ?? new List<string>();
        }
    }
}