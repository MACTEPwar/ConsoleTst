using ConsoleTst.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ConsoleTst.Infrastructure;

namespace ConsoleTst.Services
{
    public class SocketServerService : ISocketServer
    {
        SocketServer socket;

        public void Start(int port)
        {
            socket = new SocketServer(port);
            socket.Start();
        }

        public void Stop()
        {
            socket.Stop();
        }

        public void setMesaage(string message)
        {
            socket.message = message;
        }

        public string getMesaage()
        {
            return socket.message;
        }
    }
}
