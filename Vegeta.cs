using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace ERomagnaGame
{
    public class Vegeta : DrawableGameComponent
    {
        private List<Texture2D> vegetaTexList;
        private List<Rectangle> vegetaRc;
        private List<SoundEffect> vegetaSound;

        private SpriteBatch spriteBatch;
        public Vector2 position;
        public Vector2 position2;

        private int delayCounterVegeta = 0;
        public int myIndexVegeta = 0;
        private int delay = 4;
        private int delayOriginal = 4;

        public bool VegetaAlive = false;
        public bool VegetaAttacking = false;
        public int VegetaDamage = 23;
        public int VegetaLife = 170;

        public Vegeta(Game game,
            SpriteBatch spriteBatch,
            List<Texture2D> vegetaTexList,
            Vector2 position,
            Vector2 position2,
            List<Rectangle> vegetaRc,
            List<SoundEffect> vegetaSound
                      ) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.vegetaTexList = vegetaTexList;
            this.position = position;
            this.position2 = position2;
            this.vegetaRc = vegetaRc;
            this.vegetaSound = vegetaSound;
        }

        public override void Draw(GameTime gameTime)
        {
            if (VegetaAlive)
            {
                spriteBatch.Begin();
                if (myIndexVegeta >= 25)
                {                  
                    spriteBatch.Draw(vegetaTexList[myIndexVegeta],
                        position2,
                        vegetaRc[myIndexVegeta],
                        Color.White,
                        0,
                        Vector2.Zero,
                        1.75f,
                        SpriteEffects.None,
                        0);
                }
                else
                {
                    spriteBatch.Draw(vegetaTexList[myIndexVegeta],
                        position,
                        vegetaRc[myIndexVegeta],
                        Color.White,
                        0,
                        Vector2.Zero,
                        1.75f,
                        SpriteEffects.None,
                        0);                  
                }
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (VegetaAlive)
            {
                if (delayCounterVegeta > delay)
                {
                    myIndexVegeta++;
                    if (myIndexVegeta == 25)
                    {
                        delay = 20;
                    }
                    else
                    {
                        delay = delayOriginal;
                    }

                    if (myIndexVegeta == 44)
                    {
                        myIndexVegeta = 0;
                        VegetaAttacking = false;
                    }
                    if (myIndexVegeta == 30)
                    {
                        vegetaSound[0].Play();
                        VegetaAttacking = true;
                    }

                    delayCounterVegeta = 0;
                }

                delayCounterVegeta++;
            }

            base.Update(gameTime);
        }
    }
}
