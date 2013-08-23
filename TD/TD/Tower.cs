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
        public static List<Texture2D> towerTextures;

        public void LoadTowerTextures()
        {

        }

        public Tower(Types type)
        {
            text = towerTextures[(int)type];
            boundingBox = new Rectangle(0, 0, 50, 50);
        }

        public void Add(MouseHandler souris)
        {

        }

        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(text, boundingBox, Color.White);
        }

        public Rectangle BoundingBox { get { return boundingBox; } }

    }
}
