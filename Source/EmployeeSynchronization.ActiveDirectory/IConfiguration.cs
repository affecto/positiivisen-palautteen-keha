using System.Collections;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory
{
    internal interface IConfiguration
    {
        string DomainPath { get; }
        IEnumerable Groups { get; }
        IEnumerable AdditionalProperties { get; }
    }
}