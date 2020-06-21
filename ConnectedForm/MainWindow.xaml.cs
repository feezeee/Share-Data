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

namespace ConnectedForm
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Height00 = 100;
            Width00 = 100;
        }

        private int _height00;
        public int Height00
        {
            get { return _height00; }
            set { _height00 = value; }
        }

        private int _height01;
        public int Height01
        {
            get { return _height01; }
            set { _height01 = value; }
        }

        private int _height10;
        public int Height10
        {
            get { return _height10; }
            set { _height10 = value; }
        }

        private int _height11;
        public int Height11
        {
            get { return _height11; }
            set { _height11 = value; }
        }

        /////////////////////Ширина
        ///
        private int _width00;
        public int Width00
        {
            get { return _width00; }
            set { _width00 = value; }
        }///
        private int _width01;
        public int Width01
        {
            get { return _width01; }
            set { _width01 = value; }
        }
        ///
        private int _width10;
        public int Width10
        {
            get { return _width10; }
            set { _width10 = value; }
        }
        ///
        private int _width11;
        public int Width11
        {
            get { return _width11; }
            set { _width11 = value; }
        }
    }
}
