using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab06.Reflection
{
    // 4
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute() { }
        public DescriptionAttribute(string description)
        {
            Description = description;
        }
        public string Description { get; set; }
    }

    // 2
    class Animal
    {
        public Animal() { }
        public Animal(int Age) { }
        public Animal(int Age, string Name, string Speech) { age = Age; name = Name; speech = Speech; }

        public void Speak() 
        {
            Console.WriteLine(speech);
        }
        public void Move() { }

        public int age
        {
            get { return _age; }
            set { _age = value; }
        }
        private int _age;

        [DescriptionAttribute(Description = "Название этого животного.")]
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _name;

        [DescriptionAttribute("Звук, который издает это животное.")]
        public string speech
        {
            get { return _speech; }
            set { _speech = value; }
        }
        private string _speech;
    }

    class Program
    {
        static bool CheckAttribute(PropertyInfo checkType, Type attributeType, out object attribute)
        {
            bool Res = false;
            attribute = null;

            var isAttribute = checkType.GetCustomAttributes(attributeType, false);
            if (isAttribute.Length > 0)
            {
                Res = true;
                attribute = isAttribute[0];
            }

            return Res;
        }
        static void Main(string[] args)
        {
            // 3
            Type t = typeof(Animal);

            Console.WriteLine("Конструкторы: ");
            foreach (var i in t.GetConstructors())
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("\nМетоды: ");
            foreach (var i in t.GetMethods())
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("\nСвойства: ");
            foreach (var i in t.GetProperties())
            {
                Console.WriteLine(i);
            }

            // 5
            Console.WriteLine("\nСвойства с атрибутом DescriptionAttribute:");
            foreach (var i in t.GetProperties())
            {
                object attrObj;
                if (CheckAttribute(i, typeof(DescriptionAttribute), out attrObj))
                {
                    DescriptionAttribute attr = attrObj as DescriptionAttribute;
                    Console.WriteLine(i.Name + " - " + attr.Description);
                }
            }

            // 6
            Animal Cow = new Animal(5, "Cow", "Moooooo");
            t.InvokeMember("Speak", BindingFlags.InvokeMethod, null, Cow, null);
        }
    }
}
