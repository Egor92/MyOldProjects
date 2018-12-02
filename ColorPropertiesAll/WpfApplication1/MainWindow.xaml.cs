using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var properties = typeof (Colors).GetProperties(BindingFlags.Static | BindingFlags.Public);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var property in properties)
            {
                var name = property.Name;
                var color = (Color)property.GetValue(null);
                var s = color.ToString();
                stringBuilder.AppendFormat("public static string {0} = \"{1}\";\r\n", name, s);
            }
            MyTextBox.Text = stringBuilder.ToString();
        }
    }
}
