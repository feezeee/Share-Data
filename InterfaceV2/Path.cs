using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceV2
{
    public class Path
    {
        private List<string> History;
        public static string StartPath = ".";

        private string path = ".";

        public string GoToTheDirectory(string direcotryName, string directoryLocaltion = null)
        {
            if (directoryLocaltion == null) directoryLocaltion = path;

            if (directoryLocaltion == ".")
            {
                directoryLocaltion = "";
            }
            directoryLocaltion += '\\' + direcotryName;
            History.Add(directoryLocaltion);
            return directoryLocaltion;
        }
    }
}
