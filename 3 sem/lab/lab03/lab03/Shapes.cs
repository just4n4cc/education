using System;
using System.Collections;
using System.Collections.Generic;

namespace MyShapes
{
    interface IPrint
    {
        void Print();
    }

    abstract class Shape : IComparable, IPrint
    {
        private string _type;
        public string type
        {
            get => _type;
            protected set
            {
                _type = value;
            }
        }
        public abstract double Area();
        public override string ToString()
        {
            return type + " площадью " + Area().ToString();
        }
        public abstract void Print();
        public int CompareTo(object other)
        {
            Shape s = other as Shape;
            if (this.Area() > s.Area())
            {
                return 1;
            }
            if (s.Area() < this.Area())
            {
                return -1;
            }
            return 0;
        }
    }
    class Rectangle : Shape
    {
        private double _width;
        private double _height;
        public double width
        {
            get => _width;
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Width can not be < 0, it is set to 0.");
                    _width = 0;
                }
                else
                    _width = value;
            }
        }
        public double height
        {
            get => _height;
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Height can not be < 0, it is set to 0.");
                    _height = 0;
                }
                else
                    _height = value;
            }
        }
        public override double Area() => width * height;
        public Rectangle(double Width, double Height)
        {
            width = Width;
            height = Height;
            type = "Прямоугольник";
        }
        public override string ToString()
        {
            return base.ToString() + ", высота - " + height + ", ширина - " + width;
        }
        public override void Print()
        {
            Console.WriteLine(this.ToString());
        }
    }
    class Square : Rectangle
    {
        public Square(double Length) : base(Length, Length)
        {
            type = "Квадрат";
        }
        public override string ToString()
        {
            return type + " площадью " + Area().ToString() + ", длина - " + width;
        }
    }
    class Circle : Shape
    {
        private double _radius;
        public double radius { get; set; }
        public override double Area() => Math.PI * radius * radius;
        public override string ToString()
        {
            return base.ToString() + ", радиус - " + radius;
        }
        public override void Print()
        {
            Console.WriteLine(ToString());
        }
        public Circle(double Radius) { radius = Radius; type = "Круг"; }
    }
   
}
