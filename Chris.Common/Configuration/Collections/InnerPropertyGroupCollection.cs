
namespace Chris.Common.Configuration.Collections
{
    using System.Configuration;

    using Chris.Common.Configuration.Elements;

    /*
     * [ConfigurationCollection(typeof(PropertyGroupElement), AddItemName = "property")]
     * DOESN'T WORK WITH MULTIPLE LEVELS OF NESTED COLLECTIONS
     */
    public class InnerPropertyGroupCollection : ConfigurationElementCollection
    {
        public InnerPropertyGroupCollection()
        {
            AddElementName = "property";
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new PropertyGroupElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PropertyGroupElement) element).Name;
        }

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

        public PropertyGroupElement this[int idx]
        {
            get
            {
                return (PropertyGroupElement)BaseGet(idx);
            }
        }

        public new PropertyGroupElement this[string idx]
        {
            get
            {
                return (PropertyGroupElement)BaseGet(idx);
            }
        }
    }
}