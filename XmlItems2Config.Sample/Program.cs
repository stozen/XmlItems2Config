using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlItems2Config.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            //IList<NameInfo> items = XmlItemsRepository<NameInfo>.getInstance("Names.xml", "name").GetAll();
            IList<NameInfo> items = new XmlItemsRepository<NameInfo>("Names.xml", "name").GetAll();
            foreach (NameInfo item in items)
            {
                Console.WriteLine("ID:" + item.ID + "\t" + item.FirstName + " " + item.LastName);
            }
            Console.ReadLine();
        }
    }
}
