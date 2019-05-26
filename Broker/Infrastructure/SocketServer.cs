using Broker.Interfases;
using Broker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
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
                    new Thread(() => {
                        handler = listenSocket.Accept();
                        clients.Add(handler);
                        // получаем сообщение
                        StringBuilder builder = new StringBuilder();
                        int bytes = 0; // количество полученных байтов
                        byte[] data = new byte[256]; // буфер для получаемых данных

                        while (true)
                        {
                            do
                            {
                                bytes = handler.Receive(data);
                                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                            }
                            while (handler.Available > 0);

                            //Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());
                            

                            // отправляем ответ
                            //string message = message;
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
            IFunctionService service = new FunctionService();
            MethodInfo method = service.GetType().GetMethod(comand);
            if (method == null) return "Такого метода нет";
            return method.Invoke(service, null).ToString();
        }
    }
}
