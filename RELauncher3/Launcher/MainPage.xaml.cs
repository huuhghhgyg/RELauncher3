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

            UserNameTile.Content = Settings.Default["UserName"].ToString();//磁块上显示用户名
            if (bool.Parse(Settings.Default["OnlineAccount"].ToString())==true)//磁块显示验证模式
            {
                IsOnlineModeTile.Content = "在线模式";
            }
            else
            {
                IsOnlineModeTile.Content = "离线模式";
            }

            Thread checkupdate = new Thread(IfUpdate);//检查更新
            checkupdate.IsBackground = true;
            checkupdate.Start();

            //更改背景图片
            string path = Settings.Default["BGPPath"].ToString();//背景图片路径
            if (path != "null")//如果背景图片的路径不为无
            {
                if (File.Exists(path))//如果背景图片 的 图片存在
                {
                    ChangeBackground(path);
                }
            }

            //string URL="http://huuhghhgyg.github.io/BGM/LifeInTheFastLane.mp3";
            //MediaPlayer player = new MediaPlayer();
            //player.Stop();
            //Uri uriStreaming = new Uri(URL);
            //player.Play();
        }

        void ChangeBackground(string Path)//更改主页背景
        {
            var imgBrush = new ImageBrush();

            imgBrush.ImageSource = new BitmapImage(new Uri(@Path, UriKind.Relative));
            showGrid.Background = imgBrush;
        }

        /// <summary>
        /// 软件版本在这里!!!!
        /// </summary>
        protected static int ver = checkUpdate.Ver;

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
            catch{}
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

        private void ThemeTile_Click(object sender, RoutedEventArgs e)
        {
            Grid grid;
            grid = new ThemePage();
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
    }
}
