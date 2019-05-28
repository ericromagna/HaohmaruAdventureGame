using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace ERomagnaGame
{
    public class ActionScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private SpriteFont scoreFont;
        private SpriteFont timerFont;

        List<Texture2D> texNormalList = new List<Texture2D>();
        List<Texture2D> backgroundList = new List<Texture2D>();
        List<Texture2D> hanzoTexList = new List<Texture2D>();
        List<Texture2D> vegetaTexList = new List<Texture2D>();
        List<Texture2D> lifeBarHaohmaru = new List<Texture2D>();
        List<Rectangle> vegetaRct = new List<Rectangle>();
        List<SoundEffect> soundHaohmaru = new List<SoundEffect>();
        List<SoundEffect> soundHanzo = new List<SoundEffect>();
        List<SoundEffect> soundVegeta = new List<SoundEffect>();
        List<SoundEffect> soundDamageMnt = new List<SoundEffect>();
        List<Song> songs = new List<Song>();

        private Vector2 fontPos = new Vector2(175, 50);
       
        private int barXPosition = 260;
           
        //private Characteres;
        private Haohmaru haohmaru;
        private Hanzo hanzo;
        private Vegeta vegeta;
        private DamageManager dm;
     
        /// <summary>
        /// This methods return a List of Vector2 position of life bars,
        /// based on their size (which is the same for all of them).
        /// </summary>
        /// <param name="barTex">Regular LifeBar Tex</param>
        /// <returns>List of Life Bar positions(5)</returns>
        public List<Vector2> LifeBarPosition(Texture2D barTex)
        {
            List<Vector2> barPositionList = new List<Vector2>();

            for (int i = 5; i > 0; i--)
            {
                barPositionList.Add(new Vector2(barXPosition, 25));
                barXPosition -= barTex.Width;
            }

            return barPositionList;
        }

        public ActionScene(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;

            //The loading resources from Haohmaru/Hanzo and Vegeta are not filled
            //by an interactive method because the folder routes are different. Also
            //it's not allways the pictures follow a conventional order, which some of
            //them were excluded from this game, in order to make it simple and faster. 
            //Therefore, it's easy to find sequence of pictures name like 'p1','p2','p4'. 
            //In this case p3 were left out, so create iterative method to fill the Lists 
            //would be so expensive as just type them out.

            #region loading Haohmaru resources

            //Score font
            scoreFont = game.Content.Load<SpriteFont>("Fonts/regular");
            timerFont = game.Content.Load<SpriteFont>("Fonts/hilight");

            //Loading soundeffects
            soundHaohmaru.Add(game.Content.Load<SoundEffect>("SoundEffectsHaohmaru/344131__thebuilder15__sword-slice"));
            soundHaohmaru.Add(game.Content.Load<SoundEffect>("SoundEffectsHaohmaru/storm"));
            soundHaohmaru.Add(game.Content.Load<SoundEffect>("SoundEffectDamageMng/WoopWoop"));

            //Loading Life Bar
            lifeBarHaohmaru.Add(game.Content.Load<Texture2D>("Images/Haohmaru/LifeBar/lifeOk"));
            lifeBarHaohmaru.Add(game.Content.Load<Texture2D>("Images/Haohmaru/LifeBar/lifeDamage"));

            //Loading Haohmaru Normal  Images/Haohmaru/Normal/0
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Normal/0"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Normal/1"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Normal/2"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Normal/3"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Normal/4"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Normal/5"));

            //6-12--------WALKING 
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Walking/0"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Walking/1"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Walking/2"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Walking/3"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Walking/4"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Walking/5"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Walking/6"));
            //


            // 13-29  KATANA HIT
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Katana/1"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Katana/2"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Katana/3"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Katana/4"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Katana/5"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Katana/6"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Katana/7"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Katana/8"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Katana/9"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Katana/10"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Katana/11"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Katana/11"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Katana/12"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Katana/13"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Katana/14"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Katana/15"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Katana/16"));
            //

            //30-40 RUNNING MOVIMENT
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Running/0"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Running/1"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Running/2"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Running/3"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Running/4"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Running/5"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Running/6"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Running/7"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Running/8"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Running/9"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Running/10"));

            //

            //41-53  JUMPING
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/JumpingNormal/output-0"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/JumpingNormal/output-1"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/JumpingNormal/output-2"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/JumpingNormal/output-3"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/JumpingNormal/output-4"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/JumpingNormal/output-5"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/JumpingNormal/output-6"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/JumpingNormal/output-7"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/JumpingNormal/output-8"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/JumpingNormal/output-9"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/JumpingNormal/output-10"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/JumpingNormal/output-11"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/JumpingNormal/output-12"));
            //

            //54-63  DEFENSE
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Defense/output-0"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Defense/output-1"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Defense/output-2"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Defense/output-3"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Defense/output-4"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Defense/output-5"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Defense/output-6"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Defense/output-7"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Defense/output-8"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Defense/output-9"));

            //

            //64-70 WALKING DOWN
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Walking/0"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Walking/1"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Walking/2"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Walking/3"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Walking/4"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Walking/5"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Walking/6"));

            //71-81  HIT 02
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Hit2/output-0"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Hit2/output-1"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Hit2/output-2"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Hit2/output-3"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Hit2/output-4"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Hit2/output-5"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Hit2/output-6"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Hit2/output-7"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Hit2/output-8"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Hit2/output-9"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Hit2/output-10"));
            //

            //82-97 Tornator
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/AddTornato/output-0"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/AddTornato/output-1"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/AddTornato/output-2"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/AddTornato/output-3"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/AddTornato/output-4"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/AddTornato/output-5"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/AddTornato/output-6"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/AddTornato/output-7"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/AddTornato/output-8"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/AddTornato/output-9"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/AddTornato/output-10"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/AddTornato/output-11"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/AddTornato/output-12"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/AddTornato/output-13"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/AddTornato/output-14"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/AddTornato/output-15"));

            //98-101  Defeated
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Defeat/haoh-wound-0"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Defeat/haoh-wound-1"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Defeat/haoh-wound-2"));
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/Defeat/haoh-wound-3"));

            //102 FaceTag
            texNormalList.Add(game.Content.Load<Texture2D>("Images/Haohmaru/FaceTag/tag"));

            //103 ArrowRight
            texNormalList.Add(game.Content.Load<Texture2D>("Backgrounds/Arrow-right"));

            //104 FinalTheme
            texNormalList.Add(game.Content.Load<Texture2D>("Backgrounds/victory"));
            #endregion

            #region loading Hanzo resources
            soundHanzo.Add(game.Content.Load<SoundEffect>("SoundEffectsHanzo/Big_Explosion_Effect_Video_Mp4_HD_Sound-bhZs3ALdL7Y"));

            //Fadding 0-18
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/faddingOut/0"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/faddingOut/1"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/faddingOut/2"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/faddingOut/3"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/faddingOut/4"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/faddingOut/5"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/faddingOut/6"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/faddingOut/7"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/faddingOut/8"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/faddingOut/9"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/faddingOut/10"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/faddingOut/11"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/faddingOut/12"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/faddingOut/13"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/faddingOut/14"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/faddingOut/15"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/faddingOut/16"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/faddingOut/17"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/faddingOut/18"));

            //Fire blast 19-35
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/fireBlast/hanzo-fireblast-2-0"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/fireBlast/hanzo-fireblast-2-1"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/fireBlast/hanzo-fireblast-2-2"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/fireBlast/hanzo-fireblast-2-3"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/fireBlast/hanzo-fireblast-2-4"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/fireBlast/hanzo-fireblast-2-5"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/fireBlast/hanzo-fireblast-2-6"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/fireBlast/hanzo-fireblast-2-7"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/fireBlast/hanzo-fireblast-2-8"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/fireBlast/hanzo-fireblast-2-9"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/fireBlast/hanzo-fireblast-2-10"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/fireBlast/hanzo-fireblast-2-11"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/fireBlast/hanzo-fireblast-2-12"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/fireBlast/hanzo-fireblast-2-13"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/fireBlast/hanzo-fireblast-2-14"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/fireBlast/hanzo-fireblast-2-15"));
            hanzoTexList.Add(game.Content.Load<Texture2D>("Images/Hanzo/fireBlast/hanzo-fireblast-2-16"));

            #endregion

            #region loading Background images
            backgroundList.Add(game.Content.Load<Texture2D>("Backgrounds/selectTheme"));
            backgroundList.Add(game.Content.Load<Texture2D>("Backgrounds/5"));
            backgroundList.Add(game.Content.Load<Texture2D>("Backgrounds/r1"));
            backgroundList.Add(game.Content.Load<Texture2D>("Backgrounds/r2"));
            backgroundList.Add(game.Content.Load<Texture2D>("Backgrounds/r3"));
            backgroundList.Add(game.Content.Load<Texture2D>("Backgrounds/r4"));
            backgroundList.Add(game.Content.Load<Texture2D>("Backgrounds/r5"));
            backgroundList.Add(game.Content.Load<Texture2D>("Backgrounds/r6"));
            backgroundList.Add(game.Content.Load<Texture2D>("Backgrounds/m1"));

            #endregion

            #region loading Vegeta resources

            //SoundEffects
            soundVegeta.Add(game.Content.Load<SoundEffect>("SoundEffectsHanzo/Big_Explosion_Effect_Video_Mp4_HD_Sound-bhZs3ALdL7Y"));

            //0-25 RaisingKi
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-0"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-1"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-2"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-3"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-4"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-5"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-6"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-7"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-8"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-9"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-10"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-11"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-12"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-13"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-14"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-15"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-16"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-17"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-18"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-19"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-20"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-21"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-22"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-23"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/RaisingKi/vflip1-24"));
  

            //25-44 BlastingKi
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/BlastingKi/vflip2-0"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/BlastingKi/vflip2-1"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/BlastingKi/vflip2-2"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/BlastingKi/vflip2-3"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/BlastingKi/vflip2-4"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/BlastingKi/vflip2-5"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/BlastingKi/vflip2-6"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/BlastingKi/vflip2-7"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/BlastingKi/vflip2-8"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/BlastingKi/vflip2-9"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/BlastingKi/vflip2-10"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/BlastingKi/vflip2-11"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/BlastingKi/vflip2-12"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/BlastingKi/vflip2-13"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/BlastingKi/vflip2-14"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/BlastingKi/vflip2-15"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/BlastingKi/vflip2-16"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/BlastingKi/vflip2-17"));
            vegetaTexList.Add(game.Content.Load<Texture2D>("Images/Vegeta/BlastingKi/vflip2-18"));

            for (int i = 0; i < vegetaTexList.Count(); i++)
            {
                if (i > 24 && i < 9)
                {
                    vegetaRct.Add(new Rectangle(0, 0, vegetaTexList[i].Width/2, vegetaTexList[i].Height));
                }
                else
                {
                    vegetaRct.Add(new Rectangle(0, 0, vegetaTexList[i].Width, vegetaTexList[i].Height));
                }
            }

            #endregion

            #region loading damage Manager Resources
            soundDamageMnt.Add(game.Content.Load<SoundEffect>("SoundEffectsHaohmaru/Pain"));
            soundDamageMnt.Add(game.Content.Load<SoundEffect>("SoundEffectsHanzo/MaleGrunt"));
            #endregion

            Vector2 position = new Vector2(100, 350);
            Vector2 timerPos = new Vector2(backgroundList[0].Width, 25);

            //----------Backgorund music of game------------------
            songs.Add(game.Content.Load<Song>("Music/kotoMusic"));
            songs.Add(game.Content.Load<Song>("Music/Queen"));
            songs.Add(game.Content.Load<Song>("Music/TheEnd_cutted"));

            MediaPlayer.Stop();
            MediaPlayer.Play(songs[0]);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.45f;

            //Both Hanzo and Vegeta have two position because the images have an construction possion error
            Vector2 positionHanzo = new Vector2(500, 350);
            Vector2 positionHanzo2 = new Vector2(500, 320);

            Vector2 positionVegeta = new Vector2(500, 220);
            Vector2 positionVegeta2 = new Vector2(300, 310);

            haohmaru = new Haohmaru(game,
                spriteBatch,
                position,
                texNormalList,
                backgroundList,
                soundHaohmaru,
                lifeBarHaohmaru,
                LifeBarPosition(lifeBarHaohmaru[0]),
                scoreFont,
                fontPos,
                timerFont,
                timerPos);

            this.Components.Add(haohmaru);

            hanzo = new Hanzo(game,
                spriteBatch,
                positionHanzo,
                positionHanzo2,
                hanzoTexList,
                soundHanzo);

            this.Components.Add(hanzo);

            vegeta = new Vegeta(game,
                spriteBatch,
                vegetaTexList,
                positionVegeta,
                positionVegeta2,
                vegetaRct,
                soundVegeta);

            this.Components.Add(vegeta);

            dm = new DamageManager(game,
                haohmaru,
                hanzo,
                vegeta,
                soundDamageMnt,
                songs);

            this.Components.Add(dm);
        }
    }
}
