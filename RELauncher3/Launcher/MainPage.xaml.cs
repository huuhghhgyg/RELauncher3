using RELauncher3.Launcher;
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
using RELauncher3.Update;
using System.Threading;
using System.Diagnostics;
using System.Windows.Media.Animation;
using System.IO;
using RELauncher3.Properties;
using System.Net;
using GetBingWallpaper;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace RELauncher3
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Grid
    {
        public MainPage()
        {
            InitializeComponent();

            SetMainPageUI();

            Thread checkupdate = new Thread(IfUpdate)
            {
                IsBackground = true
            };//检查更新
            checkupdate.Start();
        }

        string url;
        async void SetMainPageUI()
        {
            LaunchGrid.Visibility = Visibility.Visible;
            UserNameTile.Content = Settings.Default["UserName"].ToString();//磁块上显示用户名
            if (bool.Parse(Settings.Default["OnlineAccount"].ToString()) == true)//磁块显示验证模式
            {
                IsOnlineModeTile.Content = "在线模式";
            }
            else
            {
                IsOnlineModeTile.Content = "离线模式";
            }
            //Bing每日图片
            if (bool.Parse(Settings.Default["BingDaily"].ToString()) == true)
            {
                GetWallPaper WallPaperGetter = new GetWallPaper();
                if (Directory.Exists(@"./RE3/Theme/BingDaily") == false)
                {
                    Directory.CreateDirectory(@"./RE3/Theme/BingDaily");
                }
                WallPaperGetter.SavePathBase = @"./RE3/Theme/BingDaily/";
                WallPaperGetter.RefreshSavePath();
                if (File.Exists(WallPaperGetter.SavePath) != true)
                {
                        WallPaperGetter.GetWallPaperUrl();
                        url = "https://" + WallPaperGetter.WallPaperUrl;
                    //WallPaperGetter.DownloadFile();
                    await DownloadFileAsync(url, WallPaperGetter.SavePath);
                        Settings.Default["BGPPath"] = WallPaperGetter.SavePath;//设置背景图片路径
                        Settings.Default.Save();//保存路径
                }
            }

            //更改背景图片
            string path = Settings.Default["BGPPath"].ToString();//背景图片路径
            if (path != "null" && File.Exists(path))//如果背景图片的路径不为无 并且 背景图片 的 图片文件存在
            {
                ChangeBackground(path);//更改背景
            }
            bool IfStartBoxIsBlack = bool.Parse((Settings.Default["StartBoxIsBlack"].ToString()));//“开始”二字颜色是黑还是白？
            if (IfStartBoxIsBlack == false)
            {
                StartBox.Foreground = Brushes.White;//开始 变白
                if (path == "null" || File.Exists(path) == false)
                {
                    showGrid.Background = Brushes.Black;
                }
            }

            //完成!
            LaunchGrid.Visibility = Visibility.Hidden;
        }

        void ChangeBackground(string Path)//更改主页背景
        {
            if (File.Exists(Path))
            {
                var imgBrush = new ImageBrush();

                imgBrush.ImageSource = new BitmapImage(new Uri(@Path, UriKind.Relative));
                showGrid.Background = imgBrush;
            }
        }

        /// <summary>
        /// 软件版本在这里!!!!
        /// </summary>
        protected static double ver = checkUpdate.Ver;

        private void IfUpdate()//是否有更新
        {
            try
            {
                checkUpdate update = new checkUpdate();
                update.checkVer("https://huuhghhgyg.github.io/checkUpdate/Launcher3.up");

                //Compare Version
                int newVer = int.Parse(checkUpdate.NewSoftVersion);
                if (newVer > ver)
                {
                    Dispatcher.Invoke(new Action(delegate
                    {
                        UpdateInfo.Content = (newVer - ver).ToString();
                    }));
                }
            }
            catch { }
        }

        private void SettingTile_Click(object sender, RoutedEventArgs e)
        {
            Grid grid;
            grid = new settings();
            showGrid.Children.Clear();
            showGrid.Children.Add(grid);
        }

        private void LinkTile_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://huuhghhgyg.github.io");
        }

        private void LaunchVersionTile_Click(object sender, RoutedEventArgs e)
        {
            Grid grid;
            grid = new MCVersion();
            showGrid.Children.Clear();
            showGrid.Children.Add(grid);
        }

        private void LaunchTile_Click(object sender, RoutedEventArgs e)
        {
            Grid grid;
            grid = new LaunchVerify();
            showGrid.Children.Clear();
            showGrid.Children.Add(grid);
        }

        private void PersonalityTile_Click(object sender, RoutedEventArgs e)
        {
            Grid grid;
            grid = new Personality();
            showGrid.Children.Clear();
            showGrid.Children.Add(grid);
        }

        private void VersionTile_Click(object sender, RoutedEventArgs e)
        {
            Grid grid;
            grid = new UpdatePage();
            showGrid.Children.Clear();
            showGrid.Children.Add(grid);
        }

        private void ThemeTile_Click(object sender, RoutedEventArgs e)
        {
            Grid grid;
            grid = new ThemePage();
            showGrid.Children.Clear();
            showGrid.Children.Add(grid);
        }

        private void NotifitionTile_Click(object sender, RoutedEventArgs e)
        {
            Grid grid;
            grid = new NotifictionPage();
            showGrid.Children.Clear();
            showGrid.Children.Add(grid);
        }

        async Task DownloadFileAsync(string URL,string path)
        {
            using (WebClient client = new WebClient())
            {
                Task download = client.DownloadFileTaskAsync(URL, path);
                client.DownloadProgressChanged += client_DownloadProgressChanged;
                client.DownloadFileCompleted += client_DownloadFileCompleted;
                await download;
            }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double progress= (e.TotalBytesToReceive / e.BytesReceived * 100);//下载百分比
            LaunchProgressbar.Value = progress;
            if (LaunchProgressbar.Value == LaunchProgressbar.Maximum)
            {
                LaunchProgressbar.Visibility = Visibility.Hidden;
                LaunchProgressring.Visibility = Visibility.Visible;
            }
        }

        void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //下载成功
            LaunchProgressbar.Visibility = Visibility.Hidden;
            LaunchProgressring.Visibility = Visibility.Visible;
        }

    }
}
