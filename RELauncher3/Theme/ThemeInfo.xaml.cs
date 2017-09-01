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
    /// ThemeInfo.xaml 的交互逻辑
    /// </summary>
    public partial class ThemeInfo : Grid
    {
        string IconURL = "";
        public ThemeInfo(string ThemeName,string _IconURL)
        {
            InitializeComponent();
            ThemeNameBlock.Text = ThemeName;//更改题目
            IconURL = _IconURL;
            Thread GetThemeIconThread = new Thread(GetIconThread);
        }

        void GetIconThread()
        {
            GetPictureFromURL(IconURL, IconBox);
        }

        void GetPictureFromURL(string URL, Image image)
        {
            var request = WebRequest.Create(URL);

            using (var response = request.GetResponse())
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

        private void BackTile_Click(object sender, RoutedEventArgs e)
        {
            Grid grid;
            grid = new Launcher.ThemePage();

        }
    }
}
