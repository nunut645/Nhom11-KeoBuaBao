using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client1
{
    public partial class Form1 : Form
    {
        TcpClient client;
        NetworkStream stream;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient("127.0.0.1", 8888);
                stream = client.GetStream();
                txtLog.AppendText("✅ Đã kết nối server!\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối server: " + ex.Message);
            }
        }

        private void btnKeo_Click(object sender, EventArgs e)
        {
            SendChoice("keo");
        }

        private void btnBua_Click(object sender, EventArgs e)
        {
            SendChoice("bua");
        }

        private void btnBao_Click(object sender, EventArgs e)
        {
            SendChoice("bao");
        }

        private void SendChoice(string choice)
        {
            if (stream == null)
            {
                MessageBox.Show("⚠️ Bạn chưa kết nối tới server!");
                return;
            }

            string name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("⚠️ Vui lòng nhập tên!");
                return;
            }

            try
            {
                // Gửi dữ liệu lên server
                string message = $"{name}:{choice}";
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);

                txtLog.AppendText($"👉 Bạn chọn: {choice}\r\n");

                // Nhận kết quả từ server
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                if (bytesRead > 0)
                {
                    string result = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    txtLog.AppendText($"🏆 Kết quả: {result}\r\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi/nhận dữ liệu: " + ex.Message);
            }
        }
    }
}
