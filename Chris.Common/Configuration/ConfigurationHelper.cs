namespace Chris.Common.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Configuration;
    using System.Reflection;

    using Chris.Common.Configuration.Collections;
    using Chris.Common.Configuration.Elements;

    public static class ConfigurationHelper
    {
        #region Configuration Methods

        public static bool FindAndApplyRule(string objectId, string objectBranchName, object sourceObject, object compareObject, object arrayElement, bool missingInSource, FieldInfo field, Type deviationType)
        {
            bool isValid = false;
            var fullyQualifiedName = string.Format("{0}.{1}", objectBranchName, field.Name);

            var rules = GetConfigurationRules();
            foreach (var rule in rules)
            {
                var objectIds = rule.ObjectId.Split(';');

                if (!objectIds.Any() || objectIds.Any(o => o == objectId))
                {
                    isValid = rule.Validate(objectId, field, sourceObject, compareObject, arrayElement, missingInSource, fullyQualifiedName);

                    if (isValid)
                        break;
                }
            }

            return isValid;
        }

        private static string GetAppSetting(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }

        public static MigrationConfigurationSection GetConfiguration()
        {
            return (MigrationConfigurationSection)ConfigurationManager.GetSection(GetAppSetting(AppSettings.RootSectionName));
        }

        public static List<RuleElement> GetConfigurationRules()
        {
            var rules = new List<RuleElement>();
            var config = (MigrationConfigurationSection)ConfigurationManager.GetSection(GetAppSetting(AppSettings.RootSectionName));

            if (config != null)
                rules = config.Rules.Cast<RuleElement>().ToList();

            return rules;
        }

        public static List<ActionElement> GetConfigurationActions()
        {
            var actions = new List<ActionElement>();
            var config = (MigrationConfigurationSection)ConfigurationManager.GetSection(GetAppSetting(AppSettings.RootSectionName));

            if (config != null)
                actions = config.Actions.Cast<ActionElement>().ToList();

            return actions;
        }

        public static List<PropertyGroupElement> GetConfigurationProperties(string propertyGroupName)
        {
            var properties = new List<PropertyGroupElement>();
            var config = (MigrationConfigurationSection)ConfigurationManager.GetSection(GetAppSetting(AppSettings.RootSectionName));

            if (config != null)
            {
                var propertyGroups = config.PropertyGroups.Cast<InnerPropertyGroupCollection>().FirstOrDefault(p => p.Name == propertyGroupName);

                if (propertyGroups != null)
                    properties = propertyGroups.Cast<PropertyGroupElement>().ToList();
            }

            return properties;
        }

        public static MigrationConfigurationSection GetConfiguration(string sectionName)
        {
            return (MigrationConfigurationSection)ConfigurationManager.GetSection(sectionName);
        }

        #endregion
    }
}
