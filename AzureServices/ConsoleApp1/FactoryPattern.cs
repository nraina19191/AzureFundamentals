using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    interface IPayment {
        void PaymentProcess();
    }

    class CreditCardPayment : IPayment
    {
        public void PaymentProcess()
        {
            Console.WriteLine("Credit Card payment");
        }
    }

    class PayPalPayment : IPayment
    {
        public void PaymentProcess()
        {
            Console.WriteLine("Paypal");
        }
    }

    internal class FactoryPattern
    {
        public static IPayment GetPayment(string type) {
            switch (type)
            {
                case "credit":
                    return new CreditCardPayment();
                case "paypal":
                    return new PayPalPayment();
                default:
                    throw new Exception("No payment support");
            }
        }
    }
}
