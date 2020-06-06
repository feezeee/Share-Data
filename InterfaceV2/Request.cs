using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace InterfaceV2
{
    public enum RequestTipe
    {
        Send = 1,
        GetDirectoryFiles
    }
    public class Request
    {
        public static void DoAfterSend(RequestTipe request)
        {

        }
        public static string ExecuteRecuest(string requestMessage)
        {
            string ans = "";

            string[] mes = requestMessage.Split(' ');
            int request = int.Parse(mes[2]);
            switch (request)
            {
                case (int)RequestTipe.GetDirectoryFiles:
                    break;
                case (int)RequestTipe.Send:
                    break;

                default:
                    Console.WriteLine("request isn't distingushed");
                    break;
            }
            return ans;
        }
    }
}
