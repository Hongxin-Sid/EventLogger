using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EventLogger
{
    class EventLogger
    {
        private bool isUdpEnabled = false;
        private bool isFileEnabled = false;
        
        public string FilePath
        {
            get
            {
                return filePath;
            }

            set
            {
                filePath = value;
            }
        }

        private string filePath;

        public IPEndPoint IPEndPoint
        {
            get
            {
                return ipEndPoint;
            }

            private set
            {
                ipEndPoint = value;
            }
        }

        private IPEndPoint ipEndPoint;

        private UdpClient udpClient;

        public EventLogger()
        {
            Console.WriteLine("New EventLogger ……");
        }

        public EventLogger(string filePath):this()
        {
            this.filePath = filePath;
        }
        
        public EventLogger(IPEndPoint ipEndPoint):this()
        {
            this.ipEndPoint = ipEndPoint;
        }

        public EventLogger(string filePath,IPEndPoint ipEndPoint):this()
        {
            this.filePath = filePath;
            this.ipEndPoint = ipEndPoint;
        }

        public void start()
        {
            if (ipEndPoint != null)
            {
                isUdpEnabled = true;
                udpClient = new UdpClient(ipEndPoint);
            }
            if (filePath != null)
            {
                isFileEnabled = true;
                //File.Create(filePath);
            }
            Console.WriteLine("Start Logging ……");
        }

        public string log(string errorInfo)
        {
            return log("", errorInfo);
        }

        public string log(string errorSource,string errorInfo)
        {
            string errorMessage;
            if (errorSource != "")
            {
                errorMessage = DateTime.Now + "\t" + errorInfo + "( from " + errorSource + " )";
            }
            else
            {
                errorMessage = DateTime.Now + "\t" + errorInfo;
            }
            if (isUdpEnabled == true)
            {
                byte[] sendbytes = Encoding.Unicode.GetBytes(errorMessage);
                udpClient.Send(sendbytes, sendbytes.Length);
            }
            if (isFileEnabled == true)
            {
                File.AppendAllText(filePath, errorMessage);
            }
            Console.WriteLine(errorMessage);
            return errorMessage;
        }
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "EventList.txt";
            string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

            EventLogger EventLogger = new EventLogger(filePath);
            EventLogger.start();
            EventLogger.log("localhost", "Just a Test");
            Console.ReadLine();
        }
    }
}
