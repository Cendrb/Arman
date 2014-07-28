using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Arman_Class_Library;
using System.Diagnostics;
using Microsoft.Win32;
using Microsoft.Xna.Framework.Input;

namespace Level_Editor
{
    public enum Tools { solid, air, detector, coin, home, mob, player, movable }
    public enum Objectives { collectAllCoins, activateDetectors }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataLoader dataLoader;
        private Tools choosenTool;
        private Tools ChoosenTool
        {
            get
            {
                return choosenTool;
            }
            set
            {
                solidBlockButton.IsEnabled = true;
                airBlockButton.IsEnabled = true;
                movableBlockButton.IsEnabled = true;
                detectorButton.IsEnabled = true;
                mobBlockButton.IsEnabled = true;
                coinBlockButton.IsEnabled = true;
                playerBlockButton.IsEnabled = true;
                homeBlockButton.IsEnabled = true;
                switch (value)
                {
                    case Tools.air:
                        dynamicToolLabel.Content = "Place air";
                        airBlockButton.IsEnabled = false;
                        toolInfo.Text = "Layer blocks\nTransparent\nFully transparent block and completely useless :)";
                        break;
                    case Tools.coin:
                        dynamicToolLabel.Content = "Place coin";
                        coinBlockButton.IsEnabled = false;
                        toolInfo.Text = "Layer coins\nTransparent\nCollectible\nBasic coin or \"point\" which can be collected by players.";
                        break;
                    case Tools.detector:
                        dynamicToolLabel.Content = "Place detector";
                        detectorButton.IsEnabled = false;
                        toolInfo.Text = "Layer blocks\nTransparent\nCould be activated by movable block with same color. White detector is universal and can be activated by any movable block.\nCould be added to objectives or it could remotely replace any solid block with air block.";
                        break;
                    case Tools.home:
                        dynamicToolLabel.Content = "Place home";
                        homeBlockButton.IsEnabled = false;
                        toolInfo.Text = "Layer blocks\nTransparent\nCould act as an objective to win game.\nActivated by player standing on it.";
                        break;
                    case Tools.mob:
                        dynamicToolLabel.Content = "Spawn mob";
                        mobBlockButton.IsEnabled = false;
                        toolInfo.Text = "Layer entities\nSolid\nKills players\n";
                        break;
                    case Tools.movable:
                        dynamicToolLabel.Content = "Spawn movable block";
                        movableBlockButton.IsEnabled = false;
                        toolInfo.Text = "Layer entities\nSolid\nUsed to activate detectors. If key color is set to white, it will act as universal key.";
                        break;
                    case Tools.player:
                        dynamicToolLabel.Content = "Spawn player";
                        playerBlockButton.IsEnabled = false;
                        toolInfo.Text = "Layer entities\nSolid\n";
                        break;
                    case Tools.solid:
                        dynamicToolLabel.Content = "Place solid block";
                        solidBlockButton.IsEnabled = false;
                        toolInfo.Text = "Layer blocks\nSolid\nFully solid block.\nCould be removed using detector.";
                        break;
                }
                choosenTool = value;
            }
        }
        public GameData data;

        private PositionInGrid firstPosition;
        private PositionInGrid secondPosition;
        private bool blocksEnabled, entitiesEnabled = false;
        private bool changesSaved = true;
        private bool ctrlPressed, zPressed = false;
        private Stack<GameData> dataStack;

        public MainWindow()
        {
            InitializeComponent();
            data = new GameData();
            dataStack = new Stack<GameData>();
            data.OneBlockSize = 20;
            firstPosition = new PositionInGrid(0);
            secondPosition = new PositionInGrid(0);

            blocksLayerCheckBox.IsChecked = true;
            entitiesLayerCheckBox.IsChecked = true;

            activateDetectors.IsChecked = true;
            collectCoins.IsChecked = false;
            getHome.IsChecked = false;

            disableControlsForLevel();
        }

