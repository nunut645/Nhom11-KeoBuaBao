using System;
using System.Net.Sockets;
using System.Text;

class GameClient
{
    static void Main()
    {
        TcpClient client = new TcpClient("127.0.0.1", 5000);
        NetworkStream ns = client.GetStream();

        Console.Write("Nhap ten nguoi choi: ");
        string name = Console.ReadLine();

        Console.Write("Chon (keo/bua/bao): ");
        string choice = Console.ReadLine();

        string message = name + ":" + choice;
        byte[] data = Encoding.UTF8.GetBytes(message);
        ns.Write(data, 0, data.Length);

        // Nhận kết quả từ server
        byte[] buffer = new byte[1024];
        int bytesRead = ns.Read(buffer, 0, buffer.Length);
        string result = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        Console.WriteLine("Ket qua: " + result);

        client.Close();
    }
}
