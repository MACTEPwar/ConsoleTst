using ConsoleTst.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Broker.Interfases
{
    public interface IFunctionService
    {
        string get(SocketServer socket);
        string test();
    }
}
