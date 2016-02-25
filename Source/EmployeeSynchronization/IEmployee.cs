using System;
using System.IO;

namespace Affecto.PositiveFeedback.EmployeeSynchronization
{
    public interface IEmployee
    {
        Guid Id { get; }
        string LastName { get; }
        string FirstName { get; }
        string Title { get; set; }
        string Location { get; }
        string Organization { get; }
        string SubOrganization { get; }
        byte[] Picture { get; }
    }
}