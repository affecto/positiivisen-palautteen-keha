using System.Collections;
using System.ComponentModel;
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

        [ConfigurationProperty("groups", IsRequired = true)]
        [TypeConverter(typeof(CommaDelimitedStringCollectionConverter))]
        public IEnumerable Groups
        {
            get { return (CommaDelimitedStringCollection)this["groups"]; }
            set { this["groups"] = value; }
        }

        [ConfigurationProperty("additionalProperties", IsRequired = false)]
        [TypeConverter(typeof(CommaDelimitedStringCollectionConverter))]
        public IEnumerable AdditionalProperties
        {
            get { return (CommaDelimitedStringCollection)this["additionalProperties"]; }
            set { this["additionalProperties"] = value; }
        }
    }
}
