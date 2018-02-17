using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            AddThemeItem();
            LoadThemeList();
        }

        void LoadThemeList()
        {
            if (Directory.Exists(@"./RE3/Theme"))
            {
                string[] ThemeList = Directory.GetDirectories(@"./RE3/Theme");
                ThemeListBox.Items.Clear();//清除所有项目

                foreach (string ThemeName in ThemeList)
                {
                    ThemeListBox.Items.Add(ThemeName.Substring(ThemeName.LastIndexOf("\\") + 1));
                }
            }
        }

        string Info = "";
        string ThemeListOnce = "";
        async void AddThemeItem()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ThemeListOnce = await GetRequest("https://huuhghhgyg.github.io/RE3/Theme/ThemeList.content");
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
                ThemeList.Children.Add(new Theme.ThemeItem(ThemeIcon, ThemeName, ThemeDir));
                //MSGBox.Text += s + "\n";
                LoadingGrid.Visibility = Visibility.Hidden;
                LoadingRing.Visibility = Visibility.Hidden;
            }
        }

        void count()
        {
            Info = ThemeListOnce.Substring(0, ThemeListOnce.IndexOf("\n"));
            ThemeListOnce = ThemeListOnce.Substring(ThemeListOnce.IndexOf("\n") + 1);
        }

        async Task<string> GetRequest(string URL)//用于获取主题列表
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpClient client = new HttpClient();
            return await client.GetStringAsync(URL);
        }

        private void BackTile_Click(object sender, RoutedEventArgs e)
        {
            Grid grid;
            grid = new MainPage();
            showGrid.Children.Clear();
            showGrid.Children.Add(grid);
        }

        private void AddThemeButton_Click(object sender, RoutedEventArgs e)
        {
            ThemeTabControl.SelectedIndex = 0;
        }

        void PopupMessage(string msg)
        {
            TipBoard.Header = msg;
            TipBoard.IsOpen = true;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Directory.Delete(@"./RE3/Theme/" + ThemeListBox.SelectedItem, true);
            }
            catch (System.IO.IOException)
            {
                PopupMessage("失败，你已使用此主题。或者您可以清除缓存");
            }
            LoadThemeList();
        }

        void DeleteTheme()
        {
            if (File.Exists(@"RE3Cleanner.exe"))
            {
                Process.Start("RE3Cleanner.exe");
                Environment.Exit(0);
            }
            else
            {
                WebClient myWebClient = new WebClient();
                myWebClient.DownloadFile("http://launcher3-1251886115.cossh.myqcloud.com/RE3Cleanner.exe", @"./RE3Cleanner.exe");
                Process.Start("RE3Cleanner.exe");
                Environment.Exit(0);
            }
        }

        private void ClearCacheButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteTheme();
        }
    }
}
