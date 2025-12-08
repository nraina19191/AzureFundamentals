using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public abstract class Animal
    {
        public abstract void Eat();
    }

    public class Dog : Animal
    {
        public override void Eat()
        {
            Console.WriteLine("Dog eats");
        }
    }

    public class Cat(string type) : Animal
    {
        public override void Eat()
        {
            Console.WriteLine("Cat eats");
        }
    }
}
