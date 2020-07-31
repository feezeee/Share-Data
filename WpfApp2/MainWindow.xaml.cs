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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void AddComponentInList(Image image, Label name, Label date, Label sizeFile)
        {
            DataContext = this;
            Grid grid = new Grid();
            //grid.ShowGridLines = true;           

            ColumnDefinition column1 = new ColumnDefinition();
            column1.Width = new GridLength(30);

            ColumnDefinition column2 = new ColumnDefinition();
            column2.Width = new GridLength();

            ColumnDefinition column3 = new ColumnDefinition();
            column3.Width = new GridLength();

            ColumnDefinition column4 = new ColumnDefinition();
            column4.Width = new GridLength();

            grid.ColumnDefinitions.Add(column1);
            grid.ColumnDefinitions.Add(column2);
            grid.ColumnDefinitions.Add(column3);
            grid.ColumnDefinitions.Add(column4);


            if (image != null)
                grid.Children.Add(image);
            grid.Children.Add(name);
            grid.Children.Add(date);
            grid.Children.Add(sizeFile);

            if (image != null)
                Grid.SetColumn(image, 0);
            Grid.SetColumn(name, 1);
            Grid.SetColumn(date, 2);
            Grid.SetColumn(sizeFile, 3);

            lstbox.Items.Add(grid);

            grid.Style = (Style)FindResource("MyGridStyle");

        }

        private void gonext0_Click(object sender, RoutedEventArgs e)
        {
            Label name = new Label();
            Label date = new Label();
            Label sizeFile = new Label();

            name.Content = "hgfd";
            date.Content = "12.12.2012";
            sizeFile.Content = "123";

            AddComponentInList(null, name, date, sizeFile);
        }
    }
}
