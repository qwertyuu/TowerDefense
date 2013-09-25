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
    class InGameUI 
    {
        public Texture2D[] textures;
        public Rectangle[] textBounds;
        static List<UIButtons> buttonList;
        static List<UIButtons> allButtons;
        List<Cell> cellsWithTower;
        const float transparencyLvl = 1.0f;

        public InGameUI(Texture2D[] listOfTexture, ref List<Cell> cellWithTower)
        {
            cellsWithTower = cellWithTower;
            textures = listOfTexture;
            textBounds = new Rectangle[textures.Length];
            textBounds[0] = new Rectangle(0, GraphicsDeviceManager.DefaultBackBufferHeight - (textures[0].Height + 2), textures[0].Width, textures[0].Height + 2);
            buttonList = new List<UIButtons>();
            allButtons = new List<UIButtons>();

            UIButtons add = new UIButtons();
            add.icon = listOfTexture[2];
            add.couleur = Color.White;
            add.returnState = InGameState.Add;
            add.function = UIButtonFunction.Add;
            add.spacePos = new Rectangle(15, 375, 40, 40);
            add.Clic += add_Clic;
            allButtons.Add(add);

            UIButtons upgrade = new UIButtons();
            upgrade.icon = listOfTexture[3];
            upgrade.couleur = Color.White;
            upgrade.returnState = InGameState.Upgrade;
            upgrade.function = UIButtonFunction.Upgrade;
            upgrade.spacePos = new Rectangle(15, 425, 40, 40);
            upgrade.Clic += upgrade_Clic;
            allButtons.Add(upgrade);
            SetButtons();

            UIButtons sell = new UIButtons();
            sell.icon = listOfTexture[1];
            sell.couleur = Color.White;
            sell.spacePos = new Rectangle(65, 425, 40, 40);
            sell.returnState = InGameState.Play;
            sell.function = UIButtonFunction.Sell;
            sell.doAnimation = false;
            sell.Clic += sell_Clic;
            allButtons.Add(sell);

        }

        void upgrade_Clic(object sender, EventArgs e)
        {
            Tower.currentTower.levelUp();
        }

        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(textures[0], textBounds[0], Color.White * transparencyLvl);
            foreach (var item in buttonList)
            {
                item.Draw(sprite);
            }
        }

        void sell_Clic(object sender, EventArgs e)
        {
            var sellBuffer = cellsWithTower.Find(bk => bk.contains == Tower.currentTower);
            cellsWithTower.Remove(sellBuffer);
            foreach (var item in Map.map)
            {
                if (item == sellBuffer)
                {
                    item.contains = null;
                    break;
                }
            }
            Tower.currentTower = null;
        }

        void add_Clic(object sender, EventArgs e)
        {
            Game1.inGameState = InGameState.Add;
            Tower.currentTower = null;
        }

        internal InGameState Update(InGameState current, MouseHandler mouse)
        {
            foreach (var item in buttonList)
            {
                if (item.spacePos.Contains(mouse.position))
                {
                    if (mouse.LeftClickState == ClickState.Clicked)
                    {
                        item.Clicked();
                        return item.returnState;
                    }
                    else if (mouse.LeftClickState == ClickState.Releasing)
                    {
                        item.Released();
                    }
                }
                else
                {
                    item.Released();
                }
            }
            return current;
        }

        internal static void SetButtons(Tower tower)
        {
            List<UIButtons> buf = new List<UIButtons>();
            foreach (var item in tower.neededFunctions)
            {
                buf.Add(allButtons.Find(bk => bk.function == item));
            }
            buttonList = buf;
        }

        internal static void SetButtons()
        {
            buttonList = allButtons.Where(bk => bk.returnState == InGameState.Add).ToList();
        }
    }
}
