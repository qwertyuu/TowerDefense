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

    class Buttons : Drawable
    {
        
        public Buttons()
        {
            texture = Game1.cellT;
            returnState = GameState.None;
        }

        override public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, spacePos, couleur * transparency);
            spriteBatch.DrawString(font, text, textSpot, fontColor);
        }

        override public bool Update(MouseHandler mouse, IMenu sender)
        {

            if (spacePos.Contains(mouse.position))
            {
                transparency = 0.5f;
                if (mouse.LeftClickState == ClickState.Clicked)
                {
                    Clicked(sender);
                    if (returnState != GameState.None)
                        return true;
                }
            }
            else
                transparency = 1.0f;

            return false;
        }

    }
}
