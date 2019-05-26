using Broker.Interfases;
using ConsoleTst.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Broker.Services
{
    public class FunctionService : IFunctionService
    {
        public string get(SocketServer socket)
        {
            List<Socket> result = new List<Socket>();
            return socket.clients.First().LocalEndPoint.ToString();
        }

        public string test()
        {
            return "test complete";
        }
    }
}
