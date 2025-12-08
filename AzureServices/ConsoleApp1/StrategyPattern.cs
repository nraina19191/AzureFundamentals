using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface IStore
    {
        void StoreData();
    }

    public class SqlStore : IStore
    {
        public void StoreData()
        {
            Console.WriteLine("Saving sql");
        }
    }

    public class OracelStore : IStore
    {
        public void StoreData()
        {
            Console.WriteLine("Saving oracle");
        }
    }

    public class Order
    {
        IStore _store;
        public Order(IStore store)
        {
            this._store = store;
        }

        public void SaveOrder()
        {
            this._store.StoreData();
        }
    }
}
