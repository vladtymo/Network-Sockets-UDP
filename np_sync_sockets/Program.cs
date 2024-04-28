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
        static string address = "127.0.0.1"; // поточний адрес
        static int port = 8080;              // порт для прийому вхідних запитів

        static void Main(string[] args)
        {
            // отримуємо адресу для запуску сервера
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            // об'єкт для отримання адреси відправника
            IPEndPoint remoteEndPoint = null;

            // створюємо сокет
            //Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // зв'язуємо сокет з локальною точкою, по які будемо приймати дані
            UdpClient listener = new UdpClient(ipPoint);
            //listenSocket.Bind(ipPoint);

            try
            {
                Console.WriteLine("Server started! Waiting for connection...");

                while (true)
                {
                    // отримуємо повідомлення
                    //int bytes = 0;
                    //byte[] data = new byte[1024];
                    //bytes = listenSocket.ReceiveFrom(data, ref remoteEndPoint);
                    byte[] data = listener.Receive(ref remoteEndPoint);

                    string msg = Encoding.Unicode.GetString(data);
                    Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: {msg} from {remoteEndPoint}");

                    // відправляємо відповідь
                    string message = "Message was send!";
                    data = Encoding.Unicode.GetBytes(message);
                    //listenSocket.SendTo(data, remoteEndPoint);
                    listener.Send(data, data.Length, remoteEndPoint);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //listenSocket.Shutdown(SocketShutdown.Both);
            //listenSocket.Close();
            listener.Close();
        }
    }
}
