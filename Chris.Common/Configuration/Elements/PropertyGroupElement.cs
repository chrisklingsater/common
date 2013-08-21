namespace Chris.Common.Configuration.Elements
{
    using System.Configuration;

    public class PropertyGroupElement : ConfigurationElement
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
    }
}