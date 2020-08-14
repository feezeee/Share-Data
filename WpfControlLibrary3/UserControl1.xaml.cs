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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfControlLibrary3
{
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
            typeof(System.Windows.Controls.UserControl), new UIPropertyMetadata(250));



        private void btn_checking_Click(object sender, RoutedEventArgs e)
        {
            if(IsCheckBoxChecked_papka == false)
            {
                IsCheckBoxChecked_papka = true;

                DoubleAnimation buttonAnimation = new DoubleAnimation();
                buttonAnimation.To = list_for_papki.Items.Count*20;
                buttonAnimation.Duration = TimeSpan.FromSeconds(0.3);
                grid_for_papki.BeginAnimation(Button.HeightProperty, buttonAnimation);
            }
            else
            {
                IsCheckBoxChecked_papka = false;

                DoubleAnimation buttonAnimation = new DoubleAnimation();
                buttonAnimation.To = 0;
                buttonAnimation.Duration = TimeSpan.FromSeconds(0.3);
                grid_for_papki.BeginAnimation(Button.HeightProperty, buttonAnimation);
            }    
        }
    }

}
