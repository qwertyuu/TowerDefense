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
    public class Tower
    {
        public enum Types { type1, swag }
        private int _Range;
        public int Range
        {
            get
            {
                return _Range;
            }
            set
            {
                cercle = Camera.CreateCircle(value);
                rangePos = SetRangePos(cercle);
                _Range = value;
            }
        }

        private Vector2 SetRangePos(Texture2D cercle)
        {
            return new Vector2(boundingBox.Center.X - (cercle.Width / 2), boundingBox.Center.Y - (cercle.Height / 2));
        }
        public Vector2 rangePos { get; set; }
        public Texture2D cercle { get; set; }
        public Texture2D text;
        private static Tower _currentTower;
        public static Tower currentTower
        {
            get
            {
                return _currentTower;
            }
            set
            {
                if (value == null)
                {
                    InGameUI.SetButtons();
                    if (_currentTower != null)
                    {
                        _currentTower.show = false;
                    }
                }
                else
                {
                    if (value != _currentTower)
                    {
                        if (_currentTower != null)
                        {
                            _currentTower.show = false;
                        }
                    }
                    InGameUI.SetButtons(value);
                }
                _currentTower = value;
                if (_currentTower != null)
                {
                    _currentTower.show = true;
                }
            }
        }
        public Rectangle boundingBox;
        public Types type;
        public List<UIButtonFunction> neededFunctions { get; set; }
        public Vector2 gridPosition;
        public int level;
        public int damage;
        public bool show;

        public Tower(Point pos, Types _type, Texture2D texture, int range, bool _show)
        {
            level = 1;
            type = _type;
            text = texture;
            show = _show;
            boundingBox = new Rectangle(pos.X, pos.Y, Cell.size, Cell.size);
            Range = range;
            neededFunctions = new List<UIButtonFunction>();
            if (_type == Types.type1)
            {
                neededFunctions.Add(UIButtonFunction.Add);
                neededFunctions.Add(UIButtonFunction.Sell);
                neededFunctions.Add(UIButtonFunction.Upgrade);
            }
        }

        public void Draw(SpriteBatch sprite, float alpha)
        {
            if (this == currentTower)
                sprite.Draw(text, boundingBox, Color.Blue * alpha);
            else
                sprite.Draw(text, boundingBox, Color.Red * alpha);
        }
        public void DrawRange(SpriteBatch sprite)
        {
            sprite.Draw(cercle, rangePos, Color.Blue * 0.3f);
        }

        public void levelUp()
        {
            level++;
            //damage =   ???  ;
        }



        public Rectangle BoundingBox { get { return boundingBox; } }
    }
}