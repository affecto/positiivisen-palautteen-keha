using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;

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

        [ConfigurationProperty("groups", IsRequired = true)]
        [TypeConverter(typeof(CommaDelimitedStringCollectionConverter))]
        public IEnumerable<string> Groups
        {
            get
            {
                var groups = (CommaDelimitedStringCollection)this["groups"];
                return groups.Cast<string>();
            }
            set { this["groups"] = value; }
        }
    }
}
