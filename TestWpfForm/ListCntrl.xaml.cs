using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Media.Media3D;
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
            _headerFirstColumn = headerFirstColumn;
            _headerSecondColumn = headerSecondColumn;
            _headerThirdColumn = headerThirdColumn;
            heightStackPanel = 300;
            InitializeComponent();
            Files files = new Files()
            {
                nameFile = "1",
                time = "yest",
                sizeFile = "sizeFile"
            };
            AddingNewItemInList(files);
            AddingNewItemInList(files);
            AddingNewItemInList(files);
            AddingNewItemInList(files);
        }

        public void AddingNewItemInList(object item)
        {
            listUsers0.Items.Add(item);
        }

        private int widthStackPanel;
        private int heightStackPanel;


        private int _headerFirstColumn;
        private int _headerSecondColumn;
        private int _headerThirdColumn;

        private int headerFirstColumn = 120;
        private int headerSecondColumn = 120;
        private int headerThirdColumn = 120;


        #region Удаление, выделение в ListView

        /// <summary>
        /// По умолчанию LeftCtrl & LeftAlt
        /// </summary>
        #region сочетание клавиш для выделения Items в ListView

        private bool firstKeyForSelected = false;
        private bool secondKeyForSelected = false;

        private Key firstKeyForSelected_Key = Key.LeftCtrl;
        private Key secondKeyForSelected_Key = Key.LeftAlt;

        #region Методы доступа к свойствам

        public bool GetfirstKeyForSelected
        {
            get { return firstKeyForSelected; }
            set { firstKeyForSelected = value; }
        }

        public bool GetsecondKeyForSelected
        {
            get { return secondKeyForSelected; }
            set { secondKeyForSelected = value; }
        }

        public Key GetfirstKeyForSelected_Key
        {
            get { return secondKeyForSelected_Key; }
            set { secondKeyForSelected_Key = value; }
        }

        public Key GetsecondKeyForSelected_Key
        {
            get { return secondKeyForSelected_Key; }
            set { secondKeyForSelected_Key = value; }
        }

        #endregion

        #endregion

        /// <summary>
        /// По умолчанию RightCtrl & RightAlt
        /// </summary>
        #region сочетание клавиш для удаления выделения Items в ListView

        private bool firstKeyForUnSelected = false;
        private bool secondKeyForUnSelected = false;

        private Key firstKeyForUnSelected_Key = Key.RightAlt;
        private Key secondKeyForUnSelected_Key = Key.RightCtrl;

        #region Методы доступа к свойствам

        public bool GetfirstKeyForUnSelected
        {
            get { return firstKeyForUnSelected; }
            set { firstKeyForUnSelected = value; }
        }

        public bool GetsecondKeyForUnSelected
        {
            get { return secondKeyForUnSelected; }
            set { secondKeyForUnSelected = value; }
        }

        public Key GetfirstKeyForUnSelected_Key
        {
            get { return firstKeyForUnSelected_Key; }
            set { firstKeyForUnSelected_Key = value; }
        }

        public Key GetsecondKeyForUnSelected_Key
        {
            get { return secondKeyForUnSelected_Key; }
            set { secondKeyForUnSelected_Key = value; }
        }

        #endregion

#endregion

        #endregion

        private void Key_Down(object sender, KeyEventArgs e)
        {

            if (e.SystemKey == firstKeyForSelected_Key || e.Key == firstKeyForSelected_Key)
            {
                this.firstKeyForSelected = true;
            }
            else if (e.SystemKey == secondKeyForSelected_Key || e.Key == secondKeyForSelected_Key)
            {
                secondKeyForSelected = true;
            }
            else if (e.SystemKey == firstKeyForUnSelected_Key || e.Key == firstKeyForUnSelected_Key)
            {
                this.firstKeyForUnSelected = true;
            }
            else if (e.SystemKey == secondKeyForUnSelected_Key || e.Key == secondKeyForUnSelected_Key)
            {
                secondKeyForUnSelected = true;
            }
        }

        private void Key_Up(object sender, KeyEventArgs e)
        {
            if (e.SystemKey == firstKeyForSelected_Key || e.Key == firstKeyForSelected_Key)
            {
                this.firstKeyForSelected = false;
            }
            else if (e.SystemKey == secondKeyForSelected_Key || e.Key == secondKeyForSelected_Key)
            {
                this.secondKeyForSelected = false;
            }
            else if (e.SystemKey == firstKeyForUnSelected_Key || e.Key == firstKeyForUnSelected_Key)
            {
                this.firstKeyForUnSelected = false;
            }
            else if (e.SystemKey == secondKeyForUnSelected_Key || e.Key == secondKeyForUnSelected_Key)
            {
                secondKeyForUnSelected = false;
            }
        }

        private void MouseEnterOnItem(object sender, MouseEventArgs e)
        {

            if (Keyboard.IsKeyDown(firstKeyForSelected_Key) && Keyboard.IsKeyDown(secondKeyForSelected_Key) && !Keyboard.IsKeyDown(firstKeyForUnSelected_Key) && !Keyboard.IsKeyDown(secondKeyForUnSelected_Key)) 
            {
                var item = sender as ListViewItem;
                if (item != null && !item.IsSelected)
                {
                    item.IsSelected = true;
                    item.UpdateLayout();
                }
                //Console.WriteLine(true);
            }

            if (Keyboard.IsKeyDown(firstKeyForUnSelected_Key) && Keyboard.IsKeyDown(secondKeyForUnSelected_Key) && !Keyboard.IsKeyDown(firstKeyForSelected_Key) && !Keyboard.IsKeyDown(secondKeyForSelected_Key))
            {
                var item = sender as ListViewItem;
                if (item != null && item.IsSelected)
                {
                    item.IsSelected = false;
                    item.UpdateLayout();
                }
            }
        }

        private void MouseMoveEvent(object sender, MouseEventArgs e)
        {

        }

        public int WidthStackPanelParam
        {
            get => this.widthStackPanel;
            set => this.widthStackPanel = value;
        }
        public int HeightStackPanelParam
        {
            get => this.heightStackPanel;
            set => this.heightStackPanel = value;
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

        public int HeaderThirdColumnParam
        {
            get => this.headerThirdColumn;
            set => this.headerThirdColumn = value;
        }

        private void PrewLostKeyboardFocusEv(object sender, RoutedEventArgs e)
        {

        }

        private void ListUsers0_OnMouseEnter(object sender, MouseEventArgs e)
        {
            var listt = sender as ListView;
            listt.Focus();
        }

        public delegate void MethodContainer(object infAboutFile);
        /// <summary>
        /// Событие, вызываемое при двойном клике по item
        /// </summary>
        public event MethodContainer OnDoubleClickInElement;
        private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e)
        {
            ListViewItem list = (ListViewItem)sender;
            //throw new NotImplementedException();
            var infAboutFile = list.Content as Files;
            OnDoubleClickInElement?.Invoke(infAboutFile);
        }

        public bool ClearListWithItems()
        {
            try
            {
                listUsers0.Items.Clear();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public int GettingPauseTime()
        {
            if (_headerFirstColumn != headerFirstColumn || _headerSecondColumn != headerSecondColumn ||
                _headerThirdColumn != headerThirdColumn)
            {
                _headerFirstColumn = headerFirstColumn;
                _headerSecondColumn = headerSecondColumn;
                _headerThirdColumn = headerThirdColumn;
                return 600;
            }
            else return 0;

        }
    }

    public class Files
    {
        public string nameFile { get; set; }
        public string time { get; set; }
        public string sizeFile { get; set; }

    }
}
