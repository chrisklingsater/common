namespace Chris.Common.Configuration.Collections
{
    using System.Configuration;

    /*
     * [ConfigurationCollection(typeof(InnerPropertyGroupCollection), AddItemName = "propertyGroup")]
     * DOESN'T WORK WITH MULTIPLE LEVELS OF NESTED COLLECTIONS
     */
    public class PropertyGroupCollection : ConfigurationElementCollection
    {
        public PropertyGroupCollection()
        {
            AddElementName = "propertyGroup";
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new InnerPropertyGroupCollection();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((InnerPropertyGroupCollection)element).Name;
        }

        public InnerPropertyGroupCollection this[int idx]
        {
            get
            {
                return (InnerPropertyGroupCollection)BaseGet(idx);
            }
        }

        public new InnerPropertyGroupCollection this[string idx]
        {
            get
            {
                return (InnerPropertyGroupCollection)BaseGet(idx);
            }
        }
    }
}