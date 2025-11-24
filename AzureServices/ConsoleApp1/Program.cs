namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Factory pattern
            var creditPayment = FactoryPattern.GetPayment("credit");
            var paypalPayment = FactoryPattern.GetPayment("paypal");

            // DI pattern
            var payment = new PaymentProcessor(creditPayment);
            payment.Process();

            // TDD pattern
            var fakePayment = new PaymentProcessor(new FakePayment());
            fakePayment.Process();

            // Liskovs Substitution violation
            Rectangle rect = new Square();
            rect.Width = 9;
            rect.Height = 8;

            Console.WriteLine($"Rectangle : {rect.Area()}");

            // DI principle
            Repository repo1 = new Repository(new GitVersion());
            Repository repo2 = new Repository(new SVNVersion());

            repo1.CommitData("checkin code");
            repo2.CommitData("checkin code");


            Console.ReadLine();
        }
    }
}
