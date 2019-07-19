using ConsoleTst.Infrastructure;
using ConsoleTst.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTst
{
    public static class Fun
    {
        public static string GetReflectedPropertyValue(this object subject, string field)
        {
            object reflectedValue = subject.GetType().GetProperty(field).GetValue(subject, null);
            return reflectedValue != null ? reflectedValue.ToString() : "";
        }
    }

    class Program
    {
        
        //public static List<string> splt(string str, int sep)
        //{
        //    List<string> res = new List<string>();
        //    int lastInd = 0;
        //    string curStr = string.Empty;
        //    for (int i = 0; i < str.Length; i++)
        //    {
        //        if ((i + 1) % sep == 0)
        //        {
        //            for (int j = lastInd; j <= i; j++)
        //            {
        //                curStr += str[j];
        //            }
        //            res.Add(curStr);
        //            curStr = string.Empty;
        //            lastInd = i + 1;
        //        }
        //    }
        //    return res;
        //}

        //public static char max(string str)
        //{
        //    Dictionary<char, int> dict = new Dictionary<char, int>();
        //    str.ToList().ForEach(e =>
        //    {
        //        try
        //        {
        //            dict[e]++;
        //        }
        //        catch
        //        {
        //            dict.Add(e, 1);
        //        }
        //    });
        //    KeyValuePair<char,int> max = new KeyValuePair<char, int>(' ',0);
        //    foreach(var item in dict)
        //    {
        //        if (item.Value > max.Value)
        //        {
        //            max = new KeyValuePair<char, int>(item.Key,item.Value);
        //        }
        //    }
        //    return max.Key;
        //}

        public static void Write(string text)
        {
            Console.Write("asd123");

            //text = DateTime.Now.ToString() + "\t" + text + Environment.NewLine;
            //FileStream fstream = new FileStream(@"D:\Logs.txt", FileMode.Append);
            //byte[] array = System.Text.Encoding.Default.GetBytes(text);
            //fstream.Write(array, 0, array.Length);
            //fstream.Close();
        }

        public static string fffff(int t)
        {
            return t.ToString();
        }

        public static void DisplayText(Func<string> text)
        {
            Console.WriteLine(text());
        }

        static void Main(string[] args)
        {
            Console.Write("HELP!!!");

            var method = typeof(List<int>).GetMethod("Contains", new Type[] { typeof(int) });
            // Func<string> fnc = () => fffff("asd");
            //DisplayText(() => fffff(123));
            //List<Person> peoples = new List<Person>();
            //peoples.Add(new Person("Вася", 12, "4Б"));
            //peoples.Add(new Person("Петя", 13, "11А"));
            //peoples.Add(new Person("Галя", 19, "11А"));
            //peoples.Add(new Person("Анна", 15, "6А"));

            //List<Test> obj = new List<Test>();
            //obj.Add(new Test() { Title = "Пара", Num = 12 });
            //obj.Add(new Test() { Title = "Фвыфыв", Num = 13 });
            //obj.Add(new Test() { Title = "щзщшш", Num = 19 });
            //obj.Add(new Test() { Title = "апрпа", Num = 15 });

            //string str = "а";

            //var sers = obj.Where(m => m.Title.Contains(str)).Select(m => m.Num).ToList();

            //ParameterExpression parameter = Expression.Parameter(typeof(Person), "p");

            //var method = typeof(List<int>).GetMethod("Contains", new Type[] { typeof(int)});

            //Expression condition = Expression.Call(Expression.Property(parameter, nameof(Person.Age)), method, Expression.Constant(sers));

            //var list2 = Expression.Constant(sers);

            //var param = Expression.Parameter(typeof(Person), "p");
            //var value = Expression.Property(param, "Age");
            //var condition = Expression.Call(Expression.Constant(sers), method, value);

            //var lambda = Expression.Lambda<Func<Person, bool>>(condition, param);

            //var ppp = peoples.AsQueryable().Where(lambda).ToList();

            //var ppp = peoples.Where(e => sers.Contains(e.Age)).ToList();

            //ppp = peoples.Where(e => obj.Where(m => m.Title.Contains(str)).Contains(e.Age)).ToList();

            //ppp.ForEach(pp => Console.WriteLine(pp.ToString()));


            //Write("123");

            //splt("111000000111000", 5).ForEach(e => Console.WriteLine(max(e)));
            //string dateString = "Mon 16 Jun 8:30 AM 2008";
            //string format = "ddd dd MMM h:mm tt yyyy";

            //Console.WriteLine(Thread.CurrentThread.CurrentCulture.ToString());

            //DateTime dddd = DateTime.Parse("11.12.2018 14:12");



            //Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            //Console.WriteLine(dddd.ToString());
            //string dateString = "02.12.2010 10:21";
            //string[] formats = { "MM/dd/yyyy hh:mm", "dd.MM.yyyy hh:mm" };
            //DateTime dateTime;
            //if (DateTime.TryParseExact(dddd.ToString(), formats, CultureInfo.InvariantCulture,
            //    DateTimeStyles.None, out dateTime))
            //{
            //    Console.WriteLine(dateTime);
            //}

            //Console.WriteLine(Thread.CurrentThread.CurrentCulture.ToString()); 


            //Console.ReadKey();
            //var rr = DateTime.Parse(str_in);
            //var re = DateTime.Parse(str_in + " 23:59:59");
            //int a = 1;
            //FunctionService.GetIpNetARPTable();
            //Class1 cl = new Class1();
            //cl.start();
            //List<Test> ls = new List<Test>() {
            //    new Test(){ Title = "asd", Num = 123},
            //    new Test(){ Title = "!r3", Num = 6787},
            //    new Test(){ Title = "i8t", Num = 423},
            //};
            //Display<Test>(ls.OrderBy(l => l.GetReflectedPropertyValue("Num")).ToList());

            //List<Person> persons = new List<Person>();
            //persons.Add(new Person("Вася", 12, "4Б"));
            //persons.Add(new Person("Петя", 12, "11А"));
            //persons.Add(new Person("Галя", 19, "11А"));
            //persons.Add(new Person("Анна", 15, "6А"));

            //Display<Person>(persons);
            //Console.WriteLine();

            //Expression<Func<Person, bool>> expressionWhereReceipt = (p => true);//по умолчанию все
            //ParameterExpression parameter = Expression.Parameter(typeof(Person), "p");
            //Expression conditions = null;

            //Expression left = Expression.PropertyOrField(parameter, "Age");
            //Expression right = Expression.Constant(11, typeof(int));
            //var condition = Expression.GreaterThan(left, right);

            //conditions = condition;

            //expressionWhereReceipt = Expression.Lambda<Func<Person, bool>>(conditions, parameter);

            //var ms= persons.AsQueryable().Where(expressionWhereReceipt).ToList<Person>();

            //Display<Person>(ms);
            //Console.WriteLine();

            //var model = ms.AsQueryable();

            //Expression<Func<Person, string>> expressionOrderByReceipt = (p => "Age");//по умолчанию все

            //var type = typeof(Person);
            //var property = type.GetProperty("Age");
            //var propertyAccess = Expression.Property(parameter, property);
            //var orderByExp = Expression.Lambda(propertyAccess, parameter);
            //MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderBy", new[] { type, property.PropertyType }, model.Expression, Expression.Quote(orderByExp));
            //var reqw = (IEnumerable<Person>)Expression.Lambda(resultExp).Compile().DynamicInvoke();
            ////var reqw = model.OrderBy(expressionOrderByReceipt).ToList<Person>();

            //Display<Person>(reqw.ToList());
            //Console.WriteLine();

            //var result = Sort(new Dictionary<string, string>() { { "Age", "Asc" } });

            //Display<Person>(result);
            //Console.WriteLine();

            //var result2 = persons.OrderByDescending(p => p.GetReflectedPropertyValue("Age")).ToList();

            //Display<Person>(result2);
            //Console.WriteLine();

            List<Person> Sort(List<Person> persons, Dictionary<string, string> sort)
            {
                var r = sort.First().Key;
                Expression<Func<Person, string>> expressionGroupBy = (p => sort.First().Key);//по умолчанию все
                                                                                             //ParameterExpression parameter = Expression.Parameter(typeof(Person), "p");


                var v1 = persons.AsQueryable();
                var v2 = v1.OrderBy(expressionGroupBy);


                //return persons.AsQueryable().OrderBy(expressionGroupBy).ToList();
                return v2.ToList();
            }



            void Display<T>(List<T> list)
            {
                list.ForEach(l => Console.WriteLine(l.ToString()));
            }

            List<Person> f(List<Person> persons, int? age = null)
            {
                var res = persons.Where(m => m.Age == age || age == null).ToList();

                return res;
            }

            List<Person> ff(List<Person> persons, string filter = null)
            {
                var res = persons.Where(m => filter == null || m.Name.Contains(filter));//.ToList();

                return res.ToList();
            }

            void fff(Person p)
            {
                if (3 > 10 && p.Age > 15)
                {
                    /////
                }
                else
                {
                    ////
                }
            }

        }


        public class Test
        {
            public string Title { get; set; }
            public int Num { get; set; }

            public override string ToString()
            {
                return $"{Title}  {Num}";
            }
        }

        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Class { get; set; }

            public Person(string name, int age, string _class)
            {
                Name = name;
                Age = age;
                Class = _class;
            }

            public override string ToString()
            {
                return $"{Name} {Age} {Class}";
            }
        }
    }
}
