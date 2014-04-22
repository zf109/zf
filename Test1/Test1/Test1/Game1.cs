using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Test1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        //Game Wrold
        Texture2D myTexture;
        Rectangle myRectangle;
        Vector2 velocity;
        Random myRandom = new Random();

        //Screen Parameters
        int screenWidth;
        int screenHeight;
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
            //load textures
            //myTexture = Content.Load<Texture2D>("star-crafts-happy-zergling_design");
            myTexture = Content.Load<Texture2D>("silverBall01");
            myRectangle = new Rectangle(300, 300, 32, 32);

            //initial velocity
            velocity.X = 3f;
            velocity.Y = 3f;
            RandomizeInitialVelocity();

            //get screen parameters
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            // TODO: Add your update logic here
            // Moving with AWSD
            //if (Keyboard.GetState().IsKeyDown(Keys.D))
            //    myRectangle.X += velocity;
            //if (Keyboard.GetState().IsKeyDown(Keys.A))
            //    myRectangle.X -= velocity;
            //if (Keyboard.GetState().IsKeyDown(Keys.S))
            //    myRectangle.Y += velocity;
            //if (Keyboard.GetState().IsKeyDown(Keys.W))
            //    myRectangle.Y -= velocity;

            //free moving ball
            myRectangle.X = myRectangle.X + (int) velocity.X;
            myRectangle.Y = myRectangle.Y + (int)velocity.Y;
            
            //controlled moving ball
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                RandomizeInitialVelocity();
                //myRectangle.X = (screenWidth - myRectangle.Width) / 2;
                //myRectangle.Y = (screenHeight - myRectangle.Height) / 2;
            }
            //bounce when touched the edge of the window
            if (myRectangle.X <= 0 || myRectangle.X >= screenWidth - myRectangle.Width)
                velocity.X = -velocity.X;
            if (myRectangle.Y <= 0 || myRectangle.Y >= screenHeight - myRectangle.Height)
                velocity.Y = -velocity.Y;

            //Restrict the image within the window
            //if(myRectangle.X <= 0)
            //    myRectangle.X = 0;
            //if (myRectangle.X >= screenWidth - myRectangle.Width)
            //    myRectangle.X = screenWidth - myRectangle.Width;
            //if (myRectangle.Y <= 0)
            //    myRectangle.Y = 0;
            //if (myRectangle.Y >= screenHeight - myRectangle.Height)
            //    myRectangle.Y = screenHeight - myRectangle.Height;

            base.Update(gameTime);
        }

        //Set the ball to a random direction
        void RandomizeInitialVelocity()
        {
            int random = myRandom.Next(0,4);
            if(random==0)
            {
                velocity.X = 3f;
                velocity.Y = 3f;
            }
            if(random == 1)
            {
                velocity.X = -3f;
                velocity.Y = 3f;
            }
            if(random==2)
            {
                velocity.X = 3f;
                velocity.Y = -3f;
            }
            if(random==3)
            {
                velocity.X = -3f;
                velocity.Y = -3f;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(myTexture,myRectangle, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
