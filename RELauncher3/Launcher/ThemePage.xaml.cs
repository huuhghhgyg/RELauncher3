using RELauncher3.Properties;
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

        private void InstallThemeButton_Click(object sender, RoutedEventArgs e)
        {
            if (ThemeListBox.SelectedItem != null)//列表中有选则的项目
            {
                string Color = "Blue";//默认蓝色
                bool StartIsBlack = false;

                string ThemeName = ThemeListBox.SelectedItem.ToString();
                string _backgroundpath = "./RE3/Theme/" + ThemeName + "/Background.png";
                string infoPath = "./RE3/Theme/" + ThemeName + "/Theme.Info";

                if (File.Exists(infoPath))
                {
                    StreamReader sr = new StreamReader("./RE3/Theme/" + ThemeName + "/Theme.Info", Encoding.Default);
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.ToString() == "Color")
                        {
                            line = sr.ReadLine();
                            Color = line.ToString();
                        }
                        if (line.ToString() == "StartIsBlack")
                        {
                            line = sr.ReadLine();
                            StartIsBlack = bool.Parse(line.ToString());
                        }
                    }

                    try
                    {
                        //应用背景
                        Settings.Default["BGPPath"] = @_backgroundpath;//设置背景图片路径
                                                                       ///设置颜色
                        Settings.Default["ThemeColor"] = Color;
                        MainWindow.changeColor(Color, "BaseLight");//更改颜色
                        Settings.Default["StartBoxIsBlack"] = StartIsBlack;
                        Settings.Default["BingDaily"] = false;
                        Settings.Default.Save();
                        PopupMessage("已应用主题");
                        //ProgressringLoading.Visibility = Visibility.Hidden;
                    }
                    catch (System.IO.IOException)
                    {
                        PopupMessage("失败，已应用此主题。请转到主题管理清除缓存；或选择其它主题");
                    }
                    //MessageBox.Show(ThemeName);
                }
            }
        }
    }
}
