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

namespace RELauncher3.Theme
{
    /// <summary>
    /// ThemeInfo.xaml 的交互逻辑
    /// </summary>
    public partial class ThemeInfo : Grid
    {
        string dirURL = "";
        string iconURL = "";
        public ThemeInfo(string ThemeName, string _dirURL)
        {
            InitializeComponent();
            ThemeNameBlock.Text = ThemeName;//更改题目
            dirURL = _dirURL;
            iconURL = dirURL + "/Icon.jpg";
            GetIconThread();
            GetThemeInfo();
        }

        string ThemeInfo_str = "";
        string Info = "";
        async void GetThemeInfo()
        {
            ThemeInfo_str = await GetRequest(dirURL + "/Theme.Info");//获取主题信息
            while (ThemeInfo_str != "")
            {
                count();//读下一行
                switch (Info)
                {
                    case "Author":
                        count();
                        AuthorBox.Text = Info;
                        break;

                    case "Picture1":
                        count();
                        Image Picture1 = new Image();
                        Picture1.Source = await GetPicture2ImageAsync(dirURL + "/Picture1.png");
                        PictureFlipView.BannerText = ThemeNameBlock.Text;
                        PictureFlipView.Items.Add(Picture1);
                        break;

                    case "Picture2":
                        count();
                        Image Picture2 = new Image();
                        Picture2.Source = await GetPicture2ImageAsync(dirURL + "/Picture2.png");
                        PictureFlipView.Items.Add(Picture2);
                        break;

                    case "Picture3":
                        count();
                        Image Picture3 = new Image();
                        Picture3.Source = await GetPicture2ImageAsync(dirURL + "/Picture3.png");
                        PictureFlipView.Items.Add(Picture3);
                        break;

                    case "Description":
                        count();
                        ThemeDescribtion.Text = Info;
                        break;

                    case "Color":
                        count();
                        Color = Info;//将颜色赋值到变量Color
                        break;

                    case "Background":
                        count();
                        BackgroundPicture = Info;//将背景值赋值到background变量
                        break;

                    case "StartIsBlack":
                        count();
                        StartIsBlack = bool.Parse(Info);
                        break;
                }
            }
            LoadingPictureRing.Visibility = Visibility.Hidden;
        }

        void count()
        {
            try
            {
                Info = ThemeInfo_str.Substring(0, ThemeInfo_str.IndexOf("\n"));
                ThemeInfo_str = ThemeInfo_str.Substring(ThemeInfo_str.IndexOf("\n") + 1);
            }
            catch
            {
                ThemeInfo_str = "";//结束循环
            }
        }

        void PopupMessage(string msg)
        {
            TipBoard.Header = msg;
            TipBoard.IsOpen = true;
        }

        async void GetIconThread()
        {
            await GetPictureFromURL(iconURL, IconBox);
            LoadingIconRing.Visibility = Visibility.Hidden;
        }

        async Task<BitmapImage> GetPicture2ImageAsync(string URL)//将网址转换成iamge
        {
            var request = WebRequest.Create(URL);
            request.Timeout = 5000;

            using (var response = await request.GetResponseAsync())
            {
                var fullFilePath = @URL;

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
                bitmap.EndInit();

                return bitmap;
            }
        }

        async Task GetPictureFromURL(string URL, Image image)
        {
            var request = WebRequest.Create(URL);
            using (var response = await request.GetResponseAsync())
            {
                //var imgBrush = new ImageBrush();
                //var bitmap = new BitmapImage();
                //bitmap.BeginInit();//开始设置属性
                //bitmap.StreamSource = stream;
                //bitmap.EndInit();//终止设置属性
                //imgBrush.ImageSource = bitmap;
                //grid.Background = imgBrush;

                //image = new Image();
                var fullFilePath = @URL;

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
                bitmap.EndInit();

                image.Source = bitmap;
            }
        }

        async Task<string> GetRequest(string URL)//用于获取主题列表
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(URL);
        }


        private void BackTile_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.grid = new Launcher.ThemePage();
        }

        //DataBase:
        string BackgroundPicture = "";//背景路径 (认为它是完整的链接) (腾讯云) png
        string Color = "Blue";//颜色 (默认蓝色)
        bool StartIsBlack = true;//开始二字的颜色为黑:默认是

        private void InstallBtn_Click(object sender, RoutedEventArgs e)
        {
            ProgressringLoading.Visibility = Visibility.Visible;
            ///创建路径
            if (Directory.Exists(@"./RE3/Theme/" + ThemeNameBlock.Text) == false)
            {
                Directory.CreateDirectory(@"./RE3/Theme/" + ThemeNameBlock.Text);
            }
            //下载背景图
            string _backgroundpath = "./RE3/Theme/" + ThemeNameBlock.Text + "/Background.png";
            try
            {
                if (BackgroundPicture != "")
                {
                    if (File.Exists(@_backgroundpath))
                    {
                        File.Delete(@_backgroundpath);
                    }
                    DownloadFile(BackgroundPicture, @_backgroundpath);
                }

                //应用背景
                Settings.Default["BGPPath"] = @_backgroundpath;//设置背景图片路径
                ///设置颜色
                Settings.Default["ThemeColor"] = Color;
                MainWindow.changeColor(Color, "BaseLight");//更改颜色
                Settings.Default["StartBoxIsBlack"] = StartIsBlack;
                Settings.Default["BingDaily"] = false;
                Settings.Default.Save();
                PopupMessage("已应用主题");
                ProgressringLoading.Visibility = Visibility.Hidden;
            }
            catch (System.IO.IOException)
            {
                PopupMessage("失败，已应用此主题。请转到主题管理清除缓存；或选择其它主题");
            }
        }

        void DownloadFile(string url, string SavePath)//下载地址，保存路径
        {
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFile(url, SavePath);
        }
    }
}
