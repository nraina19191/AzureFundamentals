namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Program p = new Program();

            // Simple Factory pattern
            var creditPayment = FactoryPattern.GetPayment("credit");
            var paypalPayment = FactoryPattern.GetPayment("paypal");

            // Factory Pattern
            MPaymentProcessor paymentProcessor = new CreditCardPaymentProcessor();
            paymentProcessor.ProcessPayment(1.1M);

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

            // Tuples examples
            var dict = new Dictionary<string, (string, string)> {
                { "Key1", ("Value2", "Value3") }
            };

            // abstract methods
            Animal creature = new Dog();
            creature.Eat();

            // template data design pattern
            TemplateDesignPattern templateDesign = new UserReport();
            templateDesign.GenerateReport();
            SingletonPattern.GetInstance().ShowMessage();

            // strategy pattern
            var order = new Order(new SqlStore());
            order.SaveOrder();

            var orderToOracle = new Order(new OracelStore());
            orderToOracle.SaveOrder();

            var ids = p.GetUsersAsync().Result;
            Console.WriteLine(string.Join(',', ids));

            Console.ReadLine();
        }

        private async Task<IEnumerable<int>> GetUsersAsync() {
            var ids = Task.Run(() => Enumerable.Range(1, 100)).Result;

            return ids;
        }

        private IEnumerable<int> GetIds() {
            var ids = Task.Run(() => Enumerable.Range(1, 100)).GetAwaiter().GetResult();

            return ids;
        }

        private async void GetIds1()
        {
            var ids = Task.Run(() => Enumerable.Range(1, 100)).GetAwaiter().GetResult();
        }
    }
}
