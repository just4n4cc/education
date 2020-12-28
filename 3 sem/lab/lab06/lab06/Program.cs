using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab06
{
    class Program
    {
        // 1
        delegate void Printin(int p1, double p2);

        // 2
        static void ValPrint(int p1, double p2)
        {
            Console.WriteLine("Your integer: " + p1);
            Console.WriteLine("Your double: " + p2);
        }

        // 3.1
        static void Phoo(Printin func, int p1, double p2)
        {
            func(p1, p2);
        }

        // 4.1
        static void PhooAction(Action<int, double> func, int p1, double p2)
        {
            func(p1, p2);
        }

        static void Main(string[] args)
        {
            // 3.2
            Phoo(ValPrint, 1, 2.1);

            // 3.3
            Printin lambda = (p1, p2) =>
            {
                Console.WriteLine("Sum of integer and double: " + (p1 + p2));
            };
            Phoo(lambda, 1, 1.1);

            // 4.2
            Action<int, double> lmbd = (p1, p2) =>
            {
                Console.WriteLine("Sum of integer and double: " + (p1 + p2));
            };
            PhooAction(ValPrint, 1, 1.1);
            PhooAction(lmbd, 1, 2.1);

        }
    }
}
