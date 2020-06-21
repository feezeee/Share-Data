using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.MobileControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace interactDomain
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {

        public List<string> list = new List<string>();

        public class files
        {
            public string nameFile { get; set; }

            public string time { get; set; }

            public string sizeFile { get; set; }
        }

        public UserControl1()
        {

            setHeight = 1000;
            setWidth = 1000;
            InitializeComponent();
            setHeight = 1000;
            setWidth = 1000;
            //DataContext = this;
            //loadInfromationAboutFiles("1", "10", "100");

            //loadInfromationAboutFiles("2", "20", "200");

            //loadInfromationAboutFiles("3", "30", "200");
        }

        private int _width;

        public int setWidth
        { 
            get {   return _width;   }
            set {   _width = value;  }
        }

        private int _height;

        public int setHeight
        {
            get { return _height; }
            set { _height = value; }
        }

        public void loadInfromationAboutFiles(string nameFile, string time,string sizeFile)
        {
            files dataFile = new files() // создаём экземпляр класса        
            {
                nameFile = nameFile, // указываем имя файла  
                time = time, // указываем время создания    
                sizeFile = sizeFile // указываем пароль  
            };
            listUsers.Items.Add(dataFile); // выводим строку в список 
            
        }

    }
   
}
