using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Customer
    {
        public double Bill { get; set; }
        public void PlayTheBill()
        {
            Console.WriteLine("I will play ${0}",this.Bill);
        }
    }
}
