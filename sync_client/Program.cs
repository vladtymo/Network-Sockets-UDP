using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace sync_client
{
    class Program
    {
        // адрес и порт сервера, к которому будем подключаться
        static int port = 8080; // порт сервера
        static string address = "127.0.0.1"; // адрес сервера
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

                    //socket.SendTo(data, ipPoint);
                    client.Send(data, data.Length, ipPoint);

                    // при використанні UDP протоколу, Connect() лише встановлює дані для відправки
                    // socket.Connect(ipPoint);
                    // socket.Send(data);

                    // получаем ответ получаем сообщение
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
                // закрываем сокет
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
