using Broker.Interfases;
using Broker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTst.Infrastructure
{
    public class SocketServer
    {
        IPEndPoint ipPoint;
        Socket listenSocket;
        Socket handler;

        public List<Socket> clients = new List<Socket>();

        public string message = "Ваше сообщение доставлено";

        public SocketServer(int port)
        {
            // получаем адреса для запуска сокета
            ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

            // создаем сокет
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);

                // начинаем прослушивание
                listenSocket.Listen(10);

                // Сервер запущен
                Console.WriteLine("Сервер запущен");

                while (true)
                {
                    handler = listenSocket.Accept();
                    new Thread(() =>
                    {
                        
                        clients.Add(handler);
                        // получаем сообщение
                        StringBuilder builder = new StringBuilder();
                        int bytes = 0; // количество полученных байтов
                        byte[] data = new byte[256]; // буфер для получаемых данных

                        while (true)
                        {
                            //Array.Clear(data, 0, data.Length);
                            //data = new byte[256];
                            builder.Clear();
                            do
                            {
                                bytes = handler.Receive(data);
                                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                            }
                            while (handler.Available > 0);

                            //Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());


                            // отправляем ответ
                            data = Encoding.Unicode.GetBytes(DoFunction(builder.ToString()));
                            handler.Send(data);
                        }
                    }).Start();

                    // закрываем сокет

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Stop()
        {
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }

        public string DoFunction(string comand)
        {
            ComandPull comands = new ComandPull();
            comand.Split('\n').ToList().ForEach(cmd => comands.Add(new Comand(cmd)));
            return comands.ExecutePull();
        }
    }

    public class ComandPull
    {
        private List<Comand> comands;

        public ComandPull()
        {
            comands = new List<Comand>();
        }

        public void Add(Comand cmd)
        {
            comands.Add(cmd);
        }

        public void Clear()
        {
            comands.Clear();
        }

        public string ExecutePull()
        {
            var response = string.Empty;
            if (comands.Count > 0)
            {
                foreach (Comand cmd in comands)
                {
                    response += ExecuteComand(cmd);
                }
            }
            return response;
        }

        private string ExecuteComand(Comand cmd)
        {
            IFunctionService service = new FunctionService();
            MethodInfo method = service.GetType().GetMethod(cmd.ComandTitle);
            if (method == null) return "Такого метода нет";
            return method.Invoke(service, null).ToString();
        }
    }

    public class Comand
    {
        public string ComandTitle { get; set; }
        public Dictionary<string, string> Arguments = new Dictionary<string, string>();
        public Comand(string cmd)
        {
            Regex r = new Regex(@"\s+");
            string trims = r.Replace(cmd, @" ").Trim();
            var args = trims.Split(' ');
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Length > 0)
                {
                    if (i == 0) ComandTitle = args[i];
                    if (args[i][0] == '-')
                    {
                        if (i != args.Length - 1)
                        {
                            if (args[i + 1][0] != '-')
                            {
                                Arguments.Add(args[i], args[i + 1]);
                            }
                            else
                            {
                                Arguments.Add(args[i], null);
                            }
                        }
                    }
                }
            }
        }
    }
}
