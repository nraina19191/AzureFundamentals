using System.Runtime.CompilerServices;

namespace AsyncConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
        }
    }

    public class AsyncExample
    {

        public void GetData() {
            var est = FetchDataAsync().Result;

            var testactions = new List<Action>
            {
                () => Console.WriteLine("Action 1 executed."),
                () => Console.WriteLine("Action 2 executed."),
                () => Console.WriteLine("Action 3 executed.")
            };

            Parallel.ForEach(testactions, new ParallelOptions { MaxDegreeOfParallelism = 3 }, action => action());
        }

        public async Task<string> FetchDataAsync()
        {
            await Task.Delay(1000); // Simulate an asynchronous operation
            return "Data fetched asynchronously!";
        }
    }

    public class CustomAwaiter : INotifyCompletion
    {
        public bool IsCompleted { get; }

        public void OnCompleted(Action continuation) {

        }

        public string GetResult() {
            return "";
        }

        public CustomAwaiter GetAwaiter() {
         return this;
        }
    }
}
