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

namespace TestWpfForm
{
    /// <summary>
    /// Логика взаимодействия для ListCntrl.xaml
    /// </summary>
    public partial class ListCntrl : UserControl
    {
        public ListCntrl()
        {
            DataContext = this;
            widthStackPanel = headerFirstColumn + headerSecondColumn + headerThirdColumn;
            InitializeComponent();
            Files files = new Files()
            {
                nameFile = "1",
                time = "yest",
                sizeFile = "sizeFile"
            };
            this.listUsers0.Items.Add((object)files);
            this.listUsers0.Items.Add((object)new Files()
            {
                nameFile = "1",
                sizeFile = "123",
                time = "2"
            });
            this.listUsers0.Items.Add((object)files);
            this.listUsers0.Items.Add((object)files);
            this.listUsers0.Items.Add((object)new Files()
            {
                nameFile = "1",
                sizeFile = "123",
                time = "2"
            });
            this.listUsers0.Items.Add((object)files);
            this.listUsers0.Items.Add((object)new Files()
            {
                nameFile = "1",
                sizeFile = "123",
                time = "2"
            });
        }

        private int widthStackPanel;
        private int headerFirstColumn = 120;
        private int headerSecondColumn = 120;
        private int headerThirdColumn = 120;
        private bool isDownLeftCtrlAndAlt;
        private bool isDownLeftButtonOnMouse;
        public int HeaderThirdColumnParam
        {
            get => this.headerThirdColumn;
            set => this.headerThirdColumn = value;
        }

        private void Key_Down(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.LeftCtrl || e.Key != Key.LeftAlt)
                return;
            this.isDownLeftCtrlAndAlt = true;
        }

        private void Key_Up(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.LeftCtrl || e.Key != Key.LeftAlt)
                return;
            this.isDownLeftCtrlAndAlt = false;
        }

        private void MouseLeftButtonDownEvent(object sender, MouseButtonEventArgs e) => this.isDownLeftButtonOnMouse = true;

        private void MouseLeftButtonUpEvent(object sender, MouseButtonEventArgs e) => this.isDownLeftButtonOnMouse = false;

        private void MouseMoveEvent(object sender, MouseEventArgs e)
        {
            if (!this.isDownLeftCtrlAndAlt)
                ;
        }

        public int WidthStacPanelParam
        {
            get => this.widthStackPanel;
            set => this.widthStackPanel = value;
        }

        public int HeaderFirctColumnParam
        {
            get => this.headerFirstColumn;
            set => this.headerFirstColumn = value;
        }

        public int HeaderSecondColumnParam
        {
            get => this.headerSecondColumn;
            set => this.headerSecondColumn = value;
        }

    }

    public class Files
    {
        public string nameFile { get; set; }
        public string time { get; set; }
        public string sizeFile { get; set; }

    }
}
