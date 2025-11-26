using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    // Product inteface
    public interface IPayment {
        void PaymentProcess();
    }


    // Concrete product
    public class CreditCardPayment : IPayment
    {
        public void PaymentProcess()
        {
            Console.WriteLine("Credit Card payment");
        }
    }

    // Concrete product
    public class PayPalPayment : IPayment
    {
        public void PaymentProcess()
        {
            Console.WriteLine("Paypal");
        }
    }

    // Creator
    public abstract class MPaymentProcessor
    {
        // Factory
        public abstract IPayment CreatePaymentMethod();

        public void ProcessPayment(decimal amount) {
            var payment = CreatePaymentMethod();
            payment.PaymentProcess();
        }
    }

    //  Concrete creators - Credit card factory
    public class CreditCardPaymentProcessor : MPaymentProcessor
    {
        public override IPayment CreatePaymentMethod()
        {
            return new CreditCardPayment();
        }
    }

    //  Concrete creators - Paypal factory
    public class PayPalPaymentProcessor : MPaymentProcessor
    {
        public override IPayment CreatePaymentMethod()
        {
            return new PayPalPayment();
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
