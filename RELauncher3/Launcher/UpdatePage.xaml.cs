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

        void DownloadNewVersion(string ProgramuUrl, string ConfigUrl)
        {
            string path = ("./UpdatedFile");
            if (Directory.Exists("./UpdatedFile") == false)//如果路径不存在
            {
                Directory.CreateDirectory(path);//创建路径文件夹
            }

            if (File.Exists("Recovery.exe") == false)//下载Recovery
            {
                DownloadFile(checkUpdate.RecoveryURL, "Recovery.exe", downloadBar, UpBlock);//如果没有Recovery，下载
            }

            DownloadFile(ProgramuUrl, "./UpdatedFile/RELauncher3.exe", downloadBar, UpBlock);//下载程序
            DownloadFile(ConfigUrl, "./UpdatedFile/RELauncher3.exe.config", downloadBar, UpBlock);//下载配置文件

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

        public void DownloadFile(string URL, string filename, ProgressBar prog, TextBlock label1)//下载文件 方法
        {
            float percent = 0;
            //try
            {
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
                if (prog != null)
                {
                    Dispatcher.Invoke(new Action(delegate
                    {
                        prog.Maximum = (int)totalBytes;
                    }));
                }
                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    //Application.DoEvents();
                    so.Write(by, 0, osize);
                    if (prog != null)
                    {
                        Dispatcher.Invoke(new Action(delegate
                        {
                            prog.Value = (int)totalDownloadedByte;
                        }));
                    }
                    osize = st.Read(by, 0, (int)by.Length);

                    percent = (float)totalDownloadedByte / (float)totalBytes * 100;
                    Dispatcher.Invoke(new Action(delegate
                    {
                        label1.Text = "下载进度" + percent.ToString("0") + "%";
                    }));
                    //System.Windows.Forms.Application.DoEvents(); //必须加注这句代码，否则label1将因为循环执行太快而来不及显示信息
                }
                so.Close();
                st.Close();
            }
            //catch (System.Exception)
            {
                //throw;
            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            //更新UI状态为下载状态
            UpBlock.Visibility = Visibility.Visible;//显示上标题栏
            loadBar.Visibility = Visibility.Hidden;//隐藏load条
            downloadBar.Visibility = Visibility.Visible;//显示下载进度条
            UpBlock.Text = "正在下载新版本";//上标题栏的显示内容改为..

            Thread _DownloadNewVersion = new Thread(DownloadNewVersion);
            _DownloadNewVersion.Start();
        }

       void DownloadNewVersion()
        {
            DownloadNewVersion(checkUpdate.ProgramURL, checkUpdate.ConfigURL);//获取地址并下载
        }
    }
}
