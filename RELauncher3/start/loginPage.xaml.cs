using RELauncher3.Properties;
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

namespace RELauncher3.start
{
    /// <summary>
    /// loginPage.xaml 的交互逻辑
    /// </summary>
    public partial class loginPage : Grid
    {
        public loginPage()
        {
            InitializeComponent();
            usrtxt.Text = Settings.Default["UserName"].ToString();
        }

        private void noAccount_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default["OnlineAccount"] = false;
            Settings.Default.Save();
            Grid grid;
            grid = new setColor();
            changeGrid.Children.Clear();
            changeGrid.Children.Add(grid);
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "")
            {
                tips.IsOpen = true;
            }
            else
            {
                Settings.Default["OnlineAccount"] = true;
                Settings.Default.Save();
                Grid grid;
                grid = new setColor();
                changeGrid.Children.Clear();
                changeGrid.Children.Add(grid);
            }
        }
    }
}
