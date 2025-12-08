using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class SingletonPattern
    {
        private static SingletonPattern _instance;
        private static readonly object _lock = new object();
        // Private constructor to prevent instantiation from outside
        private SingletonPattern()
        {
        }
        public static SingletonPattern GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SingletonPattern();
                    }
                }
            }
            return _instance;
        }
        public void ShowMessage()
        {
            Console.WriteLine("Singleton Pattern Example");
        }
    }
}
