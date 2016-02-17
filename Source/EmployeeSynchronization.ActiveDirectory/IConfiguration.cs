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
        string LastNameProperty { get; set; }
        string FirstNameProperty { get; set; }
        string TitleProperty { get; set; }
    }
}