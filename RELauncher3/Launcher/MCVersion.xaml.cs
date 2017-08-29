using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    }
}