        #region Tools changing
        private void solidBlockButton_Click(object sender, RoutedEventArgs e)
        {
            ChoosenTool = Tools.solid;
        }
        private void airBlockButton_Click(object sender, RoutedEventArgs e)
        {
            ChoosenTool = Tools.air;
        }
        private void movableBlockButton_Click(object sender, RoutedEventArgs e)
        {
            ChoosenTool = Tools.movable;
        }
        private void detectorButton_Click(object sender, RoutedEventArgs e)
        {
            ChoosenTool = Tools.detector;
        }
        private void mobBlockButton_Click(object sender, RoutedEventArgs e)
        {
            ChoosenTool = Tools.mob;
        }
        private void coinBlockButton_Click(object sender, RoutedEventArgs e)
        {
            ChoosenTool = Tools.coin;
        }
        private void playerBlockButton_Click(object sender, RoutedEventArgs e)
        {
            ChoosenTool = Tools.player;
        }
        private void homeBlockButton_Click(object sender, RoutedEventArgs e)
        {
            ChoosenTool = Tools.home;
        }
        #endregion

        #region Enabling and disabling controls
        private void disableControlsForLevel()
        {
            levelCanvas.IsEnabled = false;
            // Menu
            saveMenu.IsEnabled = false;
            editMenu.IsEnabled = false;
            // Buttons (tools)
            solidBlockButton.IsEnabled = false;
            airBlockButton.IsEnabled = false;
            movableBlockButton.IsEnabled = false;
            detectorButton.IsEnabled = false;
            mobBlockButton.IsEnabled = false;
            coinBlockButton.IsEnabled = false;
            playerBlockButton.IsEnabled = false;
            homeBlockButton.IsEnabled = false;
            // Objectives
            activateDetectors.IsEnabled = false;
            collectCoins.IsEnabled = false;
            getHome.IsEnabled = false;
            // Layers checkboxes
            blocksLayerCheckBox.IsEnabled = false;
            entitiesLayerCheckBox.IsEnabled = false;
        }
        private void enableControlsForLevel()
        {
            levelCanvas.IsEnabled = true;
            // Menu
            saveMenu.IsEnabled = true;
            editMenu.IsEnabled = true;
            // Buttons (tools)
            solidBlockButton.IsEnabled = true;
            airBlockButton.IsEnabled = true;
            movableBlockButton.IsEnabled = true;
            detectorButton.IsEnabled = true;
            mobBlockButton.IsEnabled = true;
            coinBlockButton.IsEnabled = true;
            playerBlockButton.IsEnabled = true;
            homeBlockButton.IsEnabled = true;
            // Objectives
            activateDetectors.IsEnabled = true;
            collectCoins.IsEnabled = true;
            getHome.IsEnabled = true;
            // Layers checkboxes
            blocksLayerCheckBox.IsEnabled = true;
            entitiesLayerCheckBox.IsEnabled = true;

            ChoosenTool = Tools.solid;
        }
        #endregion

        #region Adding blocks
        private void levelCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition((Canvas)sender);
            PositionInGrid position = new PositionInGrid((int)point.X / data.OneBlockSize, (int)point.Y / data.OneBlockSize);

