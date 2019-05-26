using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ConsoleTst.Services;
using ConsoleTst.Interfases;

namespace Broker
{
    public class Program
    {
        static ISocketServer sockSevice = new SocketServerService();

        static void Main(string[] args)
        {
            sockSevice.Start(8005);
        }
    }
}
