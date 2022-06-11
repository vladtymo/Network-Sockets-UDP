using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace np_sync_sockets
{
    class Program
    {
        static int port = 8080; // порт для приема входящих запросов
        static void Main(string[] args)
        {
            // получаем адреса для запуска сокета
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1"); //localhost
            IPEndPoint ipPoint = new IPEndPoint(iPAddress, port);

            // об'єкт для отримання адреси відправника
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

            // создаем сокет
            //Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // связываем сокет с локальной точкой, по которой будем принимать данные
            UdpClient server = new UdpClient(ipPoint);
                
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                //listenSocket.Bind(ipPoint);
                Console.WriteLine("Server started! Waiting for connection...");

                while (true)
                {
                    // получаем сообщение
                    //int bytes = 0;
                    //byte[] data = new byte[1024];
                    //bytes = listenSocket.ReceiveFrom(data, ref remoteEndPoint);
                    byte[] data = server.Receive(ref remoteEndPoint);

                    string msg = Encoding.Unicode.GetString(data, 0, data.Length);
                    Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: {msg} from {remoteEndPoint}");

                    // отправляем ответ
                    string message = "Message was send!";
                    data = Encoding.Unicode.GetBytes(message);
                    //listenSocket.SendTo(data, remoteEndPoint);
                    server.Send(data, data.Length, remoteEndPoint);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            server.Close();
        }
    }
}
