using System.Collections.Generic;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory
{
    internal interface IConfiguration
    {
        string DomainPath { get; }
        IEnumerable<string> Groups { get; }
        string PictureProperty { get; }
    }
}