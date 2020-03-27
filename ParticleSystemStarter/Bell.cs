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
    public class Bell
    {

        public BoundingRectangle position;
        public Texture2D bell;
        ContentManager content;

        public Rectangle RectBounds
        {
            get { return position; }
        }


        public Bell(int x, int y)
        {
            position.X = x;
            position.Y = y;
            position.Height = 160;
            position.Width = 160;
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            bell = this.content.Load<Texture2D>("bell");
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bell, new Vector2(position.X, position.Y), Color.White);
        }
    }
}
