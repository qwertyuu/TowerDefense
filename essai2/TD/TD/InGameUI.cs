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
using System.Text;
using TD.Towers;
#endregion

namespace TD
{
    class InGameUI 
    {
        public Texture2D[] textures;
        public Rectangle[] textBounds;
        static List<UIButtons> buttonList;
        static List<UIButtons> allButtons;
        static UIButtons selectedButton;
        public Vector2 infoPos { get; set; }
        StringBuilder sB;
        String goldCount;
        List<Cell> cellsWithTower;
        const float transparencyLvl = 1.0f;
        Vector2 goldInfo;

        public InGameUI(Texture2D[] listOfTexture, ref List<Cell> cellWithTower)
        {
            goldCount = Game1.gold.ToString();
            sB = new StringBuilder();
            cellsWithTower = cellWithTower;
            textures = listOfTexture;
            textBounds = new Rectangle[textures.Length];
            textBounds[0] = new Rectangle(0, GraphicsDeviceManager.DefaultBackBufferHeight - (textures[0].Height + 2), textures[0].Width, textures[0].Height + 2);
            buttonList = new List<UIButtons>();
            allButtons = new List<UIButtons>();

            UIButtons add = new UIButtons();
            add.icon = listOfTexture[2];
            add.Hotkey = Keys.A;
            add.couleur = Color.White;
            add.returnState = InGameState.Add;
            add.function = UIButtonFunction.Add;
            add.spacePos = new Rectangle(15, 375, 40, 40);
            add.Clic += add_Clic;
            allButtons.Add(add);

            UIButtons upgrade = new UIButtons();
            upgrade.icon = listOfTexture[3];
            upgrade.Hotkey = Keys.U;
            upgrade.couleur = Color.White;
            upgrade.returnState = InGameState.Play;
            upgrade.function = UIButtonFunction.Upgrade;
            upgrade.spacePos = new Rectangle(15, 425, 40, 40);
            upgrade.Clic += upgrade_Clic;
            allButtons.Add(upgrade);

            UIButtons sell = new UIButtons();
            sell.icon = listOfTexture[1];
            sell.Hotkey = Keys.S;
            sell.couleur = Color.White;
            sell.spacePos = new Rectangle(65, 425, 40, 40);
            sell.returnState = InGameState.Play;
            sell.function = UIButtonFunction.Sell;
            sell.doAnimation = false;
            sell.Clic += sell_Clic;
            allButtons.Add(sell);

            UIButtons towerType1 = new UIButtons();
            towerType1.icon = listOfTexture[1];
            towerType1.couleur = Color.White;
            towerType1.spacePos = add.spacePos;
            towerType1.returnState = InGameState.Play;
            towerType1.doAnimation = false;
            towerType1.Clic += addTower1;
            se

            SetButtons();

            infoPos = new Vector2(135, 375);
            goldInfo = new Vector2(325, 425);
        }

        void upgrade_Clic(object sender, EventArgs e)
        {
            if (Game1.SelectedObject.GetType() == typeof(Tower) && Game1.gold >= ((Tower)Game1.SelectedObject).upgradeCost)
            {
                ((Tower)Game1.SelectedObject).levelUp();
                Game1.gold -= (((Tower)Game1.SelectedObject).upgradeCost);
            }
        }

        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(textures[0], textBounds[0], Color.White * transparencyLvl);
            sprite.DrawString(Game1.font, sB, infoPos, Color.White);
            sprite.DrawString(Game1.font, goldCount, goldInfo, Color.White); 

            foreach (var item in buttonList)
            {
                if (item.function == UIButtonFunction.Upgrade && (((Tower)Game1.SelectedObject).level >= Tower.maxLevel))
                    continue;

                else
                    item.Draw(sprite);
            }
        }

        void sell_Clic(object sender, EventArgs e)
        {
            var sellBuffer = cellsWithTower.Find(bk => bk.contains == Game1.SelectedObject);
            cellsWithTower.Remove(sellBuffer);
            foreach (var item in Map.map)
            {
                if (item == sellBuffer)
                {
                    Game1.gold += (uint)(item.contains.cost + 150 * (item.contains.level - 1)); 
                    item.contains = null;
                    break;
                }
            }

            if (Game1.SelectedObject.GetType() == typeof(Tower))
                Game1.SelectedObject = null;
        }

        void add_Clic(object sender, EventArgs e)
        {
            Game1.inGameState = InGameState.Add;
            Game1.SelectedObject = null;
        }

        public void Update(MouseHandler mouse, KeyboardHandler kB)
        {
            goldCount = Game1.gold.ToString();

            foreach (var item in buttonList)
            {
                if ((item.spacePos.Contains(mouse.position) && mouse.LeftClickState == ClickState.Clicked) || kB.pressedKeysList.Contains(item.Hotkey))
                {
                    item.Clicked();
                    Game1.inGameState = item.returnState;
                }
                else if (mouse.LeftClickState == ClickState.Releasing || kB.releasedKeysList.Contains(item.Hotkey))
                {
                    item.Released();
                }
            }
            SetUIText();
        }

        private void SetUIText()
        {
            sB.Clear();
            if (Game1.SelectedObject != null)
            {
                if (Game1.SelectedObject.GetType() == typeof(Tower))
                {
                    sB.Append(string.Format("Dommage:{0}\nRange:{1}\nVitesse:{2}",
                ((Tower)Game1.SelectedObject).damage, ((Tower)Game1.SelectedObject).Range, 1 / (((Tower)Game1.SelectedObject).speed / 1000d)));
                }

                else
                    sB.Append(string.Format("HP:{0}/100", ((Creep)Game1.SelectedObject).life));
            }

            else
                sB.Append("Aucune tour selectionnee");
        }

        internal static void SetButtons()
        {
            if (Game1.SelectedObject != null)
            {
                if (Game1.SelectedObject.GetType() == typeof(Tower))
                {
                    List<UIButtons> buf = new List<UIButtons>();
                    foreach (var item in ((Tower)Game1.SelectedObject).neededFunctions)
                        buf.Add(allButtons.Find(bk => bk.function == item));

                    buttonList = buf;
                    return;
                }
            }
            buttonList = allButtons.Where(bk => bk.returnState == InGameState.Add).ToList();
        }
    }
}