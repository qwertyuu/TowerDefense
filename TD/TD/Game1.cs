using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Xml.Serialization;

//////////////////////////
//LEARN TO CODE GOD DAMMIT
//////////////////////////


namespace TD
{
    enum GameState { MainMenu, Options, InGame, Quit }
    enum ClickState { Clicked, Held, Releasing, Released }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static List<Texture2D> menuButtons;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch ui;
        MouseHandler mouse;
        KeyboardHandler keyboard;
        GameState gameState;
        InGameUI gameUi;
        MainMenu menu;
        Camera cam;
        Tower clippedToMouse;
        Texture2D[] towersText;
        Texture2D[] uiTextures;
        Texture2D cellT;
        public List<Tower> towerList;
        Cell[,] map; 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            towerList = new List<Tower>();
            mouse = new MouseHandler();
            keyboard = new KeyboardHandler();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ui = new SpriteBatch(GraphicsDevice);
            menuButtons = new List<Texture2D>();
            map = Cell.Parse("1.txt");
            cam = new Camera(map);
            menuButtons.Add(Content.Load<Texture2D>("PlayButton"));
            menuButtons.Add(Content.Load<Texture2D>("OptsButton"));

            cellT = Content.Load<Texture2D>("Cell");

            towersText = new Texture2D[10];
            towersText[0] = Content.Load<Texture2D>("Tower");

            uiTextures = new Texture2D[10];
            uiTextures[0] = Content.Load<Texture2D>("planUI");

            menu = new MainMenu(menuButtons);
            clippedToMouse = new Tower(Point.Zero, Tower.Types.type1, towersText[0]);

            gameUi = new InGameUI(uiTextures);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                keyboard.Update();
                mouse.Update(cam);
                switch (gameState)
                {
                    case GameState.MainMenu:
                        gameState = menu.UpdateGameState(mouse);
                        break;
                    case GameState.Options:
                        if (keyboard.pressedKeysList.Contains(Keys.Escape))
                            gameState = GameState.MainMenu;
                        break;
                    case GameState.InGame:
                        if (mouse.LeftClickState == ClickState.Clicked)
                            ClipTowersToCell(true);
                        if (mouse.RightClickState == ClickState.Clicked)
                            DeleteTower();
                        else
                            ClipTowersToCell(false);


                        if (keyboard.pressedKeysList.Contains(Keys.Escape))
                            gameState = GameState.MainMenu;
                        cam.Update(mouse, gameTime);
                        break;
                    case GameState.Quit:
                        Exit();
                        break;
                    default:
                        break;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            switch (gameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Begin();
                    menu.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
                case GameState.InGame:
                    spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, cam.viewMatrix);
                    ui.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
                    foreach (var item in map)
                    {
                        Color a = Color.White;
                        switch (item.type)
                        {
                            case Cell.CellTypes.Rock:
                                a = Color.Brown;
                                break;
                            case Cell.CellTypes.Turret:
                                a = Color.Green;
                                break;
                            default:
                                break;
                        }
                        spriteBatch.Draw(cellT, item.spacePos, a);
                    }
                    if (clippedToMouse != null)
                        clippedToMouse.Draw(spriteBatch, 0.5f);

                    Tower.PrintAllTowers(towerList, spriteBatch);
                    gameUi.Draw(ui);
                    spriteBatch.End();
                    ui.End();
                    break;
                case GameState.Options:
                    //Draw options
                    break;
            }
            base.Draw(gameTime);
        }

        private void ClipTowersToCell(bool click)
        {
            foreach (var item in map)
            {
                if (item.spacePos.Contains(mouse.fakePos))
                {
                    if (item.type == Cell.CellTypes.Turret)
                    {
                        if (click && item.contains == null)
                        {
                            Tower buf = new Tower(item.spacePos.Location, Tower.Types.type1, towersText[0]);
                            towerList.Add(buf);
                            item.contains = buf;
                        }
                        clippedToMouse = new Tower(item.spacePos.Location, Tower.Types.type1, towersText[0]);
                        break;
                    }
                    else
                    {
                        clippedToMouse = null;
                    }
                }
            }
        }

        private void DeleteTower()
        {
            foreach (var item in map)
            {
                if (item.spacePos.Contains(mouse.fakePos) && item.contains != null)
                {
                    towerList.Remove(item.contains);
                    item.contains = null;
                }
            }
        }
    }
}
