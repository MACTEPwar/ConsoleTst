using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Broker.Infrastructure
{
    public class StateObject
    {
        // сокет-клиент 
        public Socket workSocket = null;
        // размер буфера.  
        public const int BufferSize = 1024;
        // буфер для получателя
        public byte[] buffer = new byte[BufferSize];
        // полученная информация 
        public StringBuilder sb = new StringBuilder();
    }


    public class SocketAsync
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public SocketAsync() { }

        public static void StartListening()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 8005);

            // Создаю сокет  
            Socket listener = new Socket(ipAddress.AddressFamily,SocketType.Stream, ProtocolType.Tcp);

            // привязываю socket к локальному endpoint и слушаю  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Устанавливаю в выключенное состояние 
                    allDone.Reset();

                    
                    // Начинаю ассинхронное прослоушивание 
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // ожидаю подключение
                    allDone.WaitOne();

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            // даю сигнал главному потоку для продолжения работы
            allDone.Set();

            // получаю сокет, который обрабатывает запрос клиента
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // создаю объект состояния 
            StateObject state = new StateObject();
            state.workSocket = handler;
            
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
           // while (true)
            {
                String content = String.Empty;

                //Получаю состояние и сокет-обработчик 
                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.workSocket;

                // Считываю данные из  сокета-клиента 
                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // сохраняю полученные данные.  
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    // проверяю всю ли инфу я считал 
                    content = state.sb.ToString();
                    //if (content.IndexOf("<EOF>") > -1)
                    if (content.Length > 0)
                    {
                        // записую на сервере
                        Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                            content.Length, content);
                        // шлю клиенту
                        Send(handler, content);
                    }
                    else
                    {
                        // если считано не все  
                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReadCallback), state);
                    }
                }
            }
            
        }

        private static void Send(Socket handler, String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // передаю
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // получаю сокет из объекта состояния.  
                Socket handler = (Socket)ar.AsyncState;

                // завершаю передачу данных на удаленное устройство 
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
