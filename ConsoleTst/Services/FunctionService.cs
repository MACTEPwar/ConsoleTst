using ConsoleTst.Interfases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTst.Services
{
    //Определяем структуру MIB_IPNETROW.
    [StructLayout(LayoutKind.Sequential)]
    struct MIB_IPNETROW
    {
        [MarshalAs(UnmanagedType.U4)]
        public int dwIndex;
        [MarshalAs(UnmanagedType.U4)]
        public int dwPhysAddrLen;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac0;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac1;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac2;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac3;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac4;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac5;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac6;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac7;
        [MarshalAs(UnmanagedType.U4)]
        public int dwAddr;
        [MarshalAs(UnmanagedType.U4)]
        public int dwType;
    }

    class FunctionService : IFunctionService
    {
        [DllImport("IpHlpApi.dll")]
        [return: MarshalAs(UnmanagedType.U4)]
        static extern int GetIpNetTable(
            IntPtr pIpNetTable,
            [MarshalAs(UnmanagedType.U4)] ref int pdwSize,
            bool bOrder
        );

        [DllImport("IpHlpApi.dll", SetLastError = true,
                                   CharSet = CharSet.Auto)]
        internal static extern int FreeMibTable(IntPtr plpNetTable);
        const int ERROR_INSUFFICIENT_BUFFER = 122;

        void IFunctionService.GetIpNetARPTable()
        {
            FunctionService.GetIpNetARPTable();
        }

        public static void GetIpNetARPTable()
        {
            // The number of bytes needed.
            int bytesNeeded = 0;

            //Результат вызова API.
            int result = GetIpNetTable(IntPtr.Zero, ref bytesNeeded, false);

            if (result != ERROR_INSUFFICIENT_BUFFER)
            {
                //Создаем исключение
                throw new Win32Exception(result);
            }

            IntPtr buffer = IntPtr.Zero;

            try
            {
                //Выделение памяти
                buffer = Marshal.AllocCoTaskMem(bytesNeeded);

                // Make the call again. If it did not succeed, then
                // raise an error.
                result = GetIpNetTable(buffer, ref bytesNeeded, false);

                // If the result is not 0 (no error), then throw an exception.
                if (result != 0)
                {
                    //Создаем исключение
                    throw new Win32Exception(result);
                }

                // Now we have the buffer, we have to marshal it. We can read
                // the first 4 bytes to get the length of the buffer.
                int entries = Marshal.ReadInt32(buffer);

                // Increment the memory pointer by the size of the int.
                IntPtr currentBuffer = new IntPtr(buffer.ToInt64() +
                   Marshal.SizeOf(typeof(int)));

                // Allocate an array of entries.
                MIB_IPNETROW[] table = new MIB_IPNETROW[entries];

                // Cycle through the entries.
                for (int index = 0; index < entries; index++)
                {
                    // Call PtrToStructure, getting the structure information.
                    table[index] = (MIB_IPNETROW)Marshal.PtrToStructure(new
                     IntPtr(currentBuffer.ToInt64() + (index *
                     Marshal.SizeOf(typeof(MIB_IPNETROW)))), typeof(MIB_IPNETROW));
                }


                for (int index = 0; index < entries; index++)
                {
                    MIB_IPNETROW row = table[index];

                    //Получение IP адреса
                    System.Net.IPAddress ip =
                     new System.Net.IPAddress(BitConverter.GetBytes(table[index].dwAddr));

                    //Собираем MAC адрес по кусочкам
                    string MAC = row.mac0.ToString("X2") + '-' + row.mac1.ToString("X2") + '-' +
                        row.mac2.ToString("X2") + '-' + row.mac3.ToString("X2") + '-' +
                        row.mac4.ToString("X2") + '-' + row.mac5.ToString("X2");
                    if (MAC == "00-00-00-00-00-00") continue;
                    //Добавляем новую строку в таблицу
                    Console.Write(ip.ToString() + "   ");
                    Console.WriteLine(MAC);
                }
            }
            finally
            {
                // Release the memory.
                FreeMibTable(buffer);
            }
        }
    }
}
