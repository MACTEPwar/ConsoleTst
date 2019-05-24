using ConsoleTst.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTst
{
    class Program
    {
        static void Main(string[] args)
        {
            FunctionService.GetIpNetARPTable();
        }  
    }
}
