using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TD.Menus
{
    class Options : IMenu
    {
        GraphicsDeviceManager graphics;
        public Options(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            gameState = GameState.Options;
            Buttons AntiAlias = new Buttons();
            AntiAlias.text = "Antialias: " + graphics.PreferMultiSampling;
            AntiAlias.couleur = Color.Blue;
            AntiAlias.Clic += AntiAlias_Clic;
            AddButton(AntiAlias);

            Buttons back = new Buttons();
            back.text = "Back";
            back.couleur = Color.Orange;
            back.returnState = GameState.MainMenu;
            back.Clic += back_Clic;
            AddButton(back);
            this.Escape = back.returnState;
        }

        void back_Clic(object sender, EventArgs e)
        {
            graphics.ApplyChanges();
        }
        public override void EscapePressed()
        {
            buttonsList[1].Clicked();
            base.EscapePressed();
        }

        void AntiAlias_Clic(object sender, EventArgs e)
        {
            graphics.PreferMultiSampling = !graphics.PreferMultiSampling;
            ((Buttons)sender).text = "Antialias: " + graphics.PreferMultiSampling;
        }
    }
}
