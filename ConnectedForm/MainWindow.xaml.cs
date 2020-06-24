using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using interactDomain;

namespace ConnectedForm
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;
            setWidth = (int)System.Windows.SystemParameters.PrimaryScreenWidth/6-15;
            InitializeComponent();
        }


        public class files
        {
            public string nameFile { get; set; }

            public string time { get; set; }

            public string sizeFile { get; set; }
        }

        private int _width;

        public int setWidth
        {
            get { return _width; }
            set { _width = value; }
        }

        private int _height;

        public int setHeight
        {
            get { return _height; }
            set { _height = value; }
        }

        public void loadInfromationAboutFiles0(string nameFile, string time, string sizeFile)
        {
            files dataFile = new files() // создаём экземпляр класса        
            {
                nameFile = nameFile, // указываем имя файла  
                time = time, // указываем время создания    
                sizeFile = sizeFile // указываем пароль  
            };
            listUsers0.Items.Add(dataFile); // выводим строку в список 
        }

        public void loadInfromationAboutFiles1(string nameFile, string time, string sizeFile)
        {
            files dataFile = new files() // создаём экземпляр класса        
            {
                nameFile = nameFile, // указываем имя файла  
                time = time, // указываем время создания    
                sizeFile = sizeFile // указываем пароль  
            };
            listUsers1.Items.Add(dataFile); // выводим строку в список 
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
