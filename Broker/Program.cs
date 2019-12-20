using ConsoleTst.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Broker
{
    public class Shape
    {
        public string title;
        public Shape(string t)
        {
            this.title = t;
        }
    }

    public class Circle : Shape
    {
        public Circle() : base("Треугольник")
        {

        }


    }

    public class Kl2
    {
        public Kl2()
        {

        }
        public Kl2(int s)
        {

        }
        public Kl2(double s)
        {

        }
    }

    class Program
    {
        static Random r;

        static void Main(string[] args)
        {
            r = new Random();

            List<int> list = new List<int>();
            for(int i = 0; i < 200; i++)
            {
                list.Add(r.Next(0, 40));
            }

            list.OrderBy(s => s);

            for (int i = 0; i < 200; i++)
            {
                Console.Write(list[i].ToString() + " ");
            }
        }
    }
}
