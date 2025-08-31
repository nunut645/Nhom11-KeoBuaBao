using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class GameServer
{
    static void Main()
    {
        TcpListener server = new TcpListener(IPAddress.Any, 5000);
        server.Start();
        Console.WriteLine("Tro choi dang chuan bi...");

        // Chờ client 1
        Console.WriteLine("Dang cho nguoi choi 1...");
        TcpClient client1 = server.AcceptTcpClient();
        NetworkStream ns1 = client1.GetStream();
        Console.WriteLine("Nguoi choi 1 da vao phong");

        // Chờ client 2
        Console.WriteLine("Dang cho nguoi choi 2...");
        TcpClient client2 = server.AcceptTcpClient();
        NetworkStream ns2 = client2.GetStream();
        Console.WriteLine("Nguoi choi 2 da vao phong");

        // Nhận dữ liệu từ client 1
        byte[] buffer = new byte[1024];
        int bytesRead1 = ns1.Read(buffer, 0, buffer.Length);
        string player1 = Encoding.UTF8.GetString(buffer, 0, bytesRead1);

        // Nhận dữ liệu từ client 2
        int bytesRead2 = ns2.Read(buffer, 0, buffer.Length);
        string player2 = Encoding.UTF8.GetString(buffer, 0, bytesRead2);

        Console.WriteLine($"Nguoi choi 1: {player1}");
        Console.WriteLine($"Nguoi choi 2: {player2}");

        // Xử lý kết quả
        string result = GetResult(player1, player2);

        // Gửi kết quả cho cả 2
        byte[] resBytes = Encoding.UTF8.GetBytes(result);
        ns1.Write(resBytes, 0, resBytes.Length);
        ns2.Write(resBytes, 0, resBytes.Length);

        client1.Close();
        client2.Close();
        server.Stop();
    }

    static string GetResult(string p1, string p2)
    {
        string[] parts1 = p1.Split(':');
        string[] parts2 = p2.Split(':');
        string name1 = parts1[0], choice1 = parts1[1];
        string name2 = parts2[0], choice2 = parts2[1];

        if (choice1 == choice2)
            return "Hoa!";
        else if ((choice1 == "keo" && choice2 == "bao") ||
                 (choice1 == "bua" && choice2 == "keo") ||
                 (choice1 == "bao" && choice2 == "bua"))
            return $"{name1} win!";
        else
            return $"{name2} win!";
    }
}