using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ERomagnaGame
{
    public class StartScene : GameScene
    {
        private MenuComponent menu;
        public MenuComponent Menu { get => menu; set => menu = value; }

        private SpriteBatch spriteBatch;
        private string[] menuItems = {
            "Start Game",
            "Help",
            "Credit",
            "Quit"
        };
        public StartScene(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            SpriteFont regularFont = game.Content.Load<SpriteFont>("Fonts/regular");
            SpriteFont hilightFont = game.Content.Load<SpriteFont>("Fonts/hilight");

            Texture2D tex = game.Content.Load<Texture2D>("Images/1");
            Rectangle srcRect = new Rectangle(500, 0, 1000, 500);

            menu = new MenuComponent(game, spriteBatch, regularFont,
                hilightFont, menuItems, tex, srcRect);
            this.Components.Add(menu);

        }


    }
}
