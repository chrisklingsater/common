namespace Chris.Common.Configuration.Collections
{
    using System.Configuration;

    using Chris.Common.Configuration.Elements;

    [ConfigurationCollection(typeof(ActionElement), AddItemName = "action")]
    public class ActionsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ActionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ActionElement)element).Name;
        }

        public ActionElement this[int idx]
        {
            get
            {
                return (ActionElement)BaseGet(idx);
            }
        }

        public new ActionElement this[string idx]
        {
            get
            {
                return (ActionElement)BaseGet(idx);
            }
        }
    }
}