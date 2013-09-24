using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLogger
{
    class DNSLogger
    {
        string location, extension, iface, arg;
        System.Diagnostics.Process proc;

        DNSLogger()
        {
            this.location = "Z:\\_dns\\";
            this.extension = ".pcap";
            this.iface = "Local";
            createArgument();
        }

        DNSLogger(string location, string iface = "Local", string extension = ".pcap")
        {
            this.location = location;
            this.extension = extension;
            this.iface = iface;
            createArgument();
        }

        void createArgument()
        {
            Random rand = new Random();
            string suffix = rand.Next(0, 1000).ToString();
            string timestamp = timestamp = DateTime.Now.ToString().
                Replace("/", "").Replace(":", "").Replace(" ", "");
            arg = "-i "+iface+" -f \"port 53\" -w \"" + location + timestamp + "_" + suffix + extension + "\"";
        }

        void createProc()
        {
            proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "dumpcap";
            proc.StartInfo.Arguments = arg;
            proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            proc.Start();
        }

        static void Main(string[] args)
        {
            DNSLogger logger = new DNSLogger();
            logger.createProc();
            // Console.WriteLine("[+] Logging DNS requests");
            while (true)
            {
                logger.proc.WaitForExit();
                logger.createProc();
            }
        }
    }
}