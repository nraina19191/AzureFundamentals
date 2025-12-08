using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public abstract class TemplateDesignPattern
    {
        public abstract void ProcessData();

        public void GenerateReport()
        {
            ProcessData();
            Console.WriteLine("Completed");
        }
    }

    public class UserReport : TemplateDesignPattern
    {
        public override void ProcessData()
        {
            Console.WriteLine("Process data");
        }
    }
}
