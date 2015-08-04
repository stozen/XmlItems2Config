using System.Collections.Generic;

namespace XmlItems2Config
{
    public interface ICollectionConfigHelper<T> where T : IItem
    {
        IList<T> GetAll();
    }
}
