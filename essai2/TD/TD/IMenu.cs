#region Using Statements
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
#endregion

namespace TD
{
    class IMenu
    {

        protected List<Drawable> drawableList;
        public GameState gameState { get; set; }
        public GameState senderMenuState;
        public GameState Escape = GameState.None;
        public Vector2 centerPoint = new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth / 2, GraphicsDeviceManager.DefaultBackBufferHeight / 2);
        public static List<IMenu> lesMenus = new List<IMenu>();

        public virtual void EscapePressed()
        {

        }

        public IMenu()
        {
            drawableList = new List<Drawable>();
            lesMenus.Add(this);
        }

        protected void AddHeader(string name)
        {
            Drawable menu = new Drawable();
            menu.text = name;
            menu.font = Game1.font;
            menu.fontColor = Color.Black;
            menu.texture = Game1.cellT;
            menu.couleur = Color.White;
            menu.returnState = GameState.None;
            AddDrawable(menu);
        }

        public void AddDrawable(Drawable drawable)
        {
            drawableList.Add(drawable);
            for (int i = 0; i < drawableList.Count; i++)
            {
                Rectangle place = new Rectangle((int)centerPoint.X, (int)centerPoint.Y + (int)((i + ((drawableList.Count - 1) * -0.5)) * Buttons.variation), drawableList[i].spacePos.Width, drawableList[i].spacePos.Height);
                drawableList[i].spacePos = new Rectangle(place.X - place.Width / 2, place.Y - place.Height / 2, place.Width, place.Height);
                drawableList[i].textSpot = new Vector2(drawableList[i].textOffset.X + drawableList[i].spacePos.X, drawableList[i].textOffset.Y + drawableList[i].spacePos.Y);
            }
        }

        public static IMenu UpdateMenu(MouseHandler mouse, IMenu current, KeyboardHandler kB)
        {
            List<Drawable> currentMenuList = lesMenus[lesMenus.IndexOf(current)].drawableList;

            if (kB.pressedKeysList.Contains(Keys.Escape))
            {
                current.EscapePressed();
                return lesMenus.Find(bk => bk.gameState == current.Escape);
            }

            foreach (var item in currentMenuList)
            {
                if (item.Update(mouse, current))
                    return lesMenus.Find(bk => bk.gameState == item.returnState);
            }
            return current;
        }

        public static void Draw(SpriteBatch spriteBatch, IMenu current)
        {
            List<Drawable> currentMenuList = lesMenus.Find(bk => bk == current).drawableList;
            foreach (var item in currentMenuList)
                item.Draw(spriteBatch);
        }
    }
}
