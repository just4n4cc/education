using System;

namespace lab02
{
    class Program
    {
        interface IPrint
        {
            void Print();
        }
        abstract class Shape
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

        }

        class Rectangle : Shape, IPrint
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
                return base.ToString() + "\nВысота - " + height + "\nШирина - " + width + "\n";
            }

            public void Print()
            {
                Console.WriteLine(this.ToString());
            }
        }

        class Square : Rectangle, IPrint
        {
            public Square(double Length) : base (Length, Length)
            {
                type = "Квадрат";
            }
            public override string ToString()
            {
                return type + " площадью " + Area().ToString() + "\nДлина - " + width + "\n";
            }
        }

        class Circle : Shape, IPrint
        {
            private double _radius;

            public double radius { get; set; }

            public override double Area() => Math.PI * radius * radius;

            public override string ToString()
            {
                return base.ToString() + "\nРадиус - " + radius + "\n";
            }

            public void Print()
            {
                Console.WriteLine(ToString());
            }

            public Circle(double Radius) { radius = Radius; type = "Круг"; }
        }

        static void Main(string[] args)
        {
            Rectangle a = new Rectangle(2, 3);
            a.Print();
            Square b = new Square(4);
            b.Print();
            Circle c = new Circle(0.564);
            c.Print();
        }
    }
}
