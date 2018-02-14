using Microsoft.Win32;
using RELauncher3.Properties;
using System;
using System.Collections.Generic;
using System.IO;
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
using MahApps.Metro.Controls;
using System.Net.Http;
using System.Net;
using System.Drawing;
using System.Threading;
using GetBingWallpaper;

namespace RELauncher3.Launcher
{
    /// <summary>
    /// ThemePage.xaml 的交互逻辑
    /// </summary>
    public partial class Personality : Grid
    {
        public Personality()//加载完后执行内容
        {
            InitializeComponent();

            LoadingGrid.Visibility = Visibility.Visible;
            AddThemeItemAsync();
            //AddThemeItem();
            //GetPictureFromURL("https://huuhghhgyg.github.io/RE3/Theme/Orginal/Icon.jpg", ExampleGrid);
            LoadSettings();//读取设置
        }

        void LoadSettings()//读取设置
        {
            ColorBlock.Text = MainWindow.getColorName();//获取当前颜色的名字

            string path = Settings.Default["BGPPath"].ToString();//背景图片路径
            if (path != "null")//如果背景图片的路径不为无
            {
                if (File.Exists(path))//如果背景图片 的 图片存在
                {
                    ChangeBackground(path);
                }
            }

            if (Settings.Default["BGPPath"].ToString() != "null")//有自定义的图片时
            {
                YourPictureSwitch.IsChecked = true;//switch改为开启（默认关闭）
            }
            BingDaily.IsChecked = bool.Parse(Settings.Default["BingDaily"].ToString());
            StartBoxIsBlack.IsChecked = bool.Parse(Settings.Default["StartBoxIsBlack"].ToString());//读取“开始”二字颜色
        }

        async void GetPictureFromURL(string URL, Grid grid)
        {
            HttpClient client = new HttpClient();
            var stream = await client.GetStreamAsync(URL);
            var imgBrush = new ImageBrush();
            var bitmap = new BitmapImage();
            bitmap.BeginInit();//开始设置属性
            bitmap.StreamSource = stream;
            bitmap.EndInit();//终止设置属性
            imgBrush.ImageSource = bitmap;
            grid.Background = imgBrush;
        }

        string Info = "";
        string ThemeListOnce = "";
        async void AddThemeItemAsync()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ThemeListOnce = await GetRequest("https://huuhghhgyg.github.io/RE3/Theme/ThemeList.content");
            //ThemeListOnce = GetRequest("http://launcher3-1251886115.cossh.myqcloud.com/Theme/ThemeList.content");
            string ThemeName = "", ThemeDir = "", ThemeIcon = "";//顺序也为 名字 目录 Icon
            while (ThemeListOnce != "")
            {
                count();
                ///此处定制
                ThemeName = Info;
                count();
                ThemeDir = "https://huuhghhgyg.github.io/RE3/Theme" + Info;
                //ThemeDir = "http://launcher3-1251886115.cossh.myqcloud.com/Theme" + Info;
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
            }
            Dispatcher.Invoke(new Action(delegate
            {
                LoadingGrid.Visibility = Visibility.Hidden;
            }));
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

        void ChangeBackground(string Path)//更改主页背景
        {
            var imgBrush = new ImageBrush();

            imgBrush.ImageSource = new BitmapImage(new Uri(@Path, UriKind.Relative));
            ExampleGrid.Background = imgBrush;
        }

        private void BackTile_Click(object sender, RoutedEventArgs e)
        {
            Grid grid;
            grid = new MainPage();
            showGrid.Children.Clear();
            showGrid.Children.Add(grid);
        }

        void ChangeColor(MahApps.Metro.Controls.Tile TileName)
        {
            string _TileName = TileName.Name;//获取Tile的名称
            string ColorName = _TileName.Substring(0, _TileName.Length - 4);//获取Tile的工作颜色
            MainWindow.changeColor(ColorName, "BaseLight");//更改颜色
            Settings.Default["ThemeColor"] = ColorName;
            Settings.Default.Save();

            ColorBlock.Text = ColorName;//更改TextBlock显示的颜色
        }

        private void RedTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(RedTile);
        }

        private void GreenTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(GreenTile);
        }

        private void BlueTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(BlueTile);
        }

        private void PurpleTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(PurpleTile);
        }

        private void OrangeTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(OrangeTile);
        }

        private void LimeTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(LimeTile);
        }

        private void EmeraldTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(EmeraldTile);
        }

        private void TealTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(TealTile);
        }

        private void CyanTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(CyanTile);
        }

        private void CobaltTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(CobaltTile);
        }

        private void IndigoTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(IndigoTile);
        }

        private void VioletTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(VioletTile);
        }

        private void PinkTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(PinkTile);
        }

        private void MagentaTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(MagentaTile);
        }

        private void CrimsonTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(CrimsonTile);
        }

        private void AmberTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(AmberTile);
        }

        private void YellowTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(YellowTile);
        }

        private void BrownTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(BrownTile);
        }

        private void OliveTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(OliveTile);
        }

        private void SteelTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(SteelTile);
        }

        private void MauveTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(MauveTile);
        }

        private void TaupeTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(TaupeTile);
        }

        private void SiennaTile_Click(object sender, RoutedEventArgs e)
        {
            ChangeColor(SiennaTile);
        }

        private void SelectPictureButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();//打开选择文件对话框
            dialog.Filter = "PNG图片|*.png|JPG图片|*.jpg";//可选择png或者jpg文件

            if (dialog.ShowDialog() == true)//如果对话框返回了路径
            {
                string filename = dialog.FileName;
                if (File.Exists(filename))//如果指定目录文件存在
                {
                    ChangeBackground(filename);//更换背景图片
                    Settings.Default["BGPPath"] = filename;//设置背景图片路径
                    Settings.Default.Save();//保存路径
                }
            }
        }

        private void YourPictureSwitch_Click(object sender, RoutedEventArgs e)
        {
            if (YourPictureSwitch.IsChecked == false)
            {
                Settings.Default["BGPPath"] = "null";
                Settings.Default.Save();
            }
        }

        private void StartBoxIsBlack_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default["StartBoxIsBlack"] = StartBoxIsBlack.IsChecked;
            Settings.Default.Save();
        }

        private void BingDaily_Click(object sender, RoutedEventArgs e)
        {
            GetWallPaper WallPaperGetter = new GetWallPaper();
            if (BingDaily.IsChecked == false)
            {
                Settings.Default["BingDaily"] = false;
                Settings.Default.Save();
            }
            else
            {
                Settings.Default["BingDaily"] = true;
                Settings.Default["BGPPath"] = WallPaperGetter.SavePath;
                Settings.Default.Save();
            }
        }
    }
}