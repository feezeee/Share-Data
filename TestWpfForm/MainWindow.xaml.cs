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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TestWpfForm
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Files files = new Files()
            {
                nameFile = "1",
                time = "yest",
                sizeFile = "sizeFile"
            };
            List<object> list = new List<object>();
            list.Add(files);

            list.Add(files);

            list.Add(files);

            list.Add(files);

            list.Add(files);

            list.Add(files);

            list.Add(files);

            list.Add(files);
            adding(list);
        }

        private async void adding(List<object> someobjlist)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < someobjlist.Count; i++)
                {
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                        () => { sd0.AddingNewItemInList(someobjlist[i]); }));
                    Thread.Sleep(10000);
                }
            });

        }
    }

}
