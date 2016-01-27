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

        [ConfigurationProperty("pictureProperty", IsRequired = true)]
        public string PictureProperty
        {
            get { return (string)this["pictureProperty"]; }
            set { this["pictureProperty"] = value; }
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
