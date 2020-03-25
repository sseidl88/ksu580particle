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
    enum PanAnimation
    {
        empty = 4,
        raw = 1,
        done = 2,
        burnt = 3,
    }

    public class Pan
    {
        public BoundingRectangle panPosition;
        public Texture2D pan;
        ContentManager content;

        const int ANIMATION_FRAME_RATE = 124;
        TimeSpan timerRate;
        PanAnimation animateState;

        int frame;
        const int FRAME_WIDTH = 350;

        const int FRAME_HEIGHT = 200;


        public Pan(int x, int y)
        {
            panPosition.X = x;
            panPosition.Y = y;
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            pan = this.content.Load<Texture2D>("burnPan");
            animateState = PanAnimation.empty;
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var source = new Rectangle(
            frame * FRAME_WIDTH, // X value 
            (int)animateState % 4 * FRAME_HEIGHT, // Y value
            FRAME_WIDTH, // Width 
            FRAME_HEIGHT // Height
            );

            spriteBatch.Draw(pan, new Vector2(panPosition.X, panPosition.Y),source, Color.White);
        }

    }
}
