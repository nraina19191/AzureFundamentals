using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class FakePayment : IPayment
    {
        public void PaymentProcess()
        {
            Console.WriteLine("Fake payment");
        }
    }
}
