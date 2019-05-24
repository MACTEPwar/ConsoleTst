using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTst.Interfases
{
    interface IFunctionService
    {
        // Выводит в консоль все локальные ip и MAC адреса
        void GetIpNetARPTable();
    }
}
