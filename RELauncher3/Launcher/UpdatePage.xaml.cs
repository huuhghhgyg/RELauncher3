using RELauncher3.Update;
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
using System.Threading;
using System.Windows.Threading;
using System.Net;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Net.Http;

namespace RELauncher3.Launcher
{
    /// <summary>
    /// UpdatePage.xaml 的交互逻辑
    /// </summary>
    public partial class UpdatePage : Grid
    {
        public object CurrentDomain { get; private set; }

        public UpdatePage()
        {
            InitializeComponent();

            UpdateBtn.Visibility = Visibility.Hidden;//隐藏更新按钮
            loadBar.Visibility = Visibility.Visible;//显示load条
            downloadBar.Visibility = Visibility.Hidden;//隐藏进度条

            Thread CU = new Thread(CheckUpdate);//创建新线程检测更新
            CU.Start();//开始线程

            downloadBar.Visibility = Visibility.Hidden;//隐藏下载进度条
            isNewestVersionIcon.Visibility = Visibility.Hidden;//显示对号
        }

        void CheckUpdate()//检查更新方法
        {
            try
            {
                Dispatcher.Invoke(new Action(delegate
                {
                    downloadBar.Visibility = Visibility.Visible;
                }));//更新进度条显示状态

                checkUpdate update = new checkUpdate();
                update.checkVer("https://huuhghhgyg.github.io/checkUpdate/Launcher3.up");//给出访问地址

                //Compare Version
                int newVer = int.Parse(checkUpdate.NewSoftVersion);//Get新版本版本号
                if (newVer > checkUpdate.Ver)//如果Get的版本号 比 本地的版本号大
                {
                    Dispatcher.Invoke(new Action(delegate
                    {
                        UpdateBtn.Visibility = Visibility.Visible;//显示更新按钮

                        loadBar.Visibility = Visibility.Hidden;//隐藏load条
                        UpBlock.Text = "检测到有新版本";
                    }));
                }
                else
                {
                    Dispatcher.Invoke(new Action(delegate
                    {
                        loadBar.Visibility = Visibility.Hidden;//隐藏load条
                        UpBlock.Visibility = Visibility.Visible;//显示上标题栏
                        isNewestVersionIcon.Visibility = Visibility.Visible;//显示勾号
                    }));//更新UI（最新版本状态）
                }
            }
            catch//假装是最新版
            {
                Dispatcher.Invoke(new Action(delegate
                {
                    loadBar.Visibility = Visibility.Hidden;//隐藏load条
                    UpBlock.Visibility = Visibility.Visible;//显示下标题
                    isNewestVersionIcon.Visibility = Visibility.Visible;//显示对号
                }));//更新UI
            }
        }

        async void DownloadNewVersionAsync(string ProgramuUrl, string ConfigUrl)
        {
            string path = ("./UpdatedFile");
            if (Directory.Exists("./UpdatedFile") == false)//如果路径不存在
            {
                Directory.CreateDirectory(path);//创建路径文件夹
            }

            if (File.Exists("Recovery.exe") == false)//下载Recovery
            {
                //DownloadFile(checkUpdate.RecoveryURL, "Recovery.exe", downloadBar, UpBlock);//如果没有Recovery，下载
                UpBlock.Text = "正在下载Recovery";
                await DownloadFileAsync(checkUpdate.RecoveryURL, "Recovery.exe");//如果没有Recovery，下载
            }

            //DownloadFile(ProgramuUrl, "./UpdatedFile/RELauncher3.exe", downloadBar, UpBlock);//下载程序
            //DownloadFile(ConfigUrl, "./UpdatedFile/RELauncher3.exe.config", downloadBar, UpBlock);//下载配置文件
            await DownloadFileAsync(ProgramuUrl, "./UpdatedFile/RELauncher3.exe");//下载程序
            await DownloadFileAsync(ConfigUrl, "./UpdatedFile/RELauncher3.exe.config");//下载配置文件

            Dispatcher.Invoke(new Action(delegate
            {
                UpBlock.Text = "安装更新中";//上标题栏的内容显示为..
                downloadBar.Visibility = Visibility.Hidden;//隐藏下载条
                isNewestVersionIcon.Visibility = Visibility.Visible;//显示对号
            }));//下载完毕，再次更新UI
            Process.Start("Recovery.exe");//打开Recovery
            Environment.Exit(0);//退出程序以更新
        }

        private void BackTile_Click(object sender, RoutedEventArgs e)
        {
            Grid grid;
            grid = new MainPage();
            showGrid.Children.Clear();
            showGrid.Children.Add(grid);
        }

        async Task DownloadFileAsync(string URL, string path)
        {
            using (WebClient client = new WebClient())
            {
                Task download = client.DownloadFileTaskAsync(URL, path);
                client.DownloadProgressChanged += client_DownloadProgressChanged;
                client.DownloadFileCompleted += client_DownloadFileCompleted;
                await download;
            }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            string percent = (e.TotalBytesToReceive / e.BytesReceived * 100).ToString("0");
            UpBlock.Text = string.Format("已下载{0},({1}Mb，共{2}Mb)", percent, (e.BytesReceived/1024/1024).ToString("0.00"), (e.TotalBytesToReceive/1024/1024).ToString("0.00"));
            downloadBar.Value = e.ProgressPercentage;
        }

        void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("文件下载被取消", "提示");
            }
            downloadBar.Value = 0;
            UpBlock.Text = "下载完成";
            UpdateBtn.IsEnabled = true;
            DownloadRecoveryButton.IsEnabled = true;
            DownloadCleannerButton.IsEnabled = true;
            //下载成功
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            //更新UI状态为下载状态
            UpdateBtn.IsEnabled = false;
            DownloadRecoveryButton.IsEnabled = false;
            UpBlock.Visibility = Visibility.Visible;//显示上标题栏
            loadBar.Visibility = Visibility.Hidden;//隐藏load条
            downloadBar.Visibility = Visibility.Visible;//显示下载进度条
            UpBlock.Text = "正在下载新版本";//上标题栏的显示内容改为..
            DownloadNewVersionAsync(checkUpdate.ProgramURL, checkUpdate.ConfigURL);//获取地址并下载
        }

        private async void DownloadRecoveryButton_Click(object sender, RoutedEventArgs e)
        {
            UpBlock.Text = "正在下载Recovery";
            DownloadRecoveryButton.IsEnabled = false;
            if (File.Exists(@"Recovery.exe"))
            {
                File.Delete("Recovery.exe");
            }
            await DownloadFileAsync(checkUpdate.RecoveryURL, "Recovery.exe");//如果没有Recovery，下载
        }

        private async void DownloadCleannerButton_Click(object sender, RoutedEventArgs e)
        {
            UpBlock.Text = "正在下载Cleanner";
            DownloadCleannerButton.IsEnabled = false;
            if (File.Exists(@"Cleanner.exe"))
            {
                File.Delete("Cleanner.exe");
            }
            await DownloadFileAsync("http://launcher3-1251886115.cossh.myqcloud.com/RE3Cleanner.exe", "Cleanner.exe");//如果没有Recovery，下载
            Process.Start("Cleanner.exe");
            Environment.Exit(0);
        }
    }
}
