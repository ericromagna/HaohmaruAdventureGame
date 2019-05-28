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
    public class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont, hilightFont;
        private List<string> menuItems;
        public int SelectedIndex { get; set; } = 0;
        private Vector2 position;
        private Color regularColor = Color.Red;
        private Color hilightColor = Color.White;
        private Texture2D tex;
        private Rectangle srcRect;

        private KeyboardState oldState; 

        Vector2 position1 = new Vector2(0,0);

        public MenuComponent(Game game,
            SpriteBatch spriteBatch,
            SpriteFont regularFont,
            SpriteFont hilightFont,
            string[] menus,
            Texture2D tex,
            Rectangle srcRect) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.hilightFont = hilightFont;
            menuItems = menus.ToList<string>();
            position = new Vector2(320,150);
            this.tex = tex;
            this.srcRect = srcRect;
       
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position;
            spriteBatch.Begin();

            spriteBatch.Draw(tex, position1,srcRect, Color.White);

            for (int i = 0; i < menuItems.Count; i++)
            {
                if (SelectedIndex == i)
                {
                    spriteBatch.DrawString(hilightFont,
                        menuItems[i], tempPos, hilightColor);
                    tempPos.Y += hilightFont.LineSpacing;
                }
                else
                {
                    spriteBatch.DrawString(regularFont, menuItems[i],
                        tempPos, regularColor);
                    tempPos.Y += regularFont.LineSpacing;
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            //it is not really going to work!!!
            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                SelectedIndex++;
                if (SelectedIndex == menuItems.Count)
                {
                    SelectedIndex = 0;
                }
            }
            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                SelectedIndex--;
                if (SelectedIndex == -1)
                {
                    SelectedIndex = menuItems.Count - 1;
                }
            }

            oldState = ks;

            base.Update(gameTime);
        }
    }
}
