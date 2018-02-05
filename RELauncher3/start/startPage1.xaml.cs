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
using MahApps.Metro.Controls.Dialogs;
using RELauncher3.Properties;
using System.Threading;
using System.IO;
using System.Net;

namespace RELauncher3.start
{
    /// <summary>
    /// startPage1.xaml 的交互逻辑
    /// </summary>
    public partial class startPage1 : Grid
    {
        public startPage1()
        {
            InitializeComponent();

            quesBlock.Text = "您将以" + System.Environment.UserName + "的用户名登录游戏";
            usrNameblk.Text = System.Environment.UserName;
            btnCNm.Content = "不是" + Environment.UserName + "?";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default["UserName"] = usrNameblk.Text;
            Settings.Default.Save();

            Grid grid;
            grid = new loginPage();
            showGrid.Children.Clear();
            showGrid.Children.Add(grid);
        }

        private void btnCNm_Click(object sender, RoutedEventArgs e)
        {
            Grid grid;
            grid = new changeUsrnme();
            showGrid.Children.Clear();
            showGrid.Children.Add(grid);
        }
    }
}
