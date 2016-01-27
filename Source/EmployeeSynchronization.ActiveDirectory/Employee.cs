using System;
using System.IO;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory
{
    internal class Employee : IEmployee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Organization { get; set; }
        public byte[] Picture { get; set; }
    }
}