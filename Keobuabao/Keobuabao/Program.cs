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