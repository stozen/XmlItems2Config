using System;

namespace XmlItems2Config.Sample
{
    public class NameInfo : IItem
    {
        [XmlNodeProperty("id", true)]
        public string ID { get; set; }

        [XmlNodeProperty("firstName", true)]
        public string FirstName { get; set; }

        [XmlNodeProperty("lastName", true)]
        public string LastName { get; set; }
    }
}
