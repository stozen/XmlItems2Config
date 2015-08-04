using System;

namespace XmlItems2Config
{
    /// <summary>
    /// XML文件里node的属性名特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class XmlNodePropertyAttribute : Attribute
    {
        public XmlNodePropertyAttribute(string propertyName, bool required = false)
        {
            PropertyName = propertyName;
            Required = required;
        }
        public string PropertyName { get; set; }
        public bool Required { get; set; }
    }
}
