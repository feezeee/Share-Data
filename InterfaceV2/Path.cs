using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceV2
{
    public class Path
    {
        public string CurrentPath
        {
            get
            {
                if (_path.Count == 0) return Path.ZerroPath;
                string path = "";
                for(int i = 0; i < _path.Count; i++)
                {
                    path += _path[i];
                    if(i < _path.Count - 1) path += '\\';
                }
                return path;
            }
        }
        private List<string> _path = new List<string>();

        public void ResetPath(string path)
        {
            _path = new List<string>(path.Split('\\'));
        }
        public static string ZerroPath = ".";
        public void GoToThe(string directory)
        {
            _path.Add(directory);
        }
        public void GoBack()
        {
            if(_path.Count == 0)
            {
                return;
            }

            _path.RemoveAt(_path.Count - 1);
        }
    }
}
