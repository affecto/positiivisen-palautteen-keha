using System;

namespace Affecto.PositiveFeedback.EmployeeSynchronization
{
    public interface IEmployee
    {
        Guid Id { get; }
        string Name { get; }
        string Location { get; }
        string Organization { get; }
    }
}