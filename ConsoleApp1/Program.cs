using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriellWriter
{
    class Program
    {
        public static SerialPort _port;
        static void Main(string[] args)
        {

            string[] ports = SerialPort.GetPortNames();
            foreach (var port in ports)
            {
                var myport = new SerialPort(port);
                _port = new SerialPort(port)
                {
                    BaudRate = 9600,
                    Parity = Parity.None,
                    StopBits = StopBits.One,
                    DataBits = 8,
                    Handshake = Handshake.None,
                    ReadTimeout = 500
                };
                _port.DataReceived += DataReceivedHandler;
                _port.Open();
                while (true)
                {
                     String s = Console.ReadLine();
                    if (s.Equals("exit"))
                    {
                        break;
                    }
                    _port.Write(s);
                }
                _port.Close();
                //_port.Write("0x09");
            }
            Console.Read();
            _port.Close();
        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            string input = _port.ReadExisting();
            var lines = new List<string>();
            lines.Add(input);
            System.IO.File.AppendAllLines(@"c:\jarl\response.txt", lines);
            Console.Write(input);
            //if (input.ToLower() == "n" || string.IsNullOrEmpty(input))
            //    return;
            ////Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => txtBoxCardNumer.Text += input));
            //if (_readtext.Length < 14)
            //    _readtext += input;
            //_datareceived = true;
        }
    }
}
