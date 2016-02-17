using System;
using System.Collections.Generic;
using System.Linq;

namespace Affecto.PositiveFeedback.Application
{
    public class Employee
    {
        public Guid Id { get; private set; }
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string Title { get; private set; }
        public string Location { get; private set; }
        public IReadOnlyCollection<string> TextFeedback { get; private set; }

        public Employee(Guid id, string lastName, string firstName, string title, string location, IEnumerable<string> textFeedback = null)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id must be given.", nameof(id));
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name must be given.", nameof(lastName));
            }
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name must be given.", nameof(firstName));
            }

            Id = id;
            LastName = lastName;
            FirstName = firstName;
            Title = title;
            Location = location;
            TextFeedback = textFeedback?.ToList() ?? new List<string>();
        }
    }
}