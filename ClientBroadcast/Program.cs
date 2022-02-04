using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientBroadcast
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(IPAddress.Broadcast);
            var udpListener = new UdpClient(45679);
            var udpListenerIp = IPAddress.Parse("255.255.255.255");

            var udpListenerEp = new IPEndPoint(udpListenerIp, 0);

            Task.Run(() =>
            {
                while (true)
                {
                    var bytes = udpListener.Receive(ref udpListenerEp);
                    var str = Encoding.Default.GetString(bytes);
                    Console.WriteLine(str);
                }
            });



            var udpClient = new UdpClient();
            var ip = IPAddress.Parse("255.255.255.255");
            var ep = new IPEndPoint(ip, 45678);

            while (true)
            {
                var str = Console.ReadLine();
                var bytes = Encoding.Default.GetBytes(str);
                udpClient.Send(bytes, bytes.Length, ep);
            }

        }
    }
}
