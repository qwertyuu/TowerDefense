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

namespace TD
{

    class MainMenu
    {
        public GameState gameState { get; set; }
        List<Buttons> buttons;
        public MainMenu(List<Texture2D> textures)
        {
            gameState = GameState.MainMenu;
            buttons = new List<Buttons>();
            buttons.Add(new Buttons(new Rectangle(GraphicsDeviceManager.DefaultBackBufferWidth / 2 - 100, 150, 200, 40), textures[0], Color.White, GameState.InGame));
            buttons.Add(new Buttons(new Rectangle(GraphicsDeviceManager.DefaultBackBufferWidth / 2 - 100, buttons[0].spacePos.Y + 60, 200, 40), textures[1], Color.White, GameState.Options));
            buttons.Add(new Buttons(new Rectangle(GraphicsDeviceManager.DefaultBackBufferWidth / 2 - 100, buttons[0].spacePos.Y + 120, 200, 40), textures[0], Color.Orange, GameState.Quit));
        }

        public GameState UpdateGameState(MouseHandler mouse)
        {
            foreach (var item in buttons)
            {
                if (item.spacePos.Contains(mouse.position))
                {
                    item.Transparency = 0.5f;
                    if (mouse.LeftClickState == ClickState.Clicked)
                    {
                        return item.returnState;
                    }
                }
                else
                    item.Transparency = 1.0f;
            }
            return GameState.MainMenu;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in buttons)
            {
                spriteBatch.Draw(item.texture, item.spacePos, item.couleur * item.Transparency);
            }
        }
    }
    class Buttons
    {
        public Rectangle spacePos { get; set; }
        public Texture2D texture { get; set; }
        public Color couleur { get; set; }
        public float Transparency { get; set; }
        public GameState returnState { get; set; }
        public Buttons(Rectangle _spacePos, Texture2D _texture, Color _couleur, GameState _return)
        {
            spacePos = _spacePos;
            couleur = _couleur;
            texture = _texture;
            returnState = _return;
            Transparency = 1.0f;
        }
    }
}
