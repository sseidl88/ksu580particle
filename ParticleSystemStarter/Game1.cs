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
        ParticleSystem steam;
        Texture2D steamTexture;
        //fire
        ParticleSystem fire;
        Texture2D fireTexture;
        //star
        ParticleSystem star;
        Texture2D starTexture;
        Texture2D particleTexture;
        private Random random = new Random();
        private Random randColor = new Random();
        int colorPick;
        public Pan fryingPan = new Pan(270, 260);
        public int steamSpawnX;
        public Hamburger rawMeat = new Hamburger(600, 10);
        public Hand hand;
        public handCarry handContent = handCarry.empty;
        public Plate plate = new Plate(300, 90);
        public Buns bun = new Buns(10, 10);
        public Bell bell = new Bell(40, 350);
        SpriteFont font;
        public Trash trash = new Trash(700, 350);

        //score
        int score = 0;
        bool complete = false;
        public float starTimer = 0;


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
            font = Content.Load<SpriteFont>("font");
            fryingPan.LoadContent(Content);
            rawMeat.LoadContent(Content);
            plate.LoadContent(Content);
            bun.LoadContent(Content);
            bell.LoadContent(Content);
            trash.LoadContent(Content);
           // hand.LoadContent(Content);
            particleTexture = Content.Load<Texture2D>("particle");
            particleSystem = new ParticleSystem(GraphicsDevice, 500, particleTexture);
            particleSystem.Emitter = new Vector2(100, 100);
            particleSystem.SpawnPerFrame = 4;
            //steam
            steamTexture = Content.Load<Texture2D>("steam");
            steam = new ParticleSystem(GraphicsDevice, 100, steamTexture);
            steam.Emitter = new Vector2(100, 100);
            steam.SpawnPerFrame = 4;
            //fire
            fireTexture = Content.Load<Texture2D>("fireParticle");
            fire = new ParticleSystem(GraphicsDevice, 1000, fireTexture);
            fire.Emitter = new Vector2(100, 100);
            fire.SpawnPerFrame = 10;
            //star
            starTexture = Content.Load<Texture2D>("star");
            star = new ParticleSystem(GraphicsDevice, 5, starTexture);
            star.Emitter = new Vector2(100, 100);
            star.SpawnPerFrame = 1;
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
            //STEAM
            steam.SpawnParticle = (ref Particle particle) =>
            {
                steamSpawnX = (int)random.Next(300, 500);
                particle.Position = new Vector2(steamSpawnX, fryingPan.position.Y + 50);
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(-50, 50, (float)random.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(0, 100, (float)random.NextDouble()) // Y between 0 and 100
                    );
                particle.Acceleration = 0.1f * new Vector2(0, (float)-random.NextDouble());
                particle.Color = Color.Ivory;
                particle.Scale = 1f;
                // particle.Life = 3.0f;
                particle.Life = 10;


            };

            //fire
            fire.SpawnParticle = (ref Particle fireParticle) =>
            {
                steamSpawnX = (int)random.Next(300, 500);
                fireParticle.Position = new Vector2(steamSpawnX, fryingPan.position.Y + 50);
                fireParticle.Velocity = new Vector2(
                    MathHelper.Lerp(-50, 50, (float)random.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(0, 100, (float)random.NextDouble()) // Y between 0 and 100
                    );
                fireParticle.Acceleration = 0.1f * new Vector2(0, (float)-random.NextDouble());

               // fireParticle.Color = Color.DarkRed;
                fireParticle.Scale = 1f;
                fireParticle.Life = 15;
                colorPick = randColor.Next(1, 4);

                
                if (colorPick == 1)
                    fireParticle.Color = Color.YellowGreen;
                else if (colorPick == 2)
                    fireParticle.Color = Color.MonoGameOrange;
                else
                    fireParticle.Color = Color.Red;
                

                    

            };

            //star
            star.SpawnParticle = (ref Particle particle) =>
            {
               int starSpawnX = (int)random.Next((int)bell.position.X, (int)(bell.position.X + 70));
                particle.Position = new Vector2(starSpawnX, bell.position.Y - 20 );
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(-50, 50, (float)random.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(0, 100, (float)random.NextDouble()) // Y between 0 and 100
                    );
                // particle.Velocity = new Vector2(1,1);
                particle.Acceleration = 0.1f * new Vector2(0, (float)-random.NextDouble());
                particle.Color = Color.Aquamarine;
                particle.Scale = 2;
                 particle.Life = 3.0f;
                //particle.Life = 200;


            };

            // Set the UpdateParticle method
            particleSystem.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position += deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };
            //steam update
            steam.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position -= deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };
            //fire update
            fire.UpdateParticle = (float deltaT, ref Particle fireParticle) =>
            {
                fireParticle.Velocity += deltaT * fireParticle.Acceleration;
                fireParticle.Position -= deltaT * fireParticle.Velocity;
                fireParticle.Scale -= deltaT;
                fireParticle.Life -= deltaT;
                //change color
                //fireParticle.Color = Color.Yellow;
                //if (fireParticle.Life > 15)
                //    fireParticle.Color = Color.Yellow;
                //else if (fireParticle.Life > 10 && fireParticle.Life < 15)
                //    fireParticle.Color = Color.Orange;
                //else if (fireParticle.Life > 5 && fireParticle.Life < 10)
                //    fireParticle.Color = Color.Red;
                //else
                //    fireParticle.Color = Color.TransparentBlack;
            };
            //star update
            star.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position -= deltaT * particle.Velocity;
              //  particle.Scale -= deltaT;
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
            steam.Update(gameTime);
            fire.Update(gameTime);
            star.Update(gameTime);

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
            //ringing the bell
            if(hand.RectBounds.Intersects(bell.RectBounds) && 
                hand.animateState == handAnimation.closed && handContent == handCarry.empty)
            {
                if(plate.animateState == PlateAnimation.burger)
                {
                    score += 10;
                    complete = true;
                    starTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                plate.animateState = PlateAnimation.empty;
            }
            //picking up burnt burger
            //if (hand.RectBounds.Intersects(fryingPan.RectBounds) &&
            //hand.animateState == handAnimation.closed && handContent == handCarry.empty &&
            //fryingPan.animateState == PanAnimation.burnt)
            //{
            //    handContent = handCarry.burntBurger;
            //    fryingPan.animateState = PanAnimation.empty;
            //}

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
            //scoreboard
            spriteBatch.DrawString(font, "Score: " + score.ToString(), new Vector2(350, 20), Color.White);

            fryingPan.Draw(spriteBatch);

            //steam
            if(fryingPan.animateState == PanAnimation.raw || fryingPan.animateState == PanAnimation.done)
            steam.Draw();
            //fire
            if(fryingPan.animateState == PanAnimation.burnt)
            fire.Draw();
            //star
            while (complete && starTimer < 2)
            {
                star.Draw();
                complete = false;
            }
            starTimer = 0;


            rawMeat.Draw(spriteBatch);
            plate.Draw(spriteBatch);
            bun.Draw(spriteBatch);
            bell.Draw(spriteBatch);
            //trash doesn't work currently
            //trash.Draw(spriteBatch);
            //draw hand last so it goes over everything else
            hand.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
