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
                nameFile = "2",
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

        private void Sd0_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void adding(List<object> someobjlist)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 999; i++)
                {
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                        () => { sd0.AddingNewItemInList(someobjlist[0]); }));
                    Thread.Sleep(0);
                }
            });
            //await Task.Run(() =>
            //{
                
            //        Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
            //            () => {
            //                sd0.ClearListWithItems();
            //            }));
                 
            //});
        }
    }

}
