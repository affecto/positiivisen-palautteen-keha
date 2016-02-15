namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory
{
    internal interface IConfiguration
    {
        string DomainPath { get; }
        string QueryFilter { get; }
        string PictureUrlProperty { get; }
        string LocationProperty { get; }
        string OrganizationProperty { get; }
        string SubOrganizationProperty { get; }
    }
}