using ConsoleTst.Infrastructure;

namespace Broker
{
    public class Program
    {
        static SocketServer server = new SocketServer(8005);

        static void Main(string[] args)
        {
            server.Start();
            //SocketAsync.StartListening();
            
        }
    }
}
