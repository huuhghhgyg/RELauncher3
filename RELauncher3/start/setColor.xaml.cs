using MahApps.Metro.Controls.Dialogs;
using RELauncher3.Properties;
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

namespace RELauncher3.start
{
    /// <summary>
    /// setColor.xaml 的交互逻辑
    /// </summary>
    public partial class setColor : Grid
    {
        public setColor()
        {
            InitializeComponent();
            comboBox.Items.Add("Red");
            comboBox.Items.Add("Green");
            comboBox.Items.Add("Blue");
            comboBox.Items.Add("Purple");
            comboBox.Items.Add("Orange");
            comboBox.Items.Add("Lime");
            comboBox.Items.Add("Teal");
            comboBox.Items.Add("Cyan");
            comboBox.Items.Add("Cobalt");
            comboBox.Items.Add("Indigo");
            comboBox.Items.Add("Violet");
            comboBox.Items.Add("Pink");
            comboBox.Items.Add("Magenta");
            comboBox.Items.Add("Crimson");
            comboBox.Items.Add("Amber");
            comboBox.Items.Add("Yellow");
            comboBox.Items.Add("Brown");
            comboBox.Items.Add("Olive");
            comboBox.Items.Add("Steel");
            comboBox.Items.Add("Mauve");
            comboBox.Items.Add("Taupe");
            comboBox.Items.Add("Sienna");
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow.changeColor(comboBox.SelectedItem as string, "BaseLight");
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedItem == null)
            {
                tips.IsOpen = true;
            }
            else
            {
                Settings.Default["ThemeColor"] = comboBox.SelectedItem.ToString();
                Settings.Default.Save();

                Grid grid;
                grid = new setLaunchProp();
                showGrid.Children.Clear();
                showGrid.Children.Add(grid);
            }
        }
    }
}
