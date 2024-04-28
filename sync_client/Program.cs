using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace sync_client
{
    class Program
    {
        static string address = "127.0.0.1"; // адреса сервера
        static int port = 8080;              // порт сервера

        static void Main(string[] args)
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
                
                IPEndPoint remoteIpPoint = new IPEndPoint(IPAddress.Any, 0);

                // IP4 samples: 123.5.6.3    0.0.255.255    10.7.123.184
                //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                UdpClient client = new UdpClient();

                string message = "";
                while (message != "end")
                {
                    Console.Write("Enter a message: ");
                    message = Console.ReadLine();
                    byte[] data = Encoding.Unicode.GetBytes(message);

                    // відпрявляємо запит на сервер
                    //socket.SendTo(data, ipPoint);
                    client.Send(data, data.Length, ipPoint);

                    // при використанні UDP протоколу, Connect() лише встановлює дані для відправки
                    // socket.Connect(ipPoint);
                    // socket.Send(data);

                    // отримуємо відповідь
                    //int bytes = 0;
                    //string response = "";
                    //data = new byte[1024]; // 1KB
                    //do
                    //{
                    //    bytes = socket.ReceiveFrom(data, ref remoteIpPoint);
                    //    response += Encoding.Unicode.GetString(data, 0, bytes);
                    //} while (socket.Available > 0);
                    data = client.Receive(ref remoteIpPoint);
                    string response = Encoding.Unicode.GetString(data);

                    Console.WriteLine("server response: " + response);
                }
                // закриваємо сокет
                //socket.Shutdown(SocketShutdown.Both);
                //socket.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
