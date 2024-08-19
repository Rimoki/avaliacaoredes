using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main()
    {
        try
        {
            string server = "127.0.0.1";
            int port = 13000;

            TcpClient client = new TcpClient(server, port);
            NetworkStream stream = client.GetStream();
            
            // receber menu e imprimir
            void PrintMenu()
            {
                byte[] menuData = new byte[256];
                int bytesRead = stream.Read(menuData, 0, menuData.Length);
                string menu = Encoding.UTF8.GetString(menuData, 0, bytesRead);
                Console.WriteLine(menu);
            }
            
            PrintMenu();

            while (true)
            {
                string message = Console.ReadLine();
                byte[] data = Encoding.UTF8.GetBytes(message);

                stream.Write(data, 0, data.Length);
                Console.Clear();
                Console.WriteLine("Enviado: {0}", message);

                data = new byte[256];
                int bytesRead = stream.Read(data, 0, data.Length);
                string responseData = Encoding.UTF8.GetString(data, 0, bytesRead);
                Console.WriteLine("Recebido: {0}", responseData);
                if (message == "0")
                {
                    break;
                }
            }
            stream.Close();
            client.Close();
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("ArgumentNullException: {0}", e);
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }

        Console.WriteLine("\nPressione Enter para continuar...");
        Console.Read();
    }
}