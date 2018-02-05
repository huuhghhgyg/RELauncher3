using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RELauncher3.Launcher
{
    /// <summary>
    /// MCVersion.xaml 的交互逻辑
    /// </summary>
    public partial class MCVersion : Grid
    {
        public MCVersion()
        {
            InitializeComponent();
            DataBase db = new DataBase();
            VersionJson = db.GetMinecraftVersionJson(1);
            LoadInfo();
        }

        void LoadInfo()
        {
            //填入信息
            var versions = App.Core.GetVersions().ToArray();
            ListBoxVersion.ItemsSource = versions;
            ListBoxVersion.DisplayMemberPath = "Id";

            //自动选择首项
            if (ListBoxVersion.Items.Count != 0)
            {
                ListBoxVersion.SelectedIndex = 0;
                FillInfo();
            }
            else
            {

            }
        }

        void FillInfo()
        {
            //!!!///获取id方法
            KMCCC.Launcher.Version ver = new KMCCC.Launcher.Version();
            ver = ListBoxVersion.SelectedItem as KMCCC.Launcher.Version;
            VersionBlock.Text = ver.Id;
            //!!!///
            string path = Directory.GetCurrentDirectory() + "\\.minecraft\\versions\\" + VersionBlock.Text;
            if (Directory.Exists(path) == true)
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                CreateTimeBlock.Text = dir.CreationTimeUtc.ToLongDateString();
            }
            PathBlock.Text = path;
        }

        private void BackTile_Click(object sender, RoutedEventArgs e)
        {
            Grid grid;
            grid = new MainPage();
            showGrid.Children.Clear();
            showGrid.Children.Add(grid);
        }

        static string VersionJson;//版本列表的Json文件
        async static void GetRequestAsync(string URL)//用于获取版本列表的json
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(URL))
                {
                    using (HttpContent content = response.Content)
                    {
                        VersionJson = await content.ReadAsStringAsync();
                    }
                }
            }
        }

        private void ListBoxVersion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FillInfo();
        }

        private void OpenPathButton_Click(object sender, RoutedEventArgs e)
        {
            if(Directory.Exists(PathBlock.Text))
            {
                Process.Start(PathBlock.Text);
            }
        }

        private void TurnToLaunchButton_Click(object sender, RoutedEventArgs e)
        {
            Grid grid;
            grid = new LaunchVerify();
            showGrid.Children.Clear();
            showGrid.Children.Add(grid);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(PathBlock.Text))
            {
                if (System.Windows.MessageBox.Show("您确定要删除吗？", "提示：", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    //delete
                    Directory.Delete(PathBlock.Text, true);
                    LoadInfo();
                }
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadInfo();
        }
    }
}
