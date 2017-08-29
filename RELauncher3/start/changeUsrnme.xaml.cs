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
    /// changeUsrnme.xaml 的交互逻辑
    /// </summary>
    public partial class changeUsrnme : Grid
    {
        public changeUsrnme()
        {
            InitializeComponent();
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nametxt.Text == "")
            {
                tips.IsOpen = true;
            }
            else
            {
                Settings.Default["UserName"] = nametxt.Text;
                Settings.Default.Save();

                Grid grid;
                grid = new loginPage();
                showGrid.Children.Clear();
                showGrid.Children.Add(grid);
            }
        }

        private void nametxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            usrNameblk.Text = nametxt.Text;
        }
    }
}
