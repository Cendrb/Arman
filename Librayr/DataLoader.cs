using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using CColor = System.Drawing.Color;

namespace Arman_Class_Library
{
    public class DataLoader
    {
        public string Path { get; private set; }
        private XmlDocument document;

        GameData data;

        private TexturesPaths app;

        public DataLoader(string path)
        {
            document = new XmlDocument();

            this.Path = path;
        }
        public GameData ReadData(bool forceReload)
        {
            if (forceReload || data == null)
            {
                data = loadData();
            }
            return data;
        }

        private GameData loadData()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GameData));
                TextReader textReader = new StreamReader(Path);
                return (GameData)serializer.Deserialize(textReader);
            }
            catch (XmlException e)
            {
                MessageBox.Show(String.Format("Game save ({0}) is corrupted and cannot be read. - {1}", Path, e.Message), "Loading error");
            }
            return null;
        }

        public void SaveData(GameData data)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GameData));
                XmlTextWriter writer = new XmlTextWriter(Path, Encoding.UTF8);
                serializer.Serialize(writer, data);
                writer.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Game file ({0}) could not be saved. - {1}", Path, e.Message), "Save error");
                MessageBox.Show(e.InnerException.Message);
                MessageBox.Show(e.InnerException.InnerException.Message);
            }
        }
    }
}
/*
            try
            {
                // Deklarace
                XmlDeclaration deklarace = document.CreateXmlDeclaration("1.0", "utf-8", null);
                document.AppendChild(deklarace);

                // Basic doc
                XmlElement arman = document.CreateElement("Arman");
                XmlElement settings = document.CreateElement("Settings");
                XmlElement items = document.CreateElement("Items");
                XmlElement objectives = document.CreateElement("Objectives");

                // Objectives
                #region Objectives
                objectives.SetAttribute("ActivateDetectors", data.Objectives.ActivateDetectors.ToString());
                objectives.SetAttribute("CollectAllCoins", data.Objectives.CollectAllCoins.ToString());
                objectives.SetAttribute("GetHome", data.Objectives.GetHome.ToString());

                arman.AppendChild(objectives);
                #endregion

                // Settings
                #region Settings
                settings.SetAttribute("XGameArea", data.XGameArea.ToString());
                settings.SetAttribute("YGameArea", data.YGameArea.ToString());
                settings.SetAttribute("Name", data.Name);
                settings.SetAttribute("Speed", data.Speed.ToString());
                settings.SetAttribute("OneBlockSize", data.OneBlockSize.ToString());

                arman.AppendChild(settings);
                #endregion

                // Entities
                #region Entities
                XmlElement entities = document.CreateElement("Entities");

                XmlElement mobs = document.CreateElement("Mobs");
                XmlElement players = document.CreateElement("Players");
                XmlElement movableBlocks = document.CreateElement("MovableBlocks");

                foreach (Entity entity in data.Entities)
                {
                    if (entity is Mob)
                    {
                        #region Mobs
                        Mob mob = entity as Mob;
                        XmlElement mobEl = document.CreateElement("Mob");
                        mobEl.SetAttribute("Name", mob.Name);
                        mobEl.SetAttribute("XCoord", mob.Position.X.ToString());
                        mobEl.SetAttribute("YCoord", mob.Position.Y.ToString());
                        mobEl.SetAttribute("CanPush", mob.CanPush.ToString());
                        mobEl.SetAttribute("CanBePushed", mob.CanBePushed.ToString());
                        mobEl.SetAttribute("Speed", mob.Speed.ToString());
                        mobEl.SetAttribute("Vision", mob.Vision.ToString());
                        mobEl.SetAttribute("MoveRatio", mob.MoveRatio.ToString());

                        mobs.AppendChild(mobEl);
                        #endregion
                    }
                    else if (entity is Player)
                    {
                        #region Players
                        Player player = entity as Player;
                        XmlElement playerEl = document.CreateElement("Player");
                        playerEl.SetAttribute("Name", player.Name);
                        playerEl.SetAttribute("XCoord", player.Position.X.ToString());
                        playerEl.SetAttribute("YCoord", player.Position.Y.ToString());
                        playerEl.SetAttribute("CanPush", player.CanPush.ToString());
                        playerEl.SetAttribute("CanBePushed", player.CanBePushed.ToString());
                        playerEl.SetAttribute("Speed", player.Speed.ToString());
                        playerEl.SetAttribute("Lives", player.Lives.ToString());
                        playerEl.SetAttribute("Invulnerable", player.IsInvulnerable.ToString());
                        playerEl.SetAttribute("Controls", String.Join(",", player.Controls.up, player.Controls.down, player.Controls.left, player.Controls.right));

                        players.AppendChild(playerEl);
                        #endregion
                    }
                    else if (entity is MovableBlock)
                    {
                        #region Movable blocks
                        MovableBlock movableBlock = entity as MovableBlock;
                        XmlElement mOEl = document.CreateElement("MB");
                        mOEl.SetAttribute("Name", movableBlock.Name);
                        mOEl.SetAttribute("XCoord", movableBlock.Position.X.ToString());
                        mOEl.SetAttribute("YCoord", movableBlock.Position.Y.ToString());
                        mOEl.SetAttribute("CanPush", movableBlock.CanPush.ToString());
                        mOEl.SetAttribute("CanBePushed", movableBlock.CanBePushed.ToString());
                        mOEl.SetAttribute("Speed", movableBlock.Speed.ToString());
                        mOEl.SetAttribute("R", movableBlock.KeyColor.R.ToString());
                        mOEl.SetAttribute("G", movableBlock.KeyColor.G.ToString());
                        mOEl.SetAttribute("B", movableBlock.KeyColor.B.ToString());
                        mOEl.SetAttribute("A", movableBlock.KeyColor.A.ToString());

                        movableBlocks.AppendChild(mOEl);
                        #endregion
                    }
                }
                entities.AppendChild(mobs);
                entities.AppendChild(players);
                entities.AppendChild(movableBlocks);

                items.AppendChild(entities);
                #endregion

                // Static Blocks
                #region Static Blocks
                XmlElement blocks = document.CreateElement("Blocks");

                XmlElement solidBlocks = document.CreateElement("SolidBlocks");
                XmlElement airBlocks = document.CreateElement("AirBlocks");
                XmlElement detectors = document.CreateElement("Detectors");
                XmlElement homes = document.CreateElement("Homes");

                foreach (Block block in data.Blocks)
                {
                    if (block is Solid)
                    {
                        #region Solid Blocks
                        Solid solid = block as Solid;
                        XmlElement solidEl = document.CreateElement("Solid");
                        solidEl.SetAttribute("XCoord", solid.Position.X.ToString());
                        solidEl.SetAttribute("YCoord", solid.Position.Y.ToString());

                        solidBlocks.AppendChild(solidEl);
                        #endregion
                    }
                    else if (block is Air)
                    {
                        #region Air Blocks
                        Air air = block as Air;
                        XmlElement airEl = document.CreateElement("Air");
                        airEl.SetAttribute("XCoord", air.Position.X.ToString());
                        airEl.SetAttribute("YCoord", air.Position.Y.ToString());

                        airBlocks.AppendChild(airEl);
                        #endregion
                    }
                    else if (block is Detector)
                    {
                        #region Detectors
                        Detector detector = block as Detector;
                        XmlElement detectorEl = document.CreateElement("Detector");
                        detectorEl.SetAttribute("XCoord", detector.Position.X.ToString());
                        detectorEl.SetAttribute("YCoord", detector.Position.Y.ToString());
                        detectorEl.SetAttribute("BlockDetector", detector.BlockMovableBlockOnApproach.ToString());
                        detectorEl.SetAttribute("Objectives", detector.IsPartOfObjectives.ToString());
                        detectorEl.SetAttribute("BlockToRemove", detector.AffectedPosition.ToString());
                        detectorEl.SetAttribute("R", detector.LockColor.R.ToString());
                        detectorEl.SetAttribute("G", detector.LockColor.G.ToString());
                        detectorEl.SetAttribute("B", detector.LockColor.B.ToString());
                        detectorEl.SetAttribute("A", detector.LockColor.A.ToString());

                        detectors.AppendChild(detectorEl);
                        #endregion
                    }
                    else if (block is Home)
                    {
                        #region Homes
                        Home home = block as Home;
                        XmlElement homeEl = document.CreateElement("Home");
                        homeEl.SetAttribute("XCoord", home.Position.X.ToString());
                        homeEl.SetAttribute("YCoord", home.Position.Y.ToString());

                        homes.AppendChild(homeEl);
                        #endregion
                    }
                }
                blocks.AppendChild(solidBlocks);
                blocks.AppendChild(airBlocks);
                blocks.AppendChild(detectors);
                blocks.AppendChild(homes);

                items.AppendChild(blocks);
                #endregion

                // Coins
                #region Coins
                XmlElement coins = document.CreateElement("Coins");

                foreach (Coin coin in data.Coins)
                {
                    #region Coins
                    XmlElement coinEl = document.CreateElement("Coin");
                    coinEl.SetAttribute("XCoord", coin.Position.X.ToString());
                    coinEl.SetAttribute("YCoord", coin.Position.Y.ToString());
                    coinEl.SetAttribute("Value", coin.Value.ToString());

                    coins.AppendChild(coinEl);
                    #endregion
                }
                items.AppendChild(coins);
                #endregion

                arman.AppendChild(items);

                document.AppendChild(arman);
                document.Save(Path);
            }*/
