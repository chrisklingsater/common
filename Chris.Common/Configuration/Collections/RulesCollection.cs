namespace Chris.Common.Configuration.Collections
{
    using System.Configuration;

    using Chris.Common.Configuration.Elements;

    [ConfigurationCollection(typeof(RuleElement), AddItemName = "rule")]
    public class RulesCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new RuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RuleElement)element).Name;
        }

        public RuleElement this [int idx]
        {
            get
            {
                return (RuleElement)BaseGet(idx);
            }
        }

        public new RuleElement this[string idx]
        {
            get
            {
                return (RuleElement)BaseGet(idx);
            }
        }
    }
}
