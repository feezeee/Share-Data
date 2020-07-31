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
using System.Xml;

namespace InteractingWindow
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            Label name = new Label();
            Label date = new Label();
            Label sizeFile = new Label();

            name.Content = "hgfd";
            date.Content = "12.12.2012";
            sizeFile.Content = "123";

            AddComponentInList(null,name,date,sizeFile);
        }

        public static void AddComponentInList(Image image, Label name, Label date, Label sizeFile)
        {
            Grid grid = new Grid();
            grid.ShowGridLines = true;

            ColumnDefinition column1 = new ColumnDefinition();
            column1.Width = new GridLength(30, GridUnitType.Auto);

            grid.ColumnDefinitions.Add(column1);
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            if(image!=null)
            grid.Children.Add(image);
            grid.Children.Add(name);
            grid.Children.Add(date);
            grid.Children.Add(sizeFile);

            if(image!=null)
            Grid.SetColumn(image, 0);
            Grid.SetColumn(name, 1);
            Grid.SetColumn(date, 2);
            Grid.SetColumn(date, 3);

        }
    }
}
