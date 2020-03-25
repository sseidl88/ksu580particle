using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSystemStarter
{
    public delegate void ParticleSpawner(ref Particle particle);
    public delegate void ParticleUpdater(float deltaT, ref Particle particle);

    public class ParticleSystem
    {
        //private fields
        private Particle[] particles;
        private Texture2D Texture;
        private SpriteBatch spriteBatch;
        private Random random = new Random();
        private int nextIndex = 0;


        //public properties
        public Vector2 Emitter { get; set; }
        public int SpawnPerFrame { get; set; }
        //delegate properties
        public ParticleSpawner SpawnParticle { get; set; }
        public ParticleUpdater UpdateParticle { get; set; }



        public ParticleSystem(GraphicsDevice graphicsDevice, int size, Texture2D texture)
        {
            this.particles = new Particle[size];
            this.spriteBatch = new SpriteBatch(graphicsDevice);
            this.Texture = texture;
        }

        public void Update(GameTime gameTime)
        {
            // Make sure our delegate properties are set
            if (SpawnParticle == null || UpdateParticle == null) return;

            // Part 1: Spawn new particles 
            for (int i = 0; i < SpawnPerFrame; i++)
            {
                // Create the particle
                SpawnParticle(ref particles[nextIndex]);

                // Advance the index 
                nextIndex++;
                if (nextIndex > particles.Length - 1) nextIndex = 0;
            }

            // Part 2: Update Particles
            float deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
            for (int i = 0; i < particles.Length; i++)
            {
                // Skip any "dead" particles
                if (particles[i].Life <= 0) continue;

                // Update the individual particle
                UpdateParticle(deltaT, ref particles[i]);
            }
        }
    

        public void Draw()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);

            //  Draw particles
            // Iterate through the particles
            for (int i = 0; i < particles.Length; i++)
            {
                // Skip any "dead" particles
                if (particles[i].Life <= 0) continue;

                // Draw the individual particles
                spriteBatch.Draw(Texture, particles[i].Position, null, particles[i].Color, 0f, Vector2.Zero, particles[i].Scale, SpriteEffects.None, 0);
            }

            spriteBatch.End();
        }
    }
}
