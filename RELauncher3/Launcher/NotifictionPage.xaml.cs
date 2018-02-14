using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    /// NotifictionPage.xaml 的交互逻辑
    /// </summary>
    public partial class NotifictionPage : Grid
    {
        public NotifictionPage()
        {
            InitializeComponent();

            Progressring.Visibility = Visibility.Visible;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            GetNoticeAsync();
            //旧方法
            //Notice = GetRequest("https://huuhghhgyg.github.io/RE3/Notifiction.txt");
        }
        string Notice;

        async void GetNoticeAsync()
        {
            Notice = await GetRequestAsync("https://huuhghhgyg.github.io/RE3/Notifiction.txt");
            NoticeBlock.Text = Notice;
            Progressring.Visibility = Visibility.Hidden;
        }

        async Task<string> GetRequestAsync(string URL)
        {
            HttpClient httpClient = new HttpClient();
            Task<string> getStringTask = httpClient.GetStringAsync(URL);
            string result = await getStringTask;
            return result;
        }

        static string GetRequest(string URL)//用于获取指定页面字符串（过时）
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var request = (HttpWebRequest)WebRequest.Create(URL);
            request.Timeout = 5000;
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString.ToString();
        }

        private void BackTile_Click(object sender, RoutedEventArgs e)
        {
            Grid grid;
            grid = new MainPage();
            showGrid.Children.Clear();
            showGrid.Children.Add(grid);
        }
    }
}
