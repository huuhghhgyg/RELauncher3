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
using KMCCC.Tools;

namespace RELauncher3.Launcher
{
    /// <summary>
    /// settings.xaml 的交互逻辑
    /// </summary>
    public partial class settings : Grid
    {
        public settings()
        {
            InitializeComponent();
            LoadSettings();
        }

        void LoadSettings()
        {

            UserNameText.Text = Settings.Default["UserName"].ToString();
            PasswordBox.Password = Settings.Default["Password"].ToString();
            OnlineAccount.IsChecked = bool.Parse(Settings.Default["OnlineAccount"].ToString());
            TwitchLogin.IsChecked = bool.Parse(Settings.Default["TwitchLogin"].ToString());

            MemoryText.Text = Settings.Default["Memory"].ToString();
            JavaPathText.Text = Settings.Default["JavaPath"].ToString();
            AutoExitSwitch.IsChecked = bool.Parse(Settings.Default["AutoExit"].ToString());
            AutoSetMemorySwitch.IsChecked = bool.Parse(Settings.Default["AutoSetMemory"].ToString());

            AutoConnectSwitch.IsChecked = bool.Parse(Settings.Default["AutoConnectServer"].ToString());
            ServerIP.Text = Settings.Default["ServerIP"].ToString();
            ServerPort.Text = Settings.Default["ServerPort"].ToString();
            LaunchModeSelector.SelectedIndex = int.Parse(Settings.Default["LaunchMode"].ToString());

            //if (Settings.Default["GameWidth"].ToString() == "" || Settings.Default["GameHeight"].ToString() == "")
            //{
            //    Settings.Default["GameWidth"] = "1280";
            //    Settings.Default["GameHeight"] = "768";
            //    Settings.Default.Save();
            //}
            GameWidthText.Text = Settings.Default["GameWidth"].ToString();
            GameHeightText.Text = Settings.Default["GameHeight"].ToString();
        }

        void SaveSettings()
        {
            Settings.Default["UserName"] = UserNameText.Text;
            Settings.Default["Password"] = PasswordBox.Password;
            Settings.Default["OnlineAccount"] = OnlineAccount.IsChecked;
            Settings.Default["TwitchLogin"] = TwitchLogin.IsChecked;

            Settings.Default["Memory"] = MemoryText.Text;
            Settings.Default["JavaPath"] = JavaPathText.Text;
            Settings.Default["AutoExit"] = AutoExitSwitch.IsChecked;
            Settings.Default["AutoSetMemory"] = AutoSetMemorySwitch.IsChecked;

            Settings.Default["AutoConnectServer"] = AutoConnectSwitch.IsChecked;
            Settings.Default["ServerIP"] = ServerIP.Text;
            Settings.Default["ServerPort"] = ServerPort.Text;
            Settings.Default["LaunchMode"] = LaunchModeSelector.SelectedIndex;
            Settings.Default["GameWidth"] = GameWidthText.Text;
            Settings.Default["GameHeight"] = GameHeightText.Text;
        }

        private void BackTile_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();

            Grid grid;
            grid = new MainPage();
            showGrid.Children.Clear();
            showGrid.Children.Add(grid);
        }

        private void UserNameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Default["UserName"] = UserNameText.Text;
            Settings.Default.Save();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Settings.Default["Password"] = PasswordBox.Password;
            Settings.Default.Save();
        }

        private void MemoryText_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Default["Memory"] = MemoryText.Text;
            Settings.Default.Save();
        }

        private void JavaPathText_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Default["JavaPath"] = JavaPathText.Text;
            Settings.Default.Save();
        }

        private void ServerIP_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Default["ServerIP"] = ServerIP.Text;
            Settings.Default.Save();
        }

        private void ServerPort_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Default["ServerPort"] = ServerPort.Text;
            Settings.Default.Save();
        }

        private void AutoSetJava_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JavaPathText.Text = SystemTools.FindJava().Last();//textbox1显示我们找到的最后一个Java（也是最近安装的一个）
            }
            catch
            {
                tips.IsOpen = true;
            }
        }

        private void AutoSetMemorySwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (AutoConnectSwitch.IsChecked == true)
            {
                DataBase DB = new DataBase();
                MemoryText.Text = DB.AutoSetMemory();
                SaveSettings();
            }
        }
    }
}
