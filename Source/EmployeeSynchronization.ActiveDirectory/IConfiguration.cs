using System.Collections.Generic;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory
{
    internal interface IConfiguration
    {
        string DomainPath { get; }
        IEnumerable<string> Groups { get; }
        string PictureUrlProperty { get; }
        string LocationProperty { get; }
        string OrganizationProperty { get; }
        string SubOrganizationProperty { get; }
    }
}