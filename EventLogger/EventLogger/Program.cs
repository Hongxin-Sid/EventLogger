using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EventLogger
{
    class EventLogger
    {
        public readonly DateTime startTime;
        public bool isUDPOn;
        public bool isLogging = false;
        
        public string filePath
        {
            get
            {
                return _filePath;
            }

            set
            {
                _filePath = value;
            }
        }

        private string _filePath;

        public IPEndPoint IpEndPoint
        {
            get
            {
                return ipEndPoint;
            }

            set
            {
                ipEndPoint = value;
            }
        }

        private IPEndPoint ipEndPoint;

        public EventLogger()
        {
            startTime = DateTime.Now;
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

        public void Start()
        {
            start(false);
        }

        public void start(bool fileIsUsed)
        {
            if (ipEndPoint != null)
            {
                isUDPOn = true;
            }
            if (filePath != null)
            {
                isLogging = true;
                if (fileIsUsed != true)
                {
                    File.Create(filePath);
                }
            }
        }
        public string log(string errorInfo)
        {
            string errorMessage = log("", errorInfo);
            return errorMessage;
        }
        public string log(string errorSource,string errorInfo)
        {
            DateTime errorTime = DateTime.Now;
            string errorMessage = errorTime + "\t" + errorInfo;
            if (isUDPOn == true)
            {

            }
            if (isLogging == true)
            {
                File.AppendAllText(filePath, errorMessage);
            }
            return errorMessage;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