/*
                document.Load(Path);

                XmlElement arman = (XmlElement)document.GetElementsByTagName("Arman")[0];

                XmlElement settings = (XmlElement)arman.GetElementsByTagName("Settings")[0];
                XmlElement items = (XmlElement)arman.GetElementsByTagName("Items")[0];
                XmlElement objectives = (XmlElement)arman.GetElementsByTagName("Objectives")[0];

                // Units
                XmlElement entitiesElement = (XmlElement)items.GetElementsByTagName("Entities")[0];
                XmlElement blocksElement = (XmlElement)items.GetElementsByTagName("Blocks")[0];
                XmlElement coinsElement = (XmlElement)items.GetElementsByTagName("Coins")[0];

                // Entities
                XmlElement mobsElement = (XmlElement)entitiesElement.GetElementsByTagName("Mobs")[0];
                XmlElement playersElement = (XmlElement)entitiesElement.GetElementsByTagName("Players")[0];
                XmlElement movableBlocksElement = (XmlElement)entitiesElement.GetElementsByTagName("MovableBlocks")[0];

                // Static Blocks
                XmlElement solidBlocksElement = (XmlElement)blocksElement.GetElementsByTagName("SolidBlocks")[0];
                XmlElement airBlocksElement = (XmlElement)blocksElement.GetElementsByTagName("AirBlocks")[0];
                XmlElement detectorsElement = (XmlElement)blocksElement.GetElementsByTagName("Detectors")[0];
                XmlElement homesElement = (XmlElement)blocksElement.GetElementsByTagName("Homes")[0];

                // Entities load
                #region Players
                foreach (XmlNode playerNode in playersElement)
                {
                    XmlElement playerElement = playerNode as XmlElement;

                    string name = playerElement.GetAttribute("Name");
                    int x = int.Parse(playerElement.GetAttribute("XCoord"));
                    int y = int.Parse(playerElement.GetAttribute("YCoord"));
                    bool canPush = bool.Parse(playerElement.GetAttribute("CanPush"));
                    bool canBePushed = bool.Parse(playerElement.GetAttribute("CanBePushed"));
                    int speed = int.Parse(playerElement.GetAttribute("Speed"));
                    int lives = int.Parse(playerElement.GetAttribute("Lives"));
                    bool invulnerable = bool.Parse(playerElement.GetAttribute("Invulnerable"));
                    GameControls controls = GameControls.Parse(playerElement.GetAttribute("Controls"));

                    data.Entities.Add(new Player(game, new PositionInGrid(x, y), tools, canPush, canBePushed, name, speed, controls, lives, invulnerable));
                }
                #endregion
                #region Mobs
                foreach (XmlNode mobNode in mobsElement)
                {
                    XmlElement mobElement = mobNode as XmlElement;

                    string name = mobElement.GetAttribute("Name");
                    int x = int.Parse(mobElement.GetAttribute("XCoord"));
                    int y = int.Parse(mobElement.GetAttribute("YCoord"));
                    bool canPush = bool.Parse(mobElement.GetAttribute("CanPush"));
                    bool canBePushed = bool.Parse(mobElement.GetAttribute("CanBePushed"));
                    int speed = int.Parse(mobElement.GetAttribute("Speed"));
                    int vision = int.Parse(mobElement.GetAttribute("Vision"));
                    int moveRatio = int.Parse(mobElement.GetAttribute("MoveRatio"));

                    data.Entities.Add(new Mob(game, new PositionInGrid(x, y), tools, canPush, canBePushed, name, speed, vision, moveRatio));
                }
                #endregion
                #region Movable Blocks
                foreach (XmlNode movableBlockNode in movableBlocksElement)
                {
                    XmlElement movableBlockElement = movableBlockNode as XmlElement;

                    string name = movableBlockElement.GetAttribute("Name");
                    int x = int.Parse(movableBlockElement.GetAttribute("XCoord"));
                    int y = int.Parse(movableBlockElement.GetAttribute("YCoord"));
                    bool canPush = bool.Parse(movableBlockElement.GetAttribute("CanPush"));
                    bool canBePushed = bool.Parse(movableBlockElement.GetAttribute("CanBePushed"));
                    Color color = new Color(int.Parse(movableBlockElement.GetAttribute("R")), int.Parse(movableBlockElement.GetAttribute("G")), int.Parse(movableBlockElement.GetAttribute("B")), int.Parse(movableBlockElement.GetAttribute("A")));

                    data.Entities.Add(new MovableBlock(game, new PositionInGrid(x, y), tools, canPush, canBePushed, name, color));
                }
                #endregion

                // Static blocks load
                #region Solid Blocks
                foreach (XmlNode solidBlockNode in solidBlocksElement)
                {
                    XmlElement solidBlockElement = solidBlockNode as XmlElement;

                    int x = int.Parse(solidBlockElement.GetAttribute("XCoord"));
                    int y = int.Parse(solidBlockElement.GetAttribute("YCoord"));

                    data.Blocks.Add(new Solid(game, new PositionInGrid(x, y), tools));
                }
                #endregion
                #region Air Blocks
                foreach (XmlNode airBlockNode in airBlocksElement)
                {
                    XmlElement airBlockElement = airBlockNode as XmlElement;

                    int x = int.Parse(airBlockElement.GetAttribute("XCoord"));
                    int y = int.Parse(airBlockElement.GetAttribute("YCoord"));

                    data.Blocks.Add(new Air(game, new PositionInGrid(x, y), tools));
                }
                #endregion
                #region Detectors
                foreach (XmlNode detectorNode in detectorsElement)
                {
                    XmlElement detectorElement = detectorNode as XmlElement;

                    int x = int.Parse(detectorElement.GetAttribute("XCoord"));
                    int y = int.Parse(detectorElement.GetAttribute("YCoord"));
                    bool blockMovableBlockOnApproach = bool.Parse(detectorElement.GetAttribute("BlockDetector"));
                    bool addToObjectives = bool.Parse(detectorElement.GetAttribute("Objectives"));
                    PositionInGrid positionOfBlockToRemove = PositionInGrid.Parse(detectorElement.GetAttribute("BlockToRemove"));
                    // Parsing color
                    Color xnacolor = new Color(int.Parse(detectorElement.GetAttribute("R")), int.Parse(detectorElement.GetAttribute("G")), int.Parse(detectorElement.GetAttribute("B")), int.Parse(detectorElement.GetAttribute("A")));

                    if (positionOfBlockToRemove.X == -1 || positionOfBlockToRemove.Y == -1)
                        data.Blocks.Add(new Detector(game, new PositionInGrid(x, y), tools, xnacolor, blockMovableBlockOnApproach, addToObjectives));
                    else
                        data.Blocks.Add(new Detector(game, new PositionInGrid(x, y), tools, xnacolor, blockMovableBlockOnApproach, addToObjectives, positionOfBlockToRemove));
                }
                #endregion
                #region Homes
                foreach (XmlNode homeNode in homesElement)
                {
                    XmlElement homeElement = homeNode as XmlElement;

                    int x = int.Parse(homeElement.GetAttribute("XCoord"));
                    int y = int.Parse(homeElement.GetAttribute("YCoord"));

                    data.Blocks.Add(new Home(game, new PositionInGrid(x, y), tools));
                }
                #endregion

                // Coins load
                #region Coins
                foreach (XmlNode coinBlockNode in coinsElement)
                {
                    XmlElement coinElement = coinBlockNode as XmlElement;

                    int x = int.Parse(coinElement.GetAttribute("XCoord"));
                    int y = int.Parse(coinElement.GetAttribute("YCoord"));
                    int value = int.Parse(coinElement.GetAttribute("Value"));

                    data.Coins.Add(new Coin(game, new PositionInGrid(x, y), tools, value));
                }
                #endregion

                // Settings load
                #region Settings
                data.XGameArea = int.Parse(settings.GetAttribute("XGameArea"));
                data.YGameArea = int.Parse(settings.GetAttribute("YGameArea"));
                data.Name = settings.GetAttribute("Name");
                data.Speed = int.Parse(settings.GetAttribute("Speed"));
                data.OneBlockSize = int.Parse(settings.GetAttribute("OneBlockSize"));
                #endregion

                // Objectives load
                #region Objectives
                data.Objectives.ActivateDetectors = bool.Parse(objectives.GetAttribute("ActivateDetectors"));
                data.Objectives.CollectAllCoins = bool.Parse(objectives.GetAttribute("CollectAllCoins"));
                data.Objectives.GetHome = bool.Parse(objectives.GetAttribute("GetHome"));
                #endregion*/