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
    /// Interaction logic for LevelView.xaml
    /// </summary>
    public partial class LevelView : Window
    {
        GameData data;
        public PositionInGrid choosenPosition;

        public LevelView(GameData data)
        {
            InitializeComponent();

            this.data = data;

            refresh();
            resizeWindow(data.XGameArea, data.YGameArea);
        }
        private void refresh()
        {
            levelCanvas.Children.Clear();
            foreach (Arman_Class_Library.Block block in data.Blocks)
            {
                Rectangle detectorColor = null;
                Rectangle rect = new Rectangle();
                rect.Stroke = new SolidColorBrush(Colors.Black);
                if (block is Arman_Class_Library.Air)
                    rect.Fill = new SolidColorBrush(Colors.LightBlue);
                else if (block is Arman_Class_Library.Solid)
                    rect.Fill = new SolidColorBrush(Colors.Orange);
                else if (block is Arman_Class_Library.Detector)
                {
                    detectorColor = new Rectangle();
                    detectorColor.Width = data.OneBlockSize / 2;
                    detectorColor.Height = data.OneBlockSize / 2;
                    detectorColor.Fill = new SolidColorBrush(Color.FromScRgb((block as Arman_Class_Library.Detector).LockColor.A, (block as Arman_Class_Library.Detector).LockColor.R, (block as Arman_Class_Library.Detector).LockColor.G, (block as Arman_Class_Library.Detector).LockColor.B));
                    rect.Fill = new SolidColorBrush(Colors.Green);
                }
                else if (block is Arman_Class_Library.Home)
                    rect.Fill = new SolidColorBrush(Colors.DarkBlue);

                rect.Width = data.OneBlockSize;
                rect.Height = data.OneBlockSize;
                levelCanvas.Children.Add(rect);
                Canvas.SetLeft(rect, block.Position.X * data.OneBlockSize);
                Canvas.SetTop(rect, block.Position.Y * data.OneBlockSize);
                if (detectorColor != null)
                {
                    levelCanvas.Children.Add(detectorColor);
                    Canvas.SetLeft(detectorColor, block.Position.X * data.OneBlockSize + data.OneBlockSize / 4);
                    Canvas.SetTop(detectorColor, block.Position.Y * data.OneBlockSize + data.OneBlockSize / 4);
                }
            }
        }
        private void resizeWindow(int sizeX, int sizeY)
        {
            this.Width = (sizeX * data.OneBlockSize) + 20;
            this.Height = (sizeY * data.OneBlockSize) + 50;
        }

        private void levelCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition((Canvas)sender);
            choosenPosition = new PositionInGrid((int)point.X / data.OneBlockSize, (int)point.Y / data.OneBlockSize);
            this.Close();
        }
    }
}
