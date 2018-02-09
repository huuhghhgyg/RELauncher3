using KMCCC.Authentication;
using KMCCC.Launcher;
using KMCCC.Tools;
using MahApps.Metro.Controls.Dialogs;
using RELauncher3.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
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


namespace RELauncher3.Launcher
{
    /// <summary>
    /// SetVersion.xaml 的交互逻辑
    /// </summary>
    public partial class LaunchVerify : Grid
    {
        public LaunchVerify()
        {
            InitializeComponent();

            var versions = App.Core.GetVersions().ToArray();
            listBoxVersion.ItemsSource = versions;
            listBoxVersion.DisplayMemberPath = "Id";

            UsernameBlock.Text = Settings.Default["UserName"].ToString();
            OnlineModeBlock.Text = Settings.Default["OnlineAccount"].ToString();
            if (Settings.Default["AutoSetMemory"].ToString() == "true")
            {
                DataBase DB = new DataBase();
                Settings.Default["AutoSetMemory"] = DB.AutoSetMemory();
                Settings.Default.Save();
            }
            MemoryText.Text = Settings.Default["Memory"].ToString();

            if (listBoxVersion.Items.Count == 0)//如果没有任何可选择的版本就提示
            {
                LaunchListEmpty.Visibility = Visibility.Visible;
            }
        }

        private void BackTile_Click(object sender, RoutedEventArgs e)
        {
            Grid grid;
            grid = new MainPage();
            showGrid.Children.Clear();
            showGrid.Children.Add(grid);
        }

