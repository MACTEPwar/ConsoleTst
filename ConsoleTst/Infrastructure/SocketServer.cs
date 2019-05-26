using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;  
using System.Threading.Tasks;

namespace ConsoleTst.Infrastructure
{
    public class SocketServer
    {
        IPEndPoint ipPoint;
        Socket listenSocket;
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
                    Socket handler = listenSocket.Accept();
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

                        Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());


                        // отправляем ответ
                        //string message = message;
                        data = Encoding.Unicode.GetBytes(message);
                        handler.Send(data);
                    }
                    // закрываем сокет
                    //handler.Shutdown(SocketShutdown.Both);
                    //handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Stop()
        {

        }
    }
}
