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
    public class Creep
    {
        public enum Types { type1, type2 }

        public Rectangle boundingBox { get; set; }
        public int life;
        public Types type { get; set; }
        public Texture2D text;
        public Vector2 gridPos;

        public Creep(Point pos, Types _type, Texture2D _text)
        {
            life = 100;
            boundingBox = new Rectangle(pos.X, pos.Y, Cell.size, Cell.size);
            text = _text;
            type = _type;
        }

        public void Draw(SpriteBatch sprite)
        {
            if (life != 0)
                sprite.Draw(text, boundingBox, Color.White);
        }

        public void getDamage(int amount)
        {
            life -= amount;
            if (life < 0)
                life = 0;
        }
    }
}
