namespace Chris.Common.Configuration.Elements
{
    using System.Configuration;
    using System.Linq;
    using System.Reflection;

    public class RuleElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true, DefaultValue = "")]
        public string Name
        {
            get
            {
                return (string)base["name"];
            }
            set
            {
                base["name"] = value;
            }
        }

        [ConfigurationProperty("propertyGroup", IsRequired = true, DefaultValue = "")]
        public string PropertyGroupName
        {
            get
            {
                return (string)base["propertyGroup"];
            }
            set
            {
                base["propertyGroup"] = value;
            }
        }

        [ConfigurationProperty("action", IsRequired = true, DefaultValue = "")]
        public string ActionName
        {
            get
            {
                return (string)base["action"];
            }
            set
            {
                base["action"] = value;
            }
        }

        [ConfigurationProperty("objectId", DefaultValue = "")]
        public string ObjectId
        {
            get
            {
                return (string)base["objectId"];
            }
            set
            {
                base["objectId"] = value;
            }
        }

        public bool Validate(string objectId, FieldInfo field, object source, object compare, object arrayElement, bool missingInSource, string objectBranchName)
        {
            bool isValid = false;

            var properties = ConfigurationHelper.GetConfigurationProperties(PropertyGroupName);
            if (properties.Any())
            {
                if (properties.Any(property => property.Name == objectBranchName))
                {
                    isValid = true;

                    var actions = ConfigurationHelper.GetConfigurationActions();

                    var action = actions.FirstOrDefault(a => a.Name == ActionName);

                    if (action != null)
                        action.Execute(field, source, compare, arrayElement, missingInSource);
                    else
                        throw new ConfigurationErrorsException(string.Format("No action defined with name {0}", ActionName));
                }
            }
            else
                throw new ConfigurationErrorsException(string.Format("No propertyGroup defined with name {0}", PropertyGroupName));

            return isValid;
        }
    }
}