using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class PaymentProcessor
    {
        private readonly IPayment _paymentType;
        public PaymentProcessor(IPayment paymentType)
        {
            this._paymentType = paymentType;
        }

        public void Process() {
            _paymentType.PaymentProcess();
        }
    }
}
