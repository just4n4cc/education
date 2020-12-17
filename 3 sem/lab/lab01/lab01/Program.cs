using System;
using System.Collections.Generic;

namespace lab01
{
    class Program
    {
        static List<double> BiquadraticEquation(in double a, in double b, in double c)
        {
            List<double> roots = new List<double>();
            double tempRoot;

            double d = b * b - 4 * a * c;

            if (d >= 0)
            {
                if (c == 0)
                {
                    roots.Add(0);
                }
                if (d > 0)
                {
                    tempRoot = (-b + Math.Sqrt(d)) / (2 * a);
                    if (tempRoot > 0)
                    {
                        roots.Add(Math.Sqrt(tempRoot));
                        roots.Add(-Math.Sqrt(tempRoot));
                    }

                    tempRoot = (-b - Math.Sqrt(d)) / (2 * a);
                    if (tempRoot > 0)
                    {
                        roots.Add(Math.Sqrt(tempRoot));
                        roots.Add(-Math.Sqrt(tempRoot));
                    }
                }
                else
                {
                    tempRoot = (-b / (2 * a));
                    if (tempRoot > 0)
                    {
                        roots.Add(Math.Sqrt(tempRoot));
                        roots.Add(-Math.Sqrt(tempRoot));
                    }
                }
            }
            else
            {
                tempRoot = -b / (2 * a);
                if (tempRoot > 0)
                {
                    roots.Add(Math.Sqrt(tempRoot));
                    roots.Add(-Math.Sqrt(tempRoot));
                }
            }            

            return roots;
        }

        static void PrintRoots(in List<double> roots)
        {
            if (roots.Count != 0)
            {
                for (int i = 0; i < roots.Count; i++)
                {
                    Console.Write("x" + (i + 1) + " = ");
                    Console.ForegroundColor = System.ConsoleColor.Green;
                    Console.WriteLine(roots[i]);
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("Биквдратное уравнение не имеет действительных корней.");
                Console.ResetColor();
            }
        }

        static void PrintEquation(in double a, in double b, in double c)
        {
            string Equation = "";

            if (a != 1)
            {
                if (a == -1)
                    Equation += "-";
                else
                    Equation += a;
            }
            Equation += "x^4";

            if (b != 0)
            {
                if (b > 0)
                {
                    if (b != 1)
                        Equation += "+" + b;
                    else
                        Equation += "+";
                }
                else
                {
                    if (b == -1)
                        Equation += "-";
                    else
                        Equation += b;
                }

                Equation += "x^2";
            }

            if (c != 0)
                if (c > 0)
                    Equation += "+" + c;
                else
                    Equation += c;

            Equation += " = 0";
            Console.WriteLine(Equation);

        }

        static void PrintCreator()
        {
            Console.WriteLine("Студент: Корчевский Александр            Группа: ИУ5-32Б");  
        }

        static void Main(string[] args)
        {
            double a = 0, b = 0, c = 0;
            List<double> roots = new List<double>();

            PrintCreator();

            if (args.Length > 0)
            {
                if ((args.Length != 3) || !(double.TryParse(args[0], out a)) || (a == 0) || !(double.TryParse(args[1], out b)) || !(double.TryParse(args[2], out c)))
                {
                    Console.WriteLine("Коэффициенты введены некорректно."); 
                    return;
                }
            }
            else
            {
                #region Ввод с консоли
                while (a == 0)
                {
                    Console.WriteLine("\nВведите коэффициент А");
                    double.TryParse(Console.ReadLine(), out a);
                }

                Console.WriteLine("\nВведите коэффициент B");

                while (!double.TryParse(Console.ReadLine(), out b))
                {
                    Console.WriteLine("\nВведите коэффициент B");
                }

                Console.WriteLine("\nВведите коэффициент C");

                while (!double.TryParse(Console.ReadLine(), out c))
                {
                    Console.WriteLine("\nВведите коэффициент C");
                }
                #endregion
            }

            PrintEquation(a, b, c);

            roots = BiquadraticEquation(a, b, c);

            PrintRoots(roots);

        }
    }
}
