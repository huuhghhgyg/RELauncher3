using MahApps.Metro;
using MahApps.Metro.Controls;
using RELauncher3.start;
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

namespace RELauncher3
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //初次设置
            if (Settings.Default["UserName"].ToString() == "")//判断是否进入设置向导
            {
                changeColor("Blue", "BaseLight");

                Grid grid;
                grid = new startPage1();
                showGrid.Children.Clear();
                showGrid.Children.Add(grid);
            }
            else
            {
                changeColor(Settings.Default["ThemeColor"].ToString(), "BaseLight");

                Grid grid;
                grid = new MainPage();
                showGrid.Children.Clear();
                showGrid.Children.Add(grid);
            }
        }

        public static void changeColor(string Color, string bgColor)//Color:“Red”, “Green”, “Blue”, “Purple”, “Orange”, “Lime”, “Emerald”, “Teal”, “Cyan”, “Cobalt”, “Indigo”, “Violet”, “Pink”, “Magenta”, “Crimson”, “Amber”, “Yellow”, “Brown”, “Olive”, “Steel”, “Mauve”, “Taupe”, “Sienna”;bgcolor:“BaseLight”, “BaseDark"
        {
            // 从应用程序获取当前的应用程序样式（主题和重音）
            // 您可以使用当前主题和自定义口音，而不是设置一个新的主题
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);

            // now set the Green accent and dark theme
            ThemeManager.ChangeAppStyle(Application.Current,
                                        ThemeManager.GetAccent(Color),
                                        ThemeManager.GetAppTheme(bgColor)); // or appStyle.Item1
        }

        public static string getColorName()
        {
            return Settings.Default["ThemeColor"].ToString();
        }

        private void showWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        /*public async Task<string> showMessageMSG(string title, string message)
        {
            rest:
            string result = await this.ShowInputAsync(title, message);
            if (result == null) //user pressed cancel
            {
                await this.ShowMessageAsync("提示", "输入值为空，重新输入");
                goto rest;
            }
            else
            {
                return result;
            }
        }*/
    }
}
