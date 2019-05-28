using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace ERomagnaGame
{
    public class DamageManager : GameComponent
    {
        private Haohmaru haohmaru;
        private Hanzo hanzo;
        private Vegeta vegeta;

        private List<Song> song;
        private List<SoundEffect> soundDamage;

        private bool creatingVegeta = true;

        public DamageManager(Game game,
            Haohmaru haohmaru,
            Hanzo hanzo,
            Vegeta vegeta,
            List<SoundEffect> soundDamage,
            List<Song> song) : base(game)
        {
            this.haohmaru = haohmaru;
            this.hanzo = hanzo;
            this.vegeta = vegeta;
            this.soundDamage = soundDamage;
            this.song = song;
        }

        public override void Update(GameTime gameTime)
        {
           

            //Creating Vegeta after Hanzo died and after player entered in phase2
            if (haohmaru.phase2 && creatingVegeta)
            {
                creatingVegeta = false;
                vegeta.VegetaAlive = true; 
            }

            //Hanzo-Haohmaru Damage/Score Management
            if (hanzo.hanzoAlive)
            {
                if (hanzo.position.X - haohmaru.position.X <= 120
                || hanzo.position2.X - haohmaru.position.X <= 120)
                {
                    if (haohmaru.haohmaruAttacking && !hanzo.hanzoDefense)
                    {
                        hanzo.hanzoLife -= haohmaru.haohmaruDamage;
                        haohmaru.haohmaruAttacking = false;
                        haohmaru.haohmaruScore += 100;
                        soundDamage[1].Play();
                    }

                    if (hanzo.hanzoAttacking && !haohmaru.haohmaruDefending && hanzo.hanzoAlive)
                    {
                        haohmaru.haohmaruLife -= hanzo.hanzoDamage;
                        hanzo.hanzoAttacking = false;
                        haohmaru.haohmaruScore -= 40;
                        haohmaru.position.X -= 90f;
                        soundDamage[0].Play();
                    }

                    if (haohmaru.haohmaruLife <= 0)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(song[2]);

                        haohmaru.min = 98;
                        haohmaru.max = 101;
                        haohmaru.lockDownVariable = true;
                    }

                    if (hanzo.hanzoLife <= 0)
                    {
                        hanzo.hanzoAlive = false;
                        haohmaru.noEnemiesOnPhase1Alive = true;
                        haohmaru.haohmaruScore += 500;
                    }
                }
            }

            //Vegeta-Haohmaru Damage/Score Management
            if (vegeta.VegetaAlive)
            {
                if (vegeta.position.X - haohmaru.position.X <= 120)
                {
                    //Haohmaru hitting Vegeta
                    if (haohmaru.haohmaruAttacking /*&& !vegeta.vegetaDefense*/)
                    {
                        vegeta.position.X += 2f;
                        vegeta.VegetaLife -= haohmaru.haohmaruDamage;
                        haohmaru.haohmaruAttacking = false;
                        haohmaru.haohmaruScore += 120;
                        soundDamage[1].Play();
                    }

                    //Vegeta attacking Haohmaru
                    if (vegeta.VegetaAttacking && !haohmaru.haohmaruDefending && vegeta.VegetaAlive)
                    {
                        haohmaru.position.X -= 180f;
                        haohmaru.haohmaruLife -= vegeta.VegetaDamage;
                        vegeta.VegetaAttacking = false;
                        haohmaru.haohmaruScore -= 80;
                        soundDamage[0].Play();
                    }

                    //Haohmaru Died. Game is over!!
                    if (haohmaru.haohmaruLife <= 0)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(song[2]);

                        haohmaru.min = 98;
                        haohmaru.max = 101;
                        haohmaru.lockDownVariable = true;
                    }

                    if (vegeta.VegetaLife <= 0)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(song[1]);

                        vegeta.VegetaAlive = false;
                        haohmaru.noEnemiesOnPhase2Alive = true;
                        haohmaru.haohmaruScore += 900;
                        haohmaru.haohmaruScore += (int)haohmaru.timer * 25;
                    }
                }

            }



            //Burying Vegeta in a not available place.
            if (vegeta.VegetaAlive == false && !creatingVegeta)
            {
                vegeta.position = new Vector2(-150, -150);
            }

            //Burying Hanzo in a not available place.
            if (!hanzo.hanzoAlive)
            {
                hanzo.position = new Vector2(-150, -150);
                hanzo.position2 = new Vector2(-150, -150);
            }
            

            base.Update(gameTime);
        }
    }
}
