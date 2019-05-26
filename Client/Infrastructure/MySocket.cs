using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Infrastructure
{
    public class MySocket
    {
        Socket socket;
        IPEndPoint ipPoint;

        public MySocket(string address, int port)
        {
            ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect()
        {
            try
            {
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);
            }
            catch (Exception ex)
            {
                this.Stop();
            }
        }

        public string Send(string message)
        {
            //string message = "asddfgrqw rdsf dsf sdf ";
            byte[] data = Encoding.Unicode.GetBytes(message);
            socket.Send(data);

            // получаем ответ
            data = new byte[256]; // буфер для ответа
            StringBuilder builder = new StringBuilder();
            int bytes = 0; // количество полученных байт

            do
            {
                bytes = socket.Receive(data, data.Length, 0);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (socket.Available > 0);
            return builder.ToString();
        }

        public void Stop()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
