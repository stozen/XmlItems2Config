using System;
using System.Collections.Generic;

namespace XmlItems2Config
{
    /// <summary>
    /// 列表配置仓库
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XmlItemsRepository<T> where T : IItem
    {
        /*
        private static volatile XmlItemsRepository<T> instance = null;
        private static object syncRoot = new Object();
         * */
        private IList<T> list;

        /*
        static XmlItemsRepository() { }

        private XmlItemsRepository(string configPath, string nodeItemName)
        {
            try
            {
                XmlCollectionConfigHelper<T> helper = new XmlCollectionConfigHelper<T>();
                helper.Load(System.AppDomain.CurrentDomain.BaseDirectory + configPath, nodeItemName);
                list = helper.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static XmlItemsRepository<T> getInstance(string configPath, string nodeItemName)
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new XmlItemsRepository<T>(configPath, nodeItemName);
                    }
                }
            }
            return instance;
        }
         * */

        public XmlItemsRepository(string configPath, string nodeItemName)
        {
            try
            {
                XmlCollectionConfigHelper<T> helper = new XmlCollectionConfigHelper<T>();
                helper.Load(System.AppDomain.CurrentDomain.BaseDirectory + configPath, nodeItemName);
                list = helper.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public IList<T> GetAll()
        {
            return list;
        }

        public T Get(string id)
        {
            if (list != null && list.Count > 0)
            {
                foreach (T data in list)
                {
                    if (data.ID == id)
                        return data;
                }
                return default(T);
            }
            else
                return default(T);
        }
    }
}
