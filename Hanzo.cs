using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERomagnaGame
{
    public class Hanzo : DrawableGameComponent
    {
        private List<Texture2D> hanzoTexList;
        private List<SoundEffect> soundHanzo;
        private List<double> randomList = new List<double>();
        Random rdn = new Random();

        private SpriteBatch spriteBatch;
        public Vector2 position;
        public Vector2 position2;

        private int delayCounterHanzo = 0;
        public int myIndexHanzo = 0;
        private int delay = 4;
        private int delayOriginal = 4;

        private bool invert = true;

        public bool hanzoAlive = true;
        public bool hanzoAttacking = false;
        public bool hanzoDefense = false;
        public int  hanzoDamage = 19;
        public int hanzoLife = 130;

        public Hanzo(Game game,
            SpriteBatch spriteBatch,
            Vector2 position,
            Vector2 position2,
            List<Texture2D> hanzoTexList,
            List<SoundEffect> soundHanzo) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.position = position;
            this.position2 = position2;
            this.hanzoTexList = hanzoTexList;
            this.soundHanzo = soundHanzo;
        }

        public void RandomPositionMake()
        {
            if (invert)
            {
                float additionPosition = (position.X / 20) * rdn.Next(1, 11);
                float additionPosition2 = (position.X / 10) * rdn.Next(1, 10);

                position.X -= 1 * additionPosition;
                position2.X -= 1 * additionPosition2;
                invert = !invert;
            }
            else
            {
                float additionPosition = (position.X / 20) * rdn.Next(1, 11);
                float additionPosition2 = (position.X / 10) * rdn.Next(1, 10);

                position.X += 1 * additionPosition;
                position2.X += 1 * additionPosition2;
                invert = !invert;
            }
           
           

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (myIndexHanzo < 19)
            {
              spriteBatch.Draw(hanzoTexList[myIndexHanzo],position,Color.White);
            }
            else
            {
              spriteBatch.Draw(hanzoTexList[myIndexHanzo], position2, Color.White);
            }
            

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (hanzoAlive)
            {
                if (delayCounterHanzo > delay)
                {
                    myIndexHanzo++;
                    if (myIndexHanzo == 19)
                    {
                        //RandomPositionMake();
                        delay = 10;
                        hanzoDefense = true;
                    }
                    else if (myIndexHanzo < 6)
                    {
                        hanzoDefense = false;
                        delay = 10;
                    }
                    else
                    {
                        hanzoDefense = false;
                        delay = delayOriginal;
                    }

                    if (myIndexHanzo == 35)
                    {
                        myIndexHanzo = 0;
                        hanzoAttacking = false;
                    }
                    if (myIndexHanzo == 25)
                    {
                        soundHanzo[0].Play();
                        hanzoAttacking = true;
                    }

                    delayCounterHanzo = 0;
                }

                delayCounterHanzo++;
            }

            base.Update(gameTime);
        }
    }
}
