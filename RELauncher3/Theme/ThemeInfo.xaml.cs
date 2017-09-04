﻿using System;
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
            await Task.Run(() => Thread.Sleep(0));

            ThemeInfo_str = GetRequest(dirURL + "/Theme.Info");//获取主题信息
            while (ThemeInfo_str != "")
            {
                count();
                switch (Info)
                {
                    case "Author":
                        count();
                        AuthorBox.Text = Info;
                        break;

                    case "Picture1":
                        count();
                        Image Picture1 = new Image();

                        await Task.Run(() => Thread.Sleep(0));
                        Picture1.Source = GetPicture2Image(dirURL + "/Picture1.png");
                        PictureFlipView.BannerText = "展示图1";
                        await Task.Delay(0);

                        PictureFlipView.Items.Add(Picture1);
                        break;

                    case "Picture2":
                        count();
                        Image Picture2 = new Image();

                        await Task.Run(() => Thread.Sleep(0));
                        Picture2.Source = GetPicture2Image(dirURL + "/Picture1.png");
                        PictureFlipView.BannerText = "展示图2";
                        await Task.Delay(0);

                        PictureFlipView.Items.Add(Picture2);
                        break;

                    case "Picture3":
                        count();
                        Image Picture3 = new Image();

                        await Task.Run(() => Thread.Sleep(0));
                        Picture3.Source = GetPicture2Image(dirURL + "/Picture1.png");
                        PictureFlipView.BannerText = "展示图1";
                        await Task.Delay(0);

                        PictureFlipView.Items.Add(Picture3);
                        break;

                    case "Description":
                        count();
                        ThemeDescribtion.Text = Info;
                        break;
                }
            }
            await Task.Delay(0);
        }

        void count()
        {
            Info = ThemeInfo_str.Substring(0, ThemeInfo_str.IndexOf("\n"));
            ThemeInfo_str = ThemeInfo_str.Substring(ThemeInfo_str.IndexOf("\n") + 1);
        }

        async void GetIconThread()
        {
            await Task.Run(() => Thread.Sleep(10));
            GetPictureFromURL(iconURL, IconBox);
            await Task.Delay(100);
        }

        BitmapImage GetPicture2Image(string URL)//将网址转换成iamge
        {
            var request = WebRequest.Create(URL);

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                var fullFilePath = @URL;

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
                bitmap.EndInit();

                return bitmap;
            }
        }

        async void GetPictureFromURL(string URL, Image image)
        {
            var request = WebRequest.Create(URL);

            using (var response = await request.GetResponseAsync())
            using (var stream = response.GetResponseStream())
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

                Dispatcher.Invoke(new Action(delegate
                {
                    image.Source = bitmap;
                }));
            }
        }

        static string GetRequest(string URL)//用于获取主题列表
        {
            var request = (HttpWebRequest)WebRequest.Create(URL);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString.ToString();
        }


        private void BackTile_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.grid = new Launcher.ThemePage();
        }
    }
}
