using System;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory
{
    internal class Employee : IEmployee
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Organization { get; set; }
        public string SubOrganization { get; set; }
        public byte[] Picture { get; set; }
    }
}