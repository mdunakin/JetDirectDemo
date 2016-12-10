using System;
using System.Threading;
using System.Net.Sockets;
using System.Configuration;



namespace JetDirectTest
{
    class Program
    {
        static void Main()
        {
            string[] hexValues = new string[] { "0x01", "0x02", "0x04", "0x08", "0x10", "0x20", "0x40", "0x80" };
            while (true)
            {
                for (int i = 0; i < 7; i++)
                {
                    SetPortValue(hexValues[i]);
                    Thread.Sleep(100);
                }
                for (int i = 7; i > 0; i--)
                {
                    SetPortValue(hexValues[i]);
                    Thread.Sleep(100);
                }
            }
        }

        public static void SetPortValue(string hexValue)
        {
            hexValue = hexValue.Replace("0x", "");
            var intValue = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
            var ip = ConfigurationManager.AppSettings.Get("ip"); //get ip adress from app.config
            var client = new TcpClient(ip, 9100);
            using (var stream = client.GetStream())
            {
                Byte[] data = { Convert.ToByte(intValue) };
                stream.Write(data, 0, data.Length);
            }
        }
    }
}
