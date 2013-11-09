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

namespace TD.Menus
{
    class MainMenu : IMenu
    {
        public MainMenu()
        {
            gameState = GameState.MainMenu;

            Drawable header = new Drawable();
            header.text = "Main Menu";
            header.font = Game1.font;
            header.fontColor = Color.Black;
            header.texture = Game1.cellT;
            header.couleur = Color.White;
            header.returnState = GameState.None;
            AddDrawable(header);


            Buttons play = new Buttons();
            play.text = "Play";
            play.couleur = Color.Blue;
            play.returnState = GameState.InGame;
            AddDrawable(play);

            Buttons options = new Buttons();
            options.text = "Options";
            options.couleur = Color.White;
            options.fontColor = Color.Black;
            options.returnState = GameState.Options;
            AddDrawable(options);

            Buttons quit = new Buttons();
            quit.text = "Quit";
            quit.couleur = Color.Orange;
            quit.Clic += quit_Clic;
            AddDrawable(quit);
        }

        private void quit_Clic(object sender, IMenu swag)
        {
            Game1._Exit = true;
        }
        public override void EscapePressed()
        {
            Game1._Exit = true;
            base.EscapePressed();
        }

    }
}
