using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace IPmanip
{
    public class IP
    {
        public static string GetCurrentHostnName()
        {
            String strHostName = "";
            strHostName = Dns.GetHostName();
            return strHostName;
        }

        public static List<IPAddress> GetIPAddresses(string  strHostName)
        {
            var addresses = new List<IPAddress>();

            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;

            foreach(var IPaddress in addr)
            {
                addresses.Add(IPaddress);
            }

            return addresses;
        }

        public static List<string> GetCurrentIP()
        {
            var strAddresses = new List<string>();

            var IPaddresses = GetIPAddresses(GetCurrentHostnName());
            foreach(var addres in IPaddresses)
            {
                strAddresses.Add(addres.ToString());
            }

            return strAddresses;
        }

        public static string GetLocalIPAddress()
        {
            var host = GetIPAddresses(GetCurrentHostnName());
            foreach (var ip in host)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
