using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace XmlItems2Config
{
    public class XmlCollectionConfigHelper<T>
        : ICollectionConfigHelper<T> where T : IItem
    {
        private IList<T> list;

        public void Load(string fileName, string nodeName)
        {
            try
            {
                IDictionary<string, XmlPropertyMapping> dicts = getXmlNodeProperties(typeof(T));
                if (dicts != null && dicts.Count > 0)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(fileName);
                    XmlNodeList nodes = doc.GetElementsByTagName(nodeName);
                    if (nodes != null && nodes.Count > 0)
                    {
                        list = new List<T>();
                        foreach (XmlNode node in nodes)
                        {
                            T t = (T)Activator.CreateInstance(typeof(T));
                            foreach (KeyValuePair<string, XmlPropertyMapping> kvp in dicts)
                            {
                                string val = node.Attributes[kvp.Value.PropertyName].Value;
                                if (kvp.Value.Required && string.IsNullOrEmpty(val))
                                    throw new XmlNodeRequiredAttributeException();
                                typeof(T).GetProperty(kvp.Key).SetValue(t, val, null);
                            }
                            list.Add(t);
                        }
                    }
                }
            }
            catch (XmlException xmlEx)
            {
                throw new XmlException(xmlEx.Message, xmlEx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        #region private section
        class XmlPropertyMapping
        {
            public string PropertyName { get; set; }
            public bool Required { get; set; }

            public XmlPropertyMapping(string propertyName, bool required)
            {
                PropertyName = propertyName;
                Required = required;
            }
        }

        private IDictionary<string, XmlPropertyMapping> getXmlNodeProperties(Type t)
        {
            IDictionary<string, XmlPropertyMapping> dicts = new Dictionary<string, XmlPropertyMapping>();
            foreach (PropertyInfo p in getProperties(t))
            {
                object[] o = p.GetCustomAttributes(true);
                if (o != null && o.Length > 0)
                {
                    if (o[0].GetType() == typeof(XmlNodePropertyAttribute))
                    {
                        XmlNodePropertyAttribute attr = (XmlNodePropertyAttribute)o[0];
                        dicts.Add(p.Name, new XmlPropertyMapping(attr.PropertyName, attr.Required));
                    }
                }
            }
            return dicts;
        }

        private IList<PropertyInfo> getProperties(Type t)
        {
            return t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
        #endregion

        public IList<T> GetAll()
        {
            return list;
        }
    }
}
