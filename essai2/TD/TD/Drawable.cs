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
    class Drawable
    {
        virtual public Rectangle spacePos { get; set; }
        public Texture2D texture { get; set; }

        public Color couleur { get; set; }

        virtual public Vector2 textSpot { get; set; }
        public Vector2 textOffset { get; set; }

        public Color fontColor { get; set; }
        private SpriteFont _font { get; set; }

        public SpriteFont font
        {
            get
            {
                return _font;
            }

            set
            {
                _font = value;
                fontColor = Color.White;
                Vector2 buf = _font.MeasureString(text);
                spacePos = new Rectangle(spacePos.X, spacePos.Y, (int)buf.X + 100, (int)buf.Y + 10);
                textOffset = new Vector2(spacePos.Width / 2 - buf.X / 2, spacePos.Height / 2 - buf.Y / 2);
            }
        }

        public static float variation = 50;

        public bool offset { get; set; }

        private string _text;
        public string text
        {
            get { return _text; }

            set { 
                _text = value;
                font = Game1.font;
            }
        }

        public GameState returnState { get; set; }

        public delegate void OwnerChangedEventHandler(object sender, IMenu swag);

        public event OwnerChangedEventHandler Clic;

        internal void Clicked(IMenu lol)
        {
            if (Clic != null)
            {
                Clic(this, lol);
            }
        }

        public float transparency = 1;

        virtual public bool Update(MouseHandler mouse, IMenu sender)
        {
            return false;
        }

        virtual public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, textSpot, couleur);
        }

    }
}