        private void LauchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxVersion.SelectedItem != null)
            {
                Thread launch = new Thread(LaunchGame);
                launch.IsBackground = true;
                launch.Start();
            }
            else
            {
                //提示请选择版本
                PopupMessage("请选择要启动的版本");
            }
        }

        void PopupMessage(string msg)
        {
            TipBoard.Header = msg;
            TipBoard.IsOpen = true;
        }

        void LaunchGame()
        {

            Dispatcher.Invoke(new Action(delegate
            {
                string username = Settings.Default["UserName"].ToString();//用户名
                string password = Settings.Default["Password"].ToString();//密码
                bool isTwitch = bool.Parse(Settings.Default["TwitchLogin"].ToString());//是否登录twitch

                LaunchMode mode;
                if (Settings.Default["LaunchMode"].ToString() == "0")//获取登录模式
                {
                    mode = LaunchMode.MCLauncher;
                }
                else
                {
                    mode = LaunchMode.BmclMode;
                }

                string width, height;
                width = Settings.Default["GameWidth"].ToString();//获取游戏窗口宽度
                height = Settings.Default["GameHeight"].ToString();//获取游戏窗口高度
                if (width == "" || height == "")//没有宽度或高度的话设置默认值
                {
                    width = "1280";
                    height = "768";
                }

                string _ServerIP = "", _ServerPort = "";
                if (bool.Parse(Settings.Default["AutoConnectServer"].ToString()) == true)//是否自动连接服务器
                {
                    _ServerIP = Settings.Default["ServerIP"].ToString();//读ip
                    _ServerPort = Settings.Default["ServerPort"].ToString();//读端口
                }

                {
                    LaunchResult resultCheck;
                    if (bool.Parse(Settings.Default["OnlineAccount"].ToString()) == true)//启动选项
                    {//正版登陆
                        if (Settings.Default["AutoConnectServer"].ToString() == "true")//自动连接服务器
                        {
                            var ver = (KMCCC.Launcher.Version)listBoxVersion.SelectedItem;
                            var result = App.Core.Launch(new LaunchOptions
                            {
                                Version = ver, //Ver为Versions里你要启动的版本名字
                                MaxMemory = int.Parse(MemoryText.Text), //最大内存，int类型
                                                                        //Authenticator = new OfflineAuthenticator(username), //离线启动，ZhaiSoul那儿为你要设置的游戏名
                                Authenticator = new YggdrasilLogin(username, password, isTwitch), // 正版启动，最后一个为是否twitch登录
                                Mode = mode, //启动模式，这个我会在后面解释有哪几种
                                Server = new ServerInfo { Address = _ServerIP, Port = ushort.Parse(_ServerPort) }, //设置启动游戏后，自动加入指定IP的服务器，可以不要
                                Size = new WindowSize { Height = ushort.Parse(height), Width = ushort.Parse(width) } //设置窗口大小，可以不要
                            });
                            resultCheck = result;
                        }
                        else
                        {
                            var ver = (KMCCC.Launcher.Version)listBoxVersion.SelectedItem;
                            var result = App.Core.Launch(new LaunchOptions
                            {
                                Version = ver, //Ver为Versions里你要启动的版本名字
                                MaxMemory = int.Parse(MemoryText.Text), //最大内存，int类型
                                                                        //Authenticator = new OfflineAuthenticator(username), //离线启动，ZhaiSoul那儿为你要设置的游戏名
                                Authenticator = new YggdrasilLogin(username, password, isTwitch), // 正版启动，最后一个为是否twitch登录
                                Mode = mode, //启动模式，这个我会在后面解释有哪几种
                                //Server = new ServerInfo { Address = _ServerIP, Port = ushort.Parse(_ServerPort) }, //设置启动游戏后，自动加入指定IP的服务器，可以不要
                                Size = new WindowSize { Height = ushort.Parse(height), Width = ushort.Parse(width) } //设置窗口大小，可以不要
                            });
                            resultCheck = result;
                        }
                    }
                    else
                    {//离线模式
                        if (Settings.Default["AutoConnectServer"].ToString() == "true")
                        {
                            var ver = (KMCCC.Launcher.Version)listBoxVersion.SelectedItem;
                            var result = App.Core.Launch(new LaunchOptions
                            {
                                Version = ver, //Ver为Versions里你要启动的版本名字
                                MaxMemory = int.Parse(MemoryText.Text), //最大内存，int类型
                                Authenticator = new OfflineAuthenticator(username), //离线启动，ZhaiSoul那儿为你要设置的游戏名
                                                                                    //Authenticator = new YggdrasilLogin("邮箱", "密码", true), // 正版启动，最后一个为是否twitch登录
                                Mode = mode, //启动模式，这个我会在后面解释有哪几种
                                Server = new ServerInfo { Address = _ServerIP, Port = ushort.Parse(_ServerPort) }, //设置启动游戏后，自动加入指定IP的服务器，可以不要
                                Size = new WindowSize { Height = ushort.Parse(height), Width = ushort.Parse(width) } //设置窗口大小，可以不要
                            });
                            resultCheck = result;
                        }
                        else
                        {
                            var ver = (KMCCC.Launcher.Version)listBoxVersion.SelectedItem;
                            var result = App.Core.Launch(new LaunchOptions
                            {
                                Version = ver, //Ver为Versions里你要启动的版本名字
                                MaxMemory = int.Parse(MemoryText.Text), //最大内存，int类型
                                Authenticator = new OfflineAuthenticator(username), //离线启动，ZhaiSoul那儿为你要设置的游戏名
                                                                                    //Authenticator = new YggdrasilLogin("邮箱", "密码", true), // 正版启动，最后一个为是否twitch登录
                                Mode = mode, //启动模式，这个我会在后面解释有哪几种
                                //Server = new ServerInfo { Address = _ServerIP, Port = ushort.Parse(_ServerPort) }, //设置启动游戏后，自动加入指定IP的服务器，可以不要
                                Size = new WindowSize { Height = ushort.Parse(height), Width = ushort.Parse(width) } //设置窗口大小，可以不要
                            });
                            resultCheck = result;
                        }
                    }
                    LaunchBtn.Content = "已启动";

                    if (!resultCheck.Success)//检测错误
                    {
                        //MessageBox.Show(result.ErrorMessage, result.ErrorType.ToString());
                        switch (resultCheck.ErrorType)
                        {
                            case ErrorType.NoJAVA:
                                MessageBox.Show("你系统的Java有异常，可能你非正常途径删除过Java，请尝试重新安装Java\n详细信息：" + resultCheck.ErrorMessage, "错误");
                                break;
                            case ErrorType.AuthenticationFailed:
                                MessageBox.Show("正版验证失败！请检查你的账号密码。账号错误\n详细信息：" + resultCheck.ErrorMessage, "错误");
                                break;
                            case ErrorType.UncompressingFailed:
                                MessageBox.Show("可能的多开或文件损坏，请确认文件完整且不要多开\n如果你不是多开游戏的话，请检查libraries文件夹是否完整\n详细信息：" + resultCheck.ErrorMessage, "可能的多开或文件损坏");
                                break;
                            default:
                                MessageBox.Show(resultCheck.ErrorMessage + "\n" +
                                (resultCheck.Exception == null ? string.Empty : resultCheck.Exception.StackTrace), "启动错误，请将此窗口截图向开发者寻求帮助");
                                break;
                        }
                        LaunchBtn.Content = "启动失败";
                    }
                }
            }));

        }

        private void ReleaseMemoryBtn_Click(object sender, RoutedEventArgs e)
        {
            SystemTools st = new SystemTools();
            st.ClearRAM();

            DataBase DB = new DataBase();
            MemoryText.Text = DB.AutoSetMemory();

            Settings.Default["Memory"] = MemoryText.Text;
            Settings.Default.Save();
        }
    }
}
