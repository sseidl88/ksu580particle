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
    public enum PlateAnimation
    {
        empty = 4,
        patty = 1,
        bun = 2,
        burger = 3,
    }
    public class Plate
    {
        public BoundingRectangle position;
        public Rectangle RectBounds
        {
            get { return position; }
        }
        public Texture2D plate;
        ContentManager content;

        const int ANIMATION_FRAME_RATE = 124;
        TimeSpan timerRate;
        public PlateAnimation animateState;

        int frame;
        const int FRAME_WIDTH = 230;

        const int FRAME_HEIGHT = 120;

   


        public Plate(int x, int y)
        {
            position.X = x;
            position.Y = y;
            position.Height = 120;
            position.Width = 230;
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            plate = this.content.Load<Texture2D>("plate");
            animateState = PlateAnimation.empty;
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

            spriteBatch.Draw(plate, new Vector2(position.X, position.Y), source, Color.White);
        }
    }
}
