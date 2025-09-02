using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class GameServer
{
    static void Main()
    {
        TcpListener server = new TcpListener(IPAddress.Any, 8888);
        server.Start();
        Console.WriteLine("🎮 Tro choi dang chuan bi...");

        // Chờ client 1
        Console.WriteLine("⏳ Dang cho nguoi choi 1...");
        TcpClient client1 = server.AcceptTcpClient();
        NetworkStream ns1 = client1.GetStream();
        Console.WriteLine("✅ Nguoi choi 1 da vao phong");

        // Chờ client 2
        Console.WriteLine("⏳ Dang cho nguoi choi 2...");
        TcpClient client2 = server.AcceptTcpClient();
        NetworkStream ns2 = client2.GetStream();
        Console.WriteLine("✅ Nguoi choi 2 da vao phong");

        // Nhận dữ liệu từ client 1
        byte[] buffer = new byte[1024];
        int bytesRead1 = ns1.Read(buffer, 0, buffer.Length);
        string player1 = Encoding.UTF8.GetString(buffer, 0, bytesRead1).Trim();

        // Nhận dữ liệu từ client 2
        int bytesRead2 = ns2.Read(buffer, 0, buffer.Length);
        string player2 = Encoding.UTF8.GetString(buffer, 0, bytesRead2).Trim();

        Console.WriteLine($"👉 Nguoi choi 1: {player1}");
        Console.WriteLine($"👉 Nguoi choi 2: {player2}");

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
        try
        {
            string[] parts1 = p1.Split(':');
            string[] parts2 = p2.Split(':');

            if (parts1.Length < 2 || parts2.Length < 2)
                return "❌ Loi du lieu gui len!";

            string name1 = parts1[0].Trim();
            string choice1 = parts1[1].Trim().ToLower();

            string name2 = parts2[0].Trim();
            string choice2 = parts2[1].Trim().ToLower();

            // Nếu lựa chọn không hợp lệ
            string[] validChoices = { "keo", "bua", "bao" };
            if (Array.IndexOf(validChoices, choice1) == -1 ||
                Array.IndexOf(validChoices, choice2) == -1)
                return "❌ Lua chon khong hop le! (chi duoc: keo, bua, bao)";

            // Trả về kết quả
            if (choice1 == choice2)
                return $"⚖ Hoa! {name1} va {name2} deu chon {choice1}";

            if ((choice1 == "keo" && choice2 == "bao") ||
                (choice1 == "bua" && choice2 == "keo") ||
                (choice1 == "bao" && choice2 == "bua"))
                return $"🎉 {name1} (chon {choice1}) thang {name2} (chon {choice2})";

            return $"🎉 {name2} (chon {choice2}) thang {name1} (chon {choice1})";
        }
        catch (Exception ex)
        {
            return $"❌ Loi xu ly: {ex.Message}";
        }
    }
}
