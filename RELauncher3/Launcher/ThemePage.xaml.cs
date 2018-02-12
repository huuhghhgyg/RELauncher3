using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace RELauncher3.Launcher
{
    /// <summary>
    /// ThemePage.xaml 的交互逻辑
    /// </summary>
    public partial class ThemePage : Grid
    {
        public ThemePage()
        {
            InitializeComponent();
            Thread LoadItem = new Thread(AddThemeItem);
            LoadItem.Start();
        }

        string Info = "";
        string ThemeListOnce = "";
        void AddThemeItem()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ThemeListOnce = GetRequest("https://huuhghhgyg.github.io/RE3/Theme/ThemeList.content");
            string ThemeName = "", ThemeDir = "", ThemeIcon = "";//顺序也为 名字 目录 Icon
            while (ThemeListOnce != "")
            {
                count();
                ThemeName = Info;
                count();
                ThemeDir = "https://huuhghhgyg.github.io/RE3/Theme" + Info;
                count();
                ThemeIcon = Info;
                if (ThemeListOnce != "")//防止报错
                {
                    count();
                }
                Dispatcher.Invoke(new Action(delegate
                {
                    ThemeList.Children.Add(new Theme.ThemeItem(ThemeIcon, ThemeName, ThemeDir));
                }));
                //MSGBox.Text += s + "\n";
                Dispatcher.Invoke(new Action(delegate
                {
                    LoadingGrid.Visibility = Visibility.Hidden;
                }));
            }
        }

        void count()
        {
            Info = ThemeListOnce.Substring(0, ThemeListOnce.IndexOf("\n"));
            ThemeListOnce = ThemeListOnce.Substring(ThemeListOnce.IndexOf("\n") + 1);
        }

        static string GetRequest(string URL)//用于获取主题列表
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var request = (HttpWebRequest)WebRequest.Create(URL);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString.ToString();
        }

        private void BackTile_Click(object sender, RoutedEventArgs e)
        {
            Grid grid;
            grid = new MainPage();
            showGrid.Children.Clear();
            showGrid.Children.Add(grid);
        }
    }
}
