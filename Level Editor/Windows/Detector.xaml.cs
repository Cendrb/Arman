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
using System.Windows.Shapes;
using Arman_Class_Library;

namespace Level_Editor.Windows
{
    /// <summary>
    /// Interaction logic for Detector.xaml
    /// </summary>
    public partial class Detector : Window
    {
        public PositionInGrid blockToRemove;
        private GameData data;

        public Detector(GameData data)
        {
            InitializeComponent();
            this.data = data;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void removeBlock_Checked(object sender, RoutedEventArgs e)
        {
            LevelView view = new LevelView(data);
            view.ShowDialog();
            blockToRemove = view.choosenPosition;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2;
            this.Top = mainWindow.Top + (mainWindow.Height - this.ActualHeight) / 2;
        }
    }
}
