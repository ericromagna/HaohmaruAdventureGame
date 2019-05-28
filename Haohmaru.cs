using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ERomagnaGame
{
    public class Haohmaru : DrawableGameComponent
    {

        private SpriteBatch spriteBatch;
        public Vector2 position;
        private SpriteFont scoreFont;
        private Vector2 fontPosition;
        private SpriteFont timerFont;
        private Vector2 timerPosition;

        public int min;
        public int max;

        public bool lockDownVariable = false;
        public bool phase2 = false;

        //Controlling by Damage Manager
        public bool haohmaruAlive = true;
        public bool haohmaruAttacking = false;
        public bool haohmaruDefending = false;
        public int haohmaruDamage = 0;
        public int haohmaruLife = 100;
        public int haohmaruScore = 0;
        public double timer = 200;
        public bool noEnemiesOnPhase1Alive = false;
        public bool noEnemiesOnPhase2Alive = false;

        private int phase = 1;

        //This indicates that all bar are green at begin
        private int[] lifeBarDameIndicator = new int[5] {0, 0, 0, 0, 0};
        private Vector2 tagPosition = new Vector2(5, 25);

        private int delayCounterNormal = 0;
        public int myIndexNormal = 0;
        private int delay = 1;

        private int arrowDelayCounter = 0;
        private int arrowDelay = 10;
        private bool arraowBlink = true;

        //Due the constructor errors of different pictures(Haohmaru has more tha 100)
        //This variable is responsable to return the characters to the standard position
        //after an event.
        private float originalY = 350;
        
        private List<Texture2D> texNormalList;
        private List<Texture2D> backgroundList;
        private List<Texture2D> lifeBar;
        private List<Vector2> lifeBarPos;
        private List<SoundEffect> soundHaohmaru;
        public List<Rectangle> haohmaruRctList = new List<Rectangle>();


        public Haohmaru(Game game, 
            SpriteBatch spriteBatch,
            Vector2 position,
            List<Texture2D> texNormalList,
            List<Texture2D> backgroundList,
            List<SoundEffect> soundHaohmaru,
            List<Texture2D> lifeBar,
            List<Vector2> lifeBarPos,
            SpriteFont scoreFont,
            Vector2 fontPosition, 
            SpriteFont timerFont, 
            Vector2 timerPosition
            ) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.position = position;
            this.texNormalList = texNormalList;
            this.backgroundList = backgroundList;
            this.soundHaohmaru = soundHaohmaru;
            this.lifeBar = lifeBar;
            this.lifeBarPos = lifeBarPos;
            this.scoreFont = scoreFont;
            this.fontPosition = fontPosition;
            this.timerFont = timerFont;
            this.timerPosition = timerPosition;
        }

        public void DrawController()
        {
            delayCounterNormal++;
            if (lockDownVariable)
            {
                //Managing position
                if (min == 6)
                {
                    position.X += 1f;
                }
                else if (min == 64)
                {
                    position.X -= 1f;
                }
                else if (min == 33)
                {
                    position.X += 3;
                }
                else if (min == 41)
                {
                    position.X += 2;
                }
                else if (myIndexNormal == 98)
                {
                    position.Y = 400;
                }

                //Managing Index
                if (delayCounterNormal > delay)
                {

                    //Changing the index counter
                    if (myIndexNormal > max || myIndexNormal < min)
                    {
                        myIndexNormal = min;

                        //Correcting position
                        if (min == 71)
                        {
                            position.Y -= 50;

                            haohmaruDamage = 10;
                            haohmaruAttacking = true;
                        }
                        else if (min == 41)
                        {
                            position.Y -= 200;
                        }
                        else if (min == 82)
                        {
                            position.Y -= 50;

                            haohmaruDamage = 20;
                            haohmaruAttacking = true;
                        }
                        else if (min == 13)
                        {
                            haohmaruDamage = 30;
                            haohmaruAttacking = true;
                        }
                    }
                    else if (myIndexNormal == max)
                    {
                        if (min != 98)
                        {
                            myIndexNormal = min;

                            lockDownVariable = false;
                            haohmaruAttacking = false;

                            position.Y = originalY;
                            //Restarting position
                            if (min == 71)
                            {
                                soundHaohmaru[0].Play();
                                myIndexNormal = 0;
                                //position.Y += 50;
                                position.X += 2f;
                            }
                            else if (min == 82)
                            {
                                soundHaohmaru[1].Play();
                                position.X += 30;
                                //position.Y += 50;
                                //myIndexNormal = 0;
                            }
                            else if (min == 13)
                            {
                                soundHaohmaru[0].Play();
                            }
                        }                      
                    }
                    else
                    {
                        myIndexNormal++;
                    }
                    delayCounterNormal = 0;
                }
            }
        }

        public void standard()
        {
            if (delayCounterNormal > delay)
            {
                if (myIndexNormal >= 5)
                {
                    myIndexNormal = 0;
                }
                else
                {
                    myIndexNormal++;
                }
                delayCounterNormal = 0;
            }
        }

        public void LifePositionController()
        {
            if (haohmaruLife <= 0)
            {
                lifeBarDameIndicator[0] = 1;
            }
            if (haohmaruLife < 25)
            {
                lifeBarDameIndicator[1] = 1;
            }
            if (haohmaruLife < 50)
            {
                lifeBarDameIndicator[2] = 1;
            }
            if (haohmaruLife < 75)
            {
                lifeBarDameIndicator[3] = 1;
            }
            if (haohmaruLife < 100)
            {
                lifeBarDameIndicator[4] = 1;
            }
        }

        public void ArrowController()
        {
            if (arrowDelayCounter == arrowDelay)
            {
                soundHaohmaru[2].Play();
                arraowBlink = false;             
            }
            else if (arrowDelayCounter == 2 *arrowDelay)
            {
                arraowBlink = true;
                arrowDelayCounter = 0;
            }
            
            arrowDelayCounter++;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            //Background Control Draw
            if (phase == 1)
            {
                spriteBatch.Draw(backgroundList[3], Vector2.Zero, Color.White);
            }
            else if (phase == 2)
            {
                spriteBatch.Draw(backgroundList[4], Vector2.Zero, Color.White);
            }

            //Victory Screen Draw
            if (noEnemiesOnPhase2Alive)
            {
                spriteBatch.Draw(texNormalList[104], Vector2.Zero, Color.White);
            }

            //Drawing Haohmaru
            spriteBatch.Draw(texNormalList[myIndexNormal],position,Color.White);

            //Drawing Bar Life
            #region BarDrawing
            //Drawing Haohmaru's Life Bar
            spriteBatch.Draw(texNormalList[102], tagPosition, Color.White);
            spriteBatch.Draw(lifeBar[lifeBarDameIndicator[4]],lifeBarPos[0], Color.White);
            spriteBatch.Draw(lifeBar[lifeBarDameIndicator[3]], lifeBarPos[1], Color.White);
            spriteBatch.Draw(lifeBar[lifeBarDameIndicator[2]], lifeBarPos[2], Color.White);
            spriteBatch.Draw(lifeBar[lifeBarDameIndicator[1]], lifeBarPos[3], Color.White);
            spriteBatch.Draw(lifeBar[lifeBarDameIndicator[0]], lifeBarPos[4], Color.White);
            #endregion

            //Drawing Score
            spriteBatch.DrawString(scoreFont,"Points: " + haohmaruScore.ToString(), fontPosition, Color.MintCream);

            //Drawing Timer
            spriteBatch.DrawString(timerFont, Math.Round(timer,0).ToString(),timerPosition, Color.MonoGameOrange);

            //Drawing warning arrow
            if (noEnemiesOnPhase1Alive && !phase2 && arraowBlink)
            {     
                spriteBatch.Draw(texNormalList[103], timerPosition, Color.White);
            }           

            spriteBatch.End();
                      
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            //Controlling Arrow
            if (noEnemiesOnPhase1Alive && !phase2 )
            {
                ArrowController();
            }
               

            //Controlling Timer
            if (timer >= 0 && !noEnemiesOnPhase2Alive)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }

            //Life Bar Events update
            LifePositionController();

            //Haohmaru Events Update
            KeyboardState ks = Keyboard.GetState();

            if (!phase2 && (position.X >= backgroundList[3].Width - 260) && noEnemiesOnPhase1Alive )
            {
                phase2 = true;
                phase = 2;
                position.X = 100;
            }
            else if ((position.X >= backgroundList[3].Width - 260))
            {
                position.X = backgroundList[3].Width -260;
            }
            else if (phase2 && (position.X >= backgroundList[4].Width - 200))
            {
                position.X = backgroundList[4].Width - 200;
            }

            haohmaruDefending = false;
            
            if (ks.IsKeyDown(Keys.Z) )//--Defense--//
            {
                haohmaruDefending = true;
                lockDownVariable = true;
                min = 60;
                max = 60;
            }
            else if (ks.IsKeyDown(Keys.C) && !lockDownVariable)//--Jumping--//
            {
               lockDownVariable = true;
               min = 41;
               max = 53;       
            }
            else if (ks.IsKeyDown(Keys.N) && !lockDownVariable)
            {               
                lockDownVariable = true;
                min = 82;
                max = 97;
            }
            else if (ks.IsKeyDown(Keys.Space) && !lockDownVariable)//--Katana hit--//
            {
                lockDownVariable = true;
                min = 13;
                max = 26;
            }
            else if (ks.IsKeyDown(Keys.B) && !lockDownVariable)//--Hit2--//
            {
                lockDownVariable = true;
                min = 71;
                max = 81;
            }
            else if (ks.IsKeyDown(Keys.Right) && ks.IsKeyDown(Keys.X) && !lockDownVariable)//--Running--//
            {
                lockDownVariable = true;
                min = 33;
                max = 40;
            }
            else if (ks.IsKeyDown(Keys.Right) && !lockDownVariable) //--Walking--//
            {
                lockDownVariable = true;
                min = 6;
                max = 12;
            }
            else if (ks.IsKeyDown(Keys.Left) && !lockDownVariable)//--WalkingBack--//
            {
                lockDownVariable = true;
                min = 64;
                max = 70;
            }
            else if(!lockDownVariable) //--Normal--//
            {
                standard();
            }

            DrawController();
            
            base.Update(gameTime);
        }
    }
}
