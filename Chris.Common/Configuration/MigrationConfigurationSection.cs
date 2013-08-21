namespace Chris.Common.Configuration
{
    using System.Configuration;

    using Chris.Common.Configuration.Collections;

    public class MigrationConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("rules")]
        public RulesCollection Rules
        {
            get
            {
                return (RulesCollection)base["rules"];
            }
        }

        [ConfigurationProperty("actions")]
        public ActionsCollection Actions
        {
            get
            {
                return (ActionsCollection)base["actions"];
            }
        }

        [ConfigurationProperty("propertyGroups", IsDefaultCollection = false)]
        public PropertyGroupCollection PropertyGroups
        {
            get
            {
                return (PropertyGroupCollection)base["propertyGroups"];
            }
        }
    }
}
