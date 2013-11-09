#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using TD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace TD.Towers
{
    class Type1Tower : Tower
    {
        public Type1Tower(Point pos, Texture2D texture, int range, bool _show)
            : base(pos, texture, range, _show)
        {
            this.cost = 50;
            this.upgradeCost = 150;
        }
    }
}
