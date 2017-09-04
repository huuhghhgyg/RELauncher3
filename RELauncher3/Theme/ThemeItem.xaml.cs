using System;
using System.Collections.Generic;
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
    /// ThemeItem.xaml 的交互逻辑
    /// </summary>
    public partial class ThemeItem : UserControl
    {
        string IconURL = "";
        string dirURL = "";
        public ThemeItem(string _IconUrl,string title,string dir)
        {
            InitializeComponent();

            ThemeTile.Content = title;//设置标题(主题名字)
            IconURL = _IconUrl;
            dirURL = dir;
            GetPictureFromURL(_IconUrl, Image);//设置icon
        }

        async void GetPictureFromURL(string URL,Image image)
        {
            await Task.Run(() => Thread.Sleep(0));
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

                image.Source = bitmap;
            }
            await Task.Delay(0);
        }

        private void ThemeTile_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow.grid = new Launcher.settings();
            MainWindow.grid = new ThemeInfo(ThemeTile.Content.ToString(), dirURL);
        }
    }
}
