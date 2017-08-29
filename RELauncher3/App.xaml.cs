using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using KMCCC.Launcher;
using System.Threading;

namespace RELauncher3
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static LauncherCore Core = LauncherCore.Create();

        public static WaitHandle ProgramStarted { get; internal set; }
    }
}
