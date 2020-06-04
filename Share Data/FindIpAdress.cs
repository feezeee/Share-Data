using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Interface;

namespace Share_Data
{
    class FindIpAdress
    {
        public List<string> FindAdress() //полусение доступных пользователей в сет
        {
            List<string> ipAdresses = new List<string>();
            ipAdresses = AvailableConection.ReturnGroupList();
            return ipAdresses;
        }
    }
}
