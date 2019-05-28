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
   public class HelpScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        
        public HelpScene(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            tex = game.Content.Load<Texture2D>("Backgrounds/Help2");
            Vector2 pos = new Vector2(0, 0);
        }

        public override void Draw(GameTime gameTime)
        {
            double wdt = tex.Width / 1.75;
            Rectangle rtg = new Rectangle(0, 0,Convert.ToInt32(wdt), tex.Height);
            spriteBatch.Begin();
            spriteBatch.Draw(tex, rtg, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
