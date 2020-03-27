using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ParticleSystemStarter
{
    public enum handCarry
    {
        empty = 0,
        rawMeat = 1,
        cookedBurger = 2,
        burntBurger = 3,
        bun = 4,
    }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ParticleSystem particleSystem;
        Texture2D particleTexture;
        private Random random = new Random();
        public Pan fryingPan = new Pan(270, 260);
        public Hamburger rawMeat = new Hamburger(600, 10);
        public Hand hand;
        public handCarry handContent = handCarry.empty;
        public Plate plate = new Plate(300, 90);
        public Buns bun = new Buns(10, 10);


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            fryingPan.LoadContent(Content);
            rawMeat.LoadContent(Content);
            plate.LoadContent(Content);
            bun.LoadContent(Content);
           // hand.LoadContent(Content);
            particleTexture = Content.Load<Texture2D>("particle");
            particleSystem = new ParticleSystem(GraphicsDevice, 1000, particleTexture);
            particleSystem.Emitter = new Vector2(100, 100);
            particleSystem.SpawnPerFrame = 4;

            // Set the SpawnParticle method
            particleSystem.SpawnParticle = (ref Particle particle) =>
            {
                MouseState mouse = Mouse.GetState();
                hand = new Hand(mouse.X, mouse.Y, mouse);
                hand.LoadContent(Content);
                particle.Position = new Vector2(mouse.X, mouse.Y);
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(-50, 50, (float)random.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(0, 100, (float)random.NextDouble()) // Y between 0 and 100
                    );
                particle.Acceleration = 0.1f * new Vector2(0, (float)-random.NextDouble());
                particle.Color = Color.Gold;
                particle.Scale = 1f;
                particle.Life = 1.0f;

                //animate hand
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    hand.animateState = handAnimation.closed;
                }
                else
                {
                    hand.animateState = handAnimation.open;
                }

            };

            // Set the UpdateParticle method
            particleSystem.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position += deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            particleSystem.Update(gameTime);
            fryingPan.Update(gameTime);

            //picking up raw meat
            if (hand.RectBounds.Intersects(rawMeat.RectBounds) && 
                hand.animateState == handAnimation.closed && handContent == handCarry.empty)
            {
                handContent = handCarry.rawMeat;
            }
            //droping raw meat in the pan
            if(hand.RectBounds.Intersects(fryingPan.RectBounds) && 
                hand.animateState == handAnimation.open && handContent == handCarry.rawMeat)
            {
                handContent = handCarry.empty;
                fryingPan.animateState = PanAnimation.raw;
                fryingPan.timer = 0;
            }
            //adding burger to hand
            if(hand.RectBounds.Intersects(fryingPan.RectBounds) && 
                hand.animateState == handAnimation.closed && handContent == handCarry.empty &&
                fryingPan.animateState == PanAnimation.done)
            {
                handContent = handCarry.cookedBurger;
                fryingPan.timer = 0;
                fryingPan.animateState = PanAnimation.empty;
            }
            //dropping burger on empty plate
            if(hand.RectBounds.Intersects(plate.RectBounds) &&
                hand.animateState == handAnimation.open && 
                handContent == handCarry.cookedBurger && plate.animateState == PlateAnimation.empty)
            {
                handContent = handCarry.empty;
                plate.animateState = PlateAnimation.patty;
            }
            //adding burger to plate with a bun on it
            if (hand.RectBounds.Intersects(plate.RectBounds) &&
                hand.animateState == handAnimation.open &&
                handContent == handCarry.cookedBurger && plate.animateState == PlateAnimation.bun)
            {
                handContent = handCarry.empty;
                plate.animateState = PlateAnimation.burger;
            }
            //adding bun to hand
            if (hand.RectBounds.Intersects(bun.RectBounds) && 
                hand.animateState == handAnimation.closed && handContent == handCarry.empty)
            {
                handContent = handCarry.bun;
            }
            //adding bun to empty plate
            if(hand.RectBounds.Intersects(plate.RectBounds) && 
                hand.animateState == handAnimation.open && 
                plate.animateState == PlateAnimation.empty && handContent == handCarry.bun)
            {
                handContent = handCarry.empty;
                plate.animateState = PlateAnimation.bun;
            }
            //adding bun to plate with burger on it
            if (hand.RectBounds.Intersects(plate.RectBounds) &&
               hand.animateState == handAnimation.open &&
               plate.animateState == PlateAnimation.patty && handContent == handCarry.bun)
            {
                handContent = handCarry.empty;
                plate.animateState = PlateAnimation.burger;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            particleSystem.Draw();

            spriteBatch.Begin();

            
            fryingPan.Draw(spriteBatch);
            rawMeat.Draw(spriteBatch);
            plate.Draw(spriteBatch);
            bun.Draw(spriteBatch);
            //draw hand last so it goes ver everything else
            hand.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
