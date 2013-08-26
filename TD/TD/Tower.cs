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
    class Tower
    {
        public enum Types { type1, swag }
        Texture2D text;
        Rectangle boundingBox;

        /*
        public static List<Texture2D> towerTextures;

        public void LoadTowerTextures(Texture2D[] texture)
        {
            for (int i = 0; i < texture.Length; i++)
                towerTextures.Add(texture[i]);
        } */

        public Tower(Point pos, Types type, Texture2D texture)
        {
            text = texture;
            boundingBox = new Rectangle(pos.X, pos.Y, 50, 50);
        }

        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(text, boundingBox, Color.White);
        }

        public Rectangle BoundingBox { get { return boundingBox; } }

    }
}
