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
    public class Hamburger
    {

        public BoundingRectangle meatPosition;
        public Texture2D meat;
        ContentManager content;


        public Hamburger(int x, int y)
        {
            meatPosition.X = x;
            meatPosition.Y = y;
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            meat = this.content.Load<Texture2D>("medMeat");
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(meat, new Vector2(meatPosition.X, meatPosition.Y), Color.White);
        }
    }
}
