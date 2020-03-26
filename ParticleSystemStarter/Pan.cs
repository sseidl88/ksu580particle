using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameWindowsStarter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSystemStarter
{
    public enum PanAnimation
    {
        empty = 4,
        raw = 1,
        done = 2,
        burnt = 3,
    }

    public class Pan
    {
        public BoundingRectangle position;
        public Rectangle RectBounds
        {
            get { return position; }
        }
        public Texture2D pan;
        ContentManager content;

        const int ANIMATION_FRAME_RATE = 124;
        TimeSpan timerRate;
        public PanAnimation animateState;

        int frame;
        const int FRAME_WIDTH = 350;

        const int FRAME_HEIGHT = 200;

        //cookingtimer
        public float timer = 0;


        public Pan(int x, int y)
        {
            position.X = x;
            position.Y = y;
            position.Height = 200;
            position.Width = 320;
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            pan = this.content.Load<Texture2D>("burnPan");
            animateState = PanAnimation.empty;
        }

        public void Update(GameTime gameTime)
        {
            if(animateState == PanAnimation.raw)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if(timer > 5 && timer < 10)
            {
                animateState = PanAnimation.done;
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if(timer > 10)
            {
                animateState = PanAnimation.burnt;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var source = new Rectangle(
            frame * FRAME_WIDTH, // X value 
            (int)animateState % 4 * FRAME_HEIGHT, // Y value
            FRAME_WIDTH, // Width 
            FRAME_HEIGHT // Height
            );

            spriteBatch.Draw(pan, new Vector2(position.X, position.Y),source, Color.White);
        }

    }
}
