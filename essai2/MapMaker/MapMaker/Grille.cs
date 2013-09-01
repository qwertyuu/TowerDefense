using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapMaker
{
    class Grille
    {
        public Cell[,] cellules = new Cell[100, 100];
        public Grille()
        {
            for (int i = 0; i < cellules.GetLength(0); i++)
            {
                for (int j = 0; j < cellules.GetLength(1); j++)
                {
                    cellules[i,j] = new Cell(j, i);
                }
            }
        }
        public void Draw(SpriteBatch sprite)
        {
            foreach (var item in cellules)
            {
                item.Draw(sprite);
            }
        }
    }
}
