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

namespace RELauncher3.Launcher
{
    /// <summary>
    /// ThemePage.xaml 的交互逻辑
    /// </summary>
    public partial class ThemePage : Grid
    {
        public ThemePage()//加载完后执行内容
        {
            InitializeComponent();
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
            Thread LoadItem = new Thread(AddThemeItem);
            LoadItem.Start();
            AddThemeItem();
            GetPictureFromURL("https://huuhghhgyg.github.io/RE3/Theme/Orginal/Icon.jpg", ExampleGrid);
        }

        void GetPictureFromURL(string URL, Grid grid)
        {
            var request = WebRequest.Create(URL);

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                var imgBrush = new ImageBrush();
                var bitmap = new BitmapImage();
                bitmap.BeginInit();//开始设置属性
                bitmap.StreamSource = stream;
                bitmap.EndInit();//终止设置属性
                imgBrush.ImageSource = bitmap;
                grid.Background = imgBrush;
            }
        }

        void AddThemeItem()
        {
            string ThemeListOnce = GetRequest("https://huuhghhgyg.github.io/RE3/Theme/ThemeList.content");
            //ThemeList.Children.Add(new Theme.ThemeItem("1", "1"));
            if (Directory.Exists("RE3") == false)//判断目录是否存在，如果不存在,
            {
                Directory.CreateDirectory("RE3");//创建目录
            }
            if (File.Exists("./RE3/ThemeList.tmp") == true)//如果指定tmp文件存在
            {
                File.Delete("./RE3/ThemeList.tmp");//删除指定tmp文件
            }

            StreamWriter sw = new StreamWriter("./RE3/ThemeList.tmp");//写入文件
            sw.Write(ThemeListOnce);//写入
            sw.Close();//关闭写入线程

            FileStream fs = new FileStream("./RE3/ThemeList.tmp", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string s, ThemeName = "", ThemeDir = "", ThemeIcon = "";//顺序也为 名字 目录 Icon
            int block = 0;
            while ((s = sr.ReadLine()) != null)
            {
                block++;
                switch (block)
                {
                    case 1:
                        ThemeName = s;
                        break;
                    case 2:
                        ThemeDir = "https://huuhghhgyg.github.io/RE3/Theme" + s;
                        break;
                    case 3:
                        ThemeIcon = s;
                        break;
                    case 4:
                        Dispatcher.Invoke(new Action(delegate
                        {
                            ThemeList.Children.Add(new Theme.ThemeItem(ThemeIcon, ThemeName, ThemeDir));
                        }));
                        block = 0;
                        break;
                }
                //MSGBox.Text += s + "\n";
            }
        }

        static string GetRequest(string URL)//用于获取主题列表
        {
            var request = (HttpWebRequest)WebRequest.Create(URL);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString.ToString();
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

            if (dialog.ShowDialog() == true)
            {
                //ChangeBackground(dialog.FileName);
                string filename = dialog.FileName;
                if (filename != "./BG.png" && File.Exists("BG.png") || filename != "./BG.jpg" && File.Exists("BG.jpg"))
                {//删除重复的文件
                    File.Delete("BG.jpg");//删除jpg
                    File.Delete("BG.png");//删除png
                }

                if (filename.Substring(filename.Length - 3, 3) == "png")//文件类型分类
                {//PNG文件
                    File.Copy(dialog.FileName, "./BG.png");
                    ChangeBackground("./BG.png");
                    Settings.Default["BGPPath"] = "./BG.png";
                }
                else//JPG文件或其他
                {
                    File.Copy(dialog.FileName, "./BG.png");
                    ChangeBackground("./BG.png");
                    Settings.Default["BGPPath"] = "./BG.jpg";
                }
                Settings.Default.Save();//保存设置
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
    }
}
