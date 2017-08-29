using RELauncher3.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// setLaunchProp.xaml 的交互逻辑
    /// </summary>
    public partial class setLaunchProp : Grid
    {
        public setLaunchProp()
        {
            InitializeComponent();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (JavaText.Text == "" || MemoryText.Text == "")
            {
                tips.IsOpen = true;
            }
            else
            {
                Settings.Default["JavaPath"] = JavaText.Text;
                Settings.Default["Memory"] = MemoryText.Text;
                Settings.Default["AutoSetMemory"] = enabledSwitch.IsChecked;
                Settings.Default.Save();

                Grid grid;
                grid = new MainPage();
                showGrid.Children.Clear();
                showGrid.Children.Add(grid);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JavaText.Text = KMCCC.Tools.SystemTools.FindJava().Last();
            }
            catch
            {
                tips2.IsOpen = true;
                Process.Start("C:/Program Files(x86)/Java");
            }
        }

        private void enabledSwitch_Checked(object sender, RoutedEventArgs e)
        {
            DataBase DB = new DataBase();
            MemoryText.Text = DB.AutoSetMemory();
        }
    }
}
