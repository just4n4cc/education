using System;
using System.Collections;
using System.Collections.Generic;
using FigureCollections;
using MyShapes;
using Simple;

namespace lab03
{
    class Program
    {
        static void Main(string[] args)
        {
            Rectangle r = new Rectangle(2,3);
            Circle c = new Circle(3);
            Square s = new Square(4);

            ArrayList ShapesArr = new ArrayList();
            ShapesArr.Add(r);
            ShapesArr.Add(c);
            ShapesArr.Add(s);
            ShapesArr.Sort();
            Console.WriteLine("Вывод элементов из ArrayList");
            foreach(object o in ShapesArr)
            {                
                Console.WriteLine(o.ToString());
            }
            Console.WriteLine();

            List<Shape> ShapesList = new List<Shape>();
            ShapesList.Add(r);
            ShapesList.Add(c);
            ShapesList.Add(s);
            Console.WriteLine("Вывод элементов из List<>");
            ShapesList.Sort();
            foreach(Shape i in ShapesList)
            {
                i.Print();
            }
            Console.WriteLine();

            SimpleStack<Shape> stack = new SimpleStack<Shape>();
            stack.Push(r);
            stack.Push(c);
            stack.Push(s);
            stack.Pop().Print();
            stack.Pop().Print();
            stack.Pop().Print();
            Console.WriteLine();

            Console.WriteLine("\nМатрица");
            Matrix<Shape> matrix = new Matrix<Shape>(3, 3, 3, new ShapeMatrixCheckEmpty());
            matrix[0, 0, 0] = r;
            matrix[1, 1, 1] = c;
            matrix[2, 2, 2] = s;
            Console.WriteLine(matrix.ToString());
            Console.WriteLine();
        }

    }
}
