using System.Configuration;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory
{
    internal class Configuration : ConfigurationSection, IConfiguration
    {
        private static readonly Configuration SettingsInstance = ConfigurationManager.GetSection("activeDirectory") as Configuration;
        public static IConfiguration Settings => SettingsInstance;

        [ConfigurationProperty("domainPath", IsRequired = true)]
        public string DomainPath
        {
            get { return (string) this["domainPath"]; }
            set { this["domainPath"] = value; }
        }

        [ConfigurationProperty("queryFilter", IsRequired = true)]
        public string QueryFilter
        {
            get { return (string) this["queryFilter"]; }
            set { this["queryFilter"] = value; }
        }

        [ConfigurationProperty("pictureUrlProperty", IsRequired = true)]
        public string PictureUrlProperty
        {
            get { return (string)this["pictureUrlProperty"]; }
            set { this["pictureUrlProperty"] = value; }
        }

        [ConfigurationProperty("locationProperty", IsRequired = true)]
        public string LocationProperty
        {
            get { return (string)this["locationProperty"]; }
            set { this["locationProperty"] = value; }
        }

        [ConfigurationProperty("organizationProperty", IsRequired = true)]
        public string OrganizationProperty
        {
            get { return (string)this["organizationProperty"]; }
            set { this["organizationProperty"] = value; }
        }

        [ConfigurationProperty("subOrganizationProperty", IsRequired = true)]
        public string SubOrganizationProperty
        {
            get { return (string)this["subOrganizationProperty"]; }
            set { this["subOrganizationProperty"] = value; }
        }
    }
}