using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameWindowsStarter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSystemStarter
{
    public enum handAnimation
    {
        open = 0,
        closed = 1,
    }

    public class Hand
    {
        public BoundingRectangle handPosition;
        public Texture2D hand;
        ContentManager content;
        MouseState mouse;

        //animation
        const int ANIMATION_FRAME_RATE = 124;
        TimeSpan timerRate;
        public handAnimation animateState;

        int frame;
        const int FRAME_WIDTH = 54;

        const int FRAME_HEIGHT = 54;


        public Hand(int mouseX, int mouseY, MouseState mouseState)
        {
            handPosition.X = mouseX;
            handPosition.Y = mouseY;
            mouse = mouseState;
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            hand = this.content.Load<Texture2D>("handMed");
        }

        public void Update(GameTime gameTime)
        {
            //animateState = handAnimation.open;
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                animateState = handAnimation.closed;
            }
            else
            {
                animateState = handAnimation.open;
            }


            //animation
            //animation
           // if (animateState != handAnimation.Idle) timerRate += gametime.ElapsedGameTime;


            while (timerRate.TotalMilliseconds > ANIMATION_FRAME_RATE)
            {

                frame++;

                timerRate -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
            }

            frame %= 1;


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var source = new Rectangle(
            frame * FRAME_WIDTH, // X value 
            (int)animateState % 2 * FRAME_HEIGHT, // Y value
            FRAME_WIDTH, // Width 
            FRAME_HEIGHT // Height
            );

            spriteBatch.Draw(hand, new Vector2(handPosition.X, handPosition.Y),source, Color.White);
        }
    }
}
