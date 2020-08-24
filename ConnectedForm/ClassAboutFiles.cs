using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectedForm
{

    public class files
    {
        public string nameFile { get; set; }

        public string time { get; set; }

        public string sizeFile { get; set; }
    }
    public class ClassAboutFilesRenamed
    {

    }

    public class ClassAboutFilesAdding : files
    {
        private string _RootLocationFilesOrDirectory;

        public string RootLocationFilesOrDirectory
        {
            get 
            { 
                return _RootLocationFilesOrDirectory; 
            }
            set 
            {
                _RootLocationFilesOrDirectory = value; 
            }           
        }


        private string _RemoteLocationFilesOrDirectory;

        public string RemoteLocationFilesOrDirectory
        {
            get 
            { 
                return _RemoteLocationFilesOrDirectory; 
            }
            set 
            { 
                _RemoteLocationFilesOrDirectory = value; 
            }
        }

        private string _LocalParentPath;
        public string LocalParentPath
        {
            get
            {
                return _LocalParentPath;
            }
            set
            {
                _LocalParentPath = value;
            }
        }

        private string _RemoteParentPath;
        public string RemoteParentPath
        {
            get
            {
                return _RemoteParentPath;
            }
            set
            {
                _RemoteParentPath = value;
            }
        }


        private string _LocalParentName;
        public string LocalParentName
        {
            get
            {
                return _LocalParentName;
            }
            set
            {
                _LocalParentName = value;
            }
        }

        private string _RemoteParentName;
        public string RemoteParentName
        {
            get
            {
                return _RemoteParentName;
            }
            set
            {
                _RemoteParentName = value;
            }
        }



        private string _sender;
        private string _receiver;

        public string Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }

        public string Receiver
        {
            get { return _receiver; }
            set { _receiver = value; }
        }

        public ClassAboutFilesAdding(): this(null,null,null)
        {
            
        }
        public ClassAboutFilesAdding(string Name, string Time, string Size)
        {
            this.nameFile = Name;
            this.time = Time;
            this.sizeFile = Size;
        }

        public ClassAboutFilesAdding(string Name, string Time, string Size, string RootLocationFilesOrDirectory, string RemoteLocationFilesOrDirectory, string SenderIp, string Receiverip)
        {
            this.nameFile = Name;
            this.time = Time;
            this.sizeFile = Size;
            this.RootLocationFilesOrDirectory = RootLocationFilesOrDirectory;
            this.RemoteLocationFilesOrDirectory = RemoteLocationFilesOrDirectory;
            this.Sender = SenderIp;
            this.Receiver = Receiverip;
        }
        public ClassAboutFilesAdding(string Name, string Time, string Size, string RootLocationFilesOrDirectory, string RemoteLocationFilesOrDirectory, string SenderIp, string Receiverip, string LocalParentPath)
        {
            this.nameFile = Name;
            this.time = Time;
            this.sizeFile = Size;
            this.RootLocationFilesOrDirectory = RootLocationFilesOrDirectory;
            this.RemoteLocationFilesOrDirectory = RemoteLocationFilesOrDirectory;
            this.Sender = SenderIp;
            this.Receiver = Receiverip;
            this.LocalParentPath = LocalParentPath;
        }

        public ClassAboutFilesAdding(string Name, string Time, string Size, string RootLocationFilesOrDirectory, string RemoteLocationFilesOrDirectory, string SenderIp, string Receiverip, string LocalParentPath, string RemoteParentPath, string LocalParentName, string RemoteParentName)
        {
            this.nameFile = Name;
            this.time = Time;
            this.sizeFile = Size;
            this.RootLocationFilesOrDirectory = RootLocationFilesOrDirectory;
            this.RemoteLocationFilesOrDirectory = RemoteLocationFilesOrDirectory;
            this.Sender = SenderIp;
            this.Receiver = Receiverip;
            this.LocalParentPath = LocalParentPath;
            this.RemoteParentPath = RemoteParentPath;
            this.LocalParentName = LocalParentName;
            this.RemoteParentName = RemoteParentName;
        }
    }
}