            firstPosition = position;
        }
        private void levelCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            dataStack.Push(data.Clone());
            Point point = e.GetPosition((Canvas)sender);
            PositionInGrid position2 = new PositionInGrid((int)point.X / data.OneBlockSize, (int)point.Y / data.OneBlockSize);

            secondPosition = position2;


            // Setblocks
            DoActionForEveryPositionInRectangle(firstPosition, secondPosition, delegate(PositionInGrid position)
            {
                switch (ChoosenTool)
                {
                    case Tools.air:
                        #region Air
                        foreach (Block block in data.Blocks.ToList())
                        {
                            if (block.Position == position)
                                data.Blocks.Remove(block);
                        }
                        data.Blocks.Add(new Air(position));
                        #endregion
                        break;
                    case Tools.coin:
                        #region Coin
                        foreach (Block block in data.Blocks.ToList())
                        {
                            if (block.Position == position)
                                data.Blocks.Remove(block);
                        }
                        Windows.Coin coinWindow = new Windows.Coin();
                        coinWindow.ShowDialog();
                        try
                        {
                            data.Blocks.Add(new Coin(position, coinWindow.blastResistance.Value.Value, coinWindow.valueUpDown.Value.Value));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error while parsing data from form  - " + ex.Message, "Error");
                        }
                        refresh();
                        #endregion
                        break;
                    case Tools.detector:
                        #region Detector
                        foreach (Block block in data.Blocks.ToList())
                        {
                            if (block.Position == position)
                                data.Blocks.Remove(block);
                        }

                        // prepare variables
                        Color color;
                        bool blockMovableBlockOnApproach, addToObjectives;

                        Windows.Detector detector = new Windows.Detector(data);
                        detector.ShowDialog();
                        try
                        {
                            blockMovableBlockOnApproach = detector.blockPlacedMovableBlockCheckBox.IsChecked.Value;
                            color = detector.colorPicker.SelectedColor;
                            addToObjectives = detector.objectives.IsChecked.Value;
                            if (detector.removeBlock.IsChecked == true)
                                data.Blocks.Add(new Detector(position, 69.0, ArmanColor.ParseWindowsColor(color), blockMovableBlockOnApproach, addToObjectives, detector.blockToRemove));
                            else
                                data.Blocks.Add(new Detector(position, 69.0, ArmanColor.ParseWindowsColor(color), blockMovableBlockOnApproach, addToObjectives, new PositionInGrid(-1)));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error while parsing data from form  - " + ex.Message, "Error");
                        }
                        refresh();
                        #endregion
                        break;
                    case Tools.home:
                        #region Home
                        foreach (Block block in data.Blocks.ToList())
                        {
                            if (block.Position == position)
                                data.Blocks.Remove(block);
                        }

                        data.Blocks.Add(new Home(position, 69.0));
                        #endregion
                        break;
                    case Tools.mob:
                        #region Mob
                        foreach (Entity entity in data.Entities.ToList())
                        {
                            if (entity.Position == position)
                                data.Entities.Remove(entity);
                        }

                        // Prepare data
                        Windows.Mob mob = new Windows.Mob();
                        mob.speedLabel.ToolTip = "Set speed of mob (-Infinity - " + data.OneBlockSize + ")";
                        mob.speed.ToolTip = "Set speed of mob (-Infinity - " + data.OneBlockSize + ")";
                        mob.ShowDialog();
                        try
                        {
                            data.Entities.Add(new Mob(position, mob.nameTextBox.Text, mob.canPushCheckBox.IsChecked.Value, mob.canBePushedCheckBox.IsChecked.Value, (float)mob.speed.Value.Value, mob.collides.IsChecked.Value, mob.health.Value.Value, mob.invulnerable.IsChecked.Value, mob.vision.Value.Value, mob.wanderRange.Value.Value, mob.wanderChance.Value.Value));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error while parsing data from form  - " + ex.Message, "Error");
                        }
                        refresh();
                        #endregion
                        break;
                    case Tools.movable:
                        #region Movable Block
                        foreach (Entity entity in data.Entities)
                        {
                            if (entity.Position == position)
                                data.Entities.Remove(entity);
                        }

                        // Prepare data
                        Windows.MovableBlock mo = new Windows.MovableBlock();
                        mo.ShowDialog();

                        try
                        {
                            data.Entities.Add(new MovableBlock(position, mo.nameTextBox.Text, mo.canPushCheckBox.IsChecked.Value, mo.canBePushedCheckBox.IsChecked.Value, ArmanColor.ParseWindowsColor(mo.colorPicker.SelectedColor)));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error while parsing data from form  - " + ex.Message, "Error");
                        }
                        refresh();
                        #endregion
                        break;
                    case Tools.player:
                        #region Player
                        foreach (Entity entity in data.Entities.ToList())
                        {
                            if (entity.Position == position)
                                data.Entities.Remove(entity);
                        }

                        // Prepare data
                        Windows.Player player = new Windows.Player();
                        player.speedLabel.ToolTip = "Set speed of player (-Infinity - " + data.OneBlockSize + ")";
                        player.speed.ToolTip = "Set speed of player (-Infinity - " + data.OneBlockSize + ")";
                        player.ShowDialog();
                        try
                        {
                            data.Entities.Add(new Player(position, player.nameTextBox.Text, player.canPushCheckBox.IsChecked.Value, player.canBePushedCheckBox.IsChecked.Value, (float)player.speed.Value, true, 69.0, (bool)player.invulnerable.IsChecked,
                                new Controls(player.upText.Text, player.downText.Text, player.leftText.Text, player.rightText.Text), (int)player.lives.Value));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error while parsing data from form  - " + ex.Message, "Error");
                        }
                        refresh();
                        #endregion
                        break;
                    case Tools.solid:
                        #region Solid

                        foreach (Block block in data.Blocks.ToList())
                        {
                            if (block.Position == position)
                                data.Blocks.Remove(block);
                        }
                        data.Blocks.Add(new Solid(position, 69.0));
                        #endregion
                        break;
                }
            });
            refresh();
            changesSaved = false;
        }
        #endregion
        #region Removing blocks
        private void levelCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition((Canvas)sender);
            PositionInGrid position = new PositionInGrid((int)point.X / data.OneBlockSize, (int)point.Y / data.OneBlockSize);
            firstPosition = position;
        }
        private void levelCanvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            dataStack.Push(data.Clone());
            // Příprava na následující akci
            blocksEnabled = (bool)blocksLayerCheckBox.IsChecked;
            entitiesEnabled = (bool)entitiesLayerCheckBox.IsChecked;

            Point point = e.GetPosition((Canvas)sender);
            PositionInGrid position2 = new PositionInGrid((int)point.X / data.OneBlockSize, (int)point.Y / data.OneBlockSize);
            secondPosition = position2;

            DoActionForEveryPositionInRectangle(firstPosition, secondPosition, delegate(PositionInGrid position)
            {
                if (blocksEnabled)
                {
                    foreach (Block block in data.Blocks.ToList())
                    {
                        if (block.Position == position && !(block is Air))
                        {
                            data.Blocks.Add(new Air(position));
                            data.Blocks.Remove(block);
                        }
                    }
                }
                if (entitiesEnabled)
                {
                    foreach (Entity entity in data.Entities.ToList())
                    {
                        if (entity.Position == position)
                            data.Entities.Remove(entity);
                    }
                }
            });
            refresh();
            changesSaved = false;
        }
        #endregion

        #region Layers checkboxes
        private void layerCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            refresh();
        }
        private void layerCheckbox_UnChecked(object sender, RoutedEventArgs e)
        {
            refresh();
        }
        #endregion

        #region Menu
        private void newMenu_Click(object sender, RoutedEventArgs e)
        {
            Windows.Start start = new Windows.Start();
            start.ShowDialog();
            data.XGameArea = (int)start.xUpDown.Value;
            data.YGameArea = (int)start.yUpDown.Value;
            data.Name = start.nameTextBox.Text;
            data.Speed = (int)start.speed.Value;
            this.Title = "Arman Level Editor - " + data.Name;
            data.Blocks.Clear();
            data.Entities.Clear();
            generateAirAndResizeWindow(data.XGameArea, data.YGameArea);
            enableControlsForLevel();
            changesSaved = false;
        }
        private void loadMenu_Click(object sender, RoutedEventArgs e)
        {
            if (!changesSaved)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save changes?", "Unsaved changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        if (save())
                            load();
                        break;
                    case MessageBoxResult.No:
                        load();
                        break;
                }
            }
            else
                load();


        }
        private void saveMenu_Click(object sender, RoutedEventArgs e)
        {
            save();
        }
        private void editMenu_Click(object sender, RoutedEventArgs e)
        {
            dataStack.Push(data.Clone());
            Windows.Start start = new Windows.Start();
            start.speed.Value = data.Speed;
            start.nameTextBox.Text = data.Name;
            start.xUpDown.Value = data.XGameArea;
            start.yUpDown.Value = data.YGameArea;
            start.xUpDown.IsEnabled = false;
            start.yUpDown.IsEnabled = false;

            start.ShowDialog();

            data.XGameArea = (int)start.xUpDown.Value;
            data.YGameArea = (int)start.yUpDown.Value;
            data.Name = start.nameTextBox.Text;
            data.Speed = (int)start.speed.Value;

            changesSaved = false;
        }
        private void exitMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Keys detection
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
                ctrlPressed = true;
            else if (e.Key == Key.Z)
                zPressed = true;
            if (ctrlPressed && zPressed)
            {
                undo();
            }
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
                ctrlPressed = false;
            else if (e.Key == Key.Z)
                zPressed = false;
        }
        #endregion

        #region Objectives
        private void activateDetectors_Checked(object sender, RoutedEventArgs e)
        {
            data.Objectives.ActivateDetectors = true;
        }

        private void activateDetectors_Unchecked(object sender, RoutedEventArgs e)
        {
            data.Objectives.ActivateDetectors = false;
            if (!verifyObjectives())
            {
                (sender as CheckBox).IsChecked = true;
                MessageBox.Show("You must select at least one objective!", "No objectives", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void collectCoins_Checked(object sender, RoutedEventArgs e)
        {
            data.Objectives.CollectAllCoins = true;
        }

        private void collectCoins_Unchecked(object sender, RoutedEventArgs e)
        {
            data.Objectives.CollectAllCoins = false;
            if (!verifyObjectives())
            {
                (sender as CheckBox).IsChecked = true;
                MessageBox.Show("You must select at least one objective!", "No objectives", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }


        private void getHome_Checked(object sender, RoutedEventArgs e)
        {
            data.Objectives.GetHome = true;
        }

        private void getHome_Unchecked(object sender, RoutedEventArgs e)
        {
            data.Objectives.GetHome = false;
            if (!verifyObjectives())
            {
                (sender as CheckBox).IsChecked = true;
                MessageBox.Show("You must select at least one objective!", "No objectives", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private bool verifyObjectives()
        {
            return !(!(bool)activateDetectors.IsChecked && !(bool)collectCoins.IsChecked && !(bool)getHome.IsChecked);
        }
        #endregion

        private void addObjectiveButton_Click(object sender, RoutedEventArgs e)
        {
            dataStack.Push(data.Clone());
        }

        private void DoActionForEveryPositionInRectangle(PositionInGrid firstPosition, PositionInGrid secondPosition, Action<PositionInGrid> action)
        {
            List<PositionInGrid> positions = new List<PositionInGrid>();

            if (firstPosition.Y <= secondPosition.Y)
            {
                for (int y = firstPosition.Y; y <= secondPosition.Y; y++)
                    if (firstPosition.X <= secondPosition.X)
                        for (int x = firstPosition.X; x <= secondPosition.X; x++)
                            positions.Add(new PositionInGrid(x, y));
                    else
                        for (int x = firstPosition.X; x >= secondPosition.X; x--)
                            positions.Add(new PositionInGrid(x, y));
            }
            else
            {
                for (int y = firstPosition.Y; y >= secondPosition.Y; y--)
                    if (firstPosition.X <= secondPosition.X)
                        for (int x = firstPosition.X; x <= secondPosition.X; x++)
                            positions.Add(new PositionInGrid(x, y));
                    else
                        for (int x = firstPosition.X; x >= secondPosition.X; x--)
                            positions.Add(new PositionInGrid(x, y));
            }
            foreach (PositionInGrid pos in positions)
                action(pos);
        }

        // Renders the whole level
        private void refresh()
        {
            levelCanvas.Children.Clear();
            if ((bool)blocksLayerCheckBox.IsChecked)
                foreach (Block block in data.Blocks)
                {
                    Rectangle detectorColor = null;
                    Rectangle rect = new Rectangle();
                    rect.Stroke = new SolidColorBrush(Colors.Black);
                    if (block is Air)
                        rect.Fill = new SolidColorBrush(Colors.LightBlue);
                    else if (block is Solid)
                        rect.Fill = new SolidColorBrush(Colors.Orange);
                    else if (block is Detector)
                    {
                        detectorColor = new Rectangle();
                        detectorColor.Width = data.OneBlockSize / 2;
                        detectorColor.Height = data.OneBlockSize / 2;
                        detectorColor.Fill = new SolidColorBrush(Color.FromScRgb((block as Detector).LockColor.A, (block as Detector).LockColor.R, (block as Detector).LockColor.G, (block as Detector).LockColor.B));
                        rect.Fill = new SolidColorBrush(Colors.Green);
                    }
                    else if (block is Home)
                        rect.Fill = new SolidColorBrush(Colors.DarkBlue);
                    else if (block is Coin)
                    {
                        rect.Stroke = new SolidColorBrush(Colors.OrangeRed);
                        rect.Fill = new SolidColorBrush(Colors.Yellow);
                    }

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
            if ((bool)entitiesLayerCheckBox.IsChecked)
                foreach (Entity entity in data.Entities)
                {
                    Rectangle mBColor = null;
                    Rectangle rect = new Rectangle();
                    rect.Width = data.OneBlockSize;
                    rect.Height = data.OneBlockSize;
                    rect.Stroke = new SolidColorBrush(Colors.DarkRed);
                    if (entity is Mob)
                        rect.Fill = new SolidColorBrush(Colors.Red);
                    else if (entity is MovableBlock)
                    {
                        mBColor = new Rectangle();
                        mBColor.Width = data.OneBlockSize / 2;
                        mBColor.Height = data.OneBlockSize / 2;
                        mBColor.Fill = new SolidColorBrush(Color.FromScRgb((entity as MovableBlock).KeyColor.A, (entity as MovableBlock).KeyColor.R, (entity as MovableBlock).KeyColor.G, (entity as MovableBlock).KeyColor.B));
                        rect.Fill = new SolidColorBrush(Colors.Purple);
                    }
                    else if (entity is Player)
                        rect.Fill = new SolidColorBrush(Colors.YellowGreen);
                    levelCanvas.Children.Add(rect);
                    Canvas.SetLeft(rect, entity.Position.X * data.OneBlockSize);
                    Canvas.SetTop(rect, entity.Position.Y * data.OneBlockSize);
                    if (mBColor != null)
                    {
                        levelCanvas.Children.Add(mBColor);
                        Canvas.SetLeft(mBColor, entity.Position.X * data.OneBlockSize + data.OneBlockSize / 4);
                        Canvas.SetTop(mBColor, entity.Position.Y * data.OneBlockSize + data.OneBlockSize / 4);
                    }
                }
        }

        private bool load()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Arman level files (*.alvl)|*.alvl|All files (*.*)|*.*";
            dialog.Title = "Choose level file to load";
            string fileName = String.Empty;
            if (dialog.ShowDialog().Value)
                fileName = dialog.FileName;
            if (fileName != String.Empty)
            {
                dataLoader = new DataLoader(fileName);
                data = dataLoader.ReadData(true);
                resizeWindow(data.XGameArea, data.YGameArea);
                refresh();
                this.Title = "Arman Level Editor - " + data.Name;
                changesSaved = true;
                enableControlsForLevel();
                activateDetectors.IsChecked = data.Objectives.ActivateDetectors;
                collectCoins.IsChecked = data.Objectives.CollectAllCoins;
                getHome.IsChecked = data.Objectives.GetHome;
                return true;
            }
            return false;
        }
        private bool save()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Arman level files (*.alvl)|*.alvl|All files (*.*)|*.*";
            dialog.Title = "Choose level file to save";
            dialog.FileName = data.Name + ".alvl";
            string fileName = String.Empty;
            if ((bool)dialog.ShowDialog())
                fileName = dialog.FileName;
            if (fileName != String.Empty)
            {
                dataLoader = new DataLoader(fileName);
                dataLoader.SaveData(data);
                changesSaved = true;
                return true;
            }
            return false;
        }
        private void undo()
        {
            try
            {
                data = dataStack.Pop();
            }
            catch (InvalidOperationException e)
            {
                changesSaved = true;
                MessageBox.Show("Nothing left to undo!", "Undo not possible");
            }
            zPressed = false;
            refresh();
        }

        private void generateAirAndResizeWindow(int sizeX, int sizeY)
        {
            for (int x = 0; x < sizeX; x++)
                for (int y = 0; y < sizeY; y++)
                {
                    data.Blocks.Add(new Air(new PositionInGrid(x, y)));
                }
            resizeWindow(sizeX, sizeY);
            refresh();
        }
        private void resizeWindow(int sizeX, int sizeY)
        {
            this.Width = (sizeX * data.OneBlockSize) + (1.8 / 8.0) * (sizeX * data.OneBlockSize);
            this.Height = (sizeY * data.OneBlockSize) + 105;
        }

        // Unsaved changes dialog
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!changesSaved)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save changes?", "Unsaved changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                switch (result)
                {
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                    case MessageBoxResult.Yes:
                        e.Cancel = true;
                        if (save())
                            e.Cancel = false;
                        break;
                }
            }
        }

    }
}
