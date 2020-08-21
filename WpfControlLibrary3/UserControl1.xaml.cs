using InterfaceV2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfControlLibrary2;

namespace WpfControlLibrary3
{

    public class files
    {
        public string nameFile { get; set; }

        public string time { get; set; }

        public string sizeFile { get; set; }
    }

    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {       
        
        public UserControl1()
        {
            DataContext = this;
            IsHeightValue = 300;
            InitializeComponent();                       
        }

        private bool _status;

        public bool _Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private string _ip_from;

        public string Ip_From
        {
            get { return _ip_from; }
            set { _ip_from = value; }
        }

        private string _ip_to;

        public string Ip_To
        {
            get { return _ip_to; }
            set { _ip_to = value; }
        }

        private string _path_from;

        public string Path_From
        {
            get { return _path_from; }
            set { _path_from = value; }
        }


        private string _path_to;

        public string Path_To
        {
            get { return _path_to; }
            set { _path_to = value; }
        }


        private int cout = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">путь по которому надо создать папку</param>
        

        //Сортирует сообщение(файл дата размер)      

        public bool IsCheckBoxChecked_papka
        {
            get { return (bool)GetValue(IsCheckBoxChecked_papkaProperty); }
            set { SetValue(IsCheckBoxChecked_papkaProperty, value); }
        }
        // Using a DependencyProperty as the backing store for 
        //IsCheckBoxChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckBoxChecked_papkaProperty =
            DependencyProperty.Register("IsCheckBoxChecked_papka", typeof(bool),
            typeof(System.Windows.Controls.UserControl), new UIPropertyMetadata(false));


        public int IsHeightValue
        {
            get { return (int)GetValue(IsHeightValueProperty); }
            set { SetValue(IsHeightValueProperty, value); }
        }
        // Using a DependencyProperty as the backing store for 
        //IsCheckBoxChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsHeightValueProperty =
            DependencyProperty.Register("IsHeightValue", typeof(int),
            typeof(System.Windows.Controls.UserControl), new UIPropertyMetadata(0));

        public delegate void MethodContainer();

        //Событие OnCount c типом делегата  OncompleteList.
        public event MethodContainer OnCompleteTransmit;

        public void ChangedvalueForProgressBar()
        {
            System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
            {
                cout++;
                double how = cout;
                double all = IsHeightValue;
                double value = how / all * 100;
                status_progress.Value = value;
                status_label.Content = cout + "|" + IsHeightValue;
                if (cout == IsHeightValue)
                {
                    OnCompleteTransmit?.Invoke();
                }
            });
        }

        private void btn_checking_Click(object sender, RoutedEventArgs e)
        {
            //if(IsCheckBoxChecked_papka == false)
            //{
            //    IsCheckBoxChecked_papka = true;

            //    DoubleAnimation buttonAnimation = new DoubleAnimation();
            //    buttonAnimation.To = list_for_papki.Items.Count*26+50;
            //    buttonAnimation.Duration = TimeSpan.FromSeconds(0.3);
            //    grid_for_papki.BeginAnimation(Button.HeightProperty, buttonAnimation);
            //}
            //else
            //{
            //    IsCheckBoxChecked_papka = false;

            //    DoubleAnimation buttonAnimation = new DoubleAnimation();
            //    buttonAnimation.To = 0;
            //    buttonAnimation.Duration = TimeSpan.FromSeconds(0.3);
            //    grid_for_papki.BeginAnimation(Button.HeightProperty, buttonAnimation);
            //}    
        }

    }

}
