using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTst.Interfases
{
    public interface ISocketServer
    {
        void Start(int port);
        void Stop();
        void setMesaage(string message);
        string getMesaage();
    }
}
