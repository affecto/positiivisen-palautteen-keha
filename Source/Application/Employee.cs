using System;

namespace Affecto.PositiveFeedback.Application
{
    public class Employee
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Employee(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
