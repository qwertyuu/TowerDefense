using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace TD
{
    class Cell
    {
        public enum CellTypes {Rock, Path, Turret};
        public Tower contains { get; set; }
        public Creep creepContains { get; set; }
        public static int size = 20;
        public Rectangle spacePos;
        public CellTypes type;

        public Cell(CellTypes _type, Rectangle _spacePos)
        {
            type = _type;
            contains = null;
            spacePos = _spacePos;
        }
    }
}
