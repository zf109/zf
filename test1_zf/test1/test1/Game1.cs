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

namespace test1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //game world
        Character zergling;
        List<Clouds> clouds = new List<Clouds>(10);
        float spawnCloud = 0;
        sideScrolling scrolling1, scrolling2;

        //screen parameters
        int screenWidth;
        int screenHeight;

        //random number generator
        Random randomNumGenerator;

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
            
            //load zergling
            zergling = new Character(Content.Load<Texture2D>("star-crafts-happy-zergling_design"), new Rectangle(10, 100, 150/2, 150/2));


            screenHeight = GraphicsDevice.Viewport.Height;
            screenWidth = GraphicsDevice.Viewport.Width;

            //load side scrolling background
            scrolling1 = new sideScrolling(Content.Load<Texture2D>("CartoonBackdrops1"), new Rectangle(0, 0, screenWidth, screenHeight));
            scrolling2 = new sideScrolling(Content.Load<Texture2D>("CartoonBackdrops1"), new Rectangle(screenWidth, 0, screenWidth, screenHeight));
            
            //load clouds
            randomNumGenerator = new Random();
            for (int i = 0; i < clouds.Count; ++i)
                clouds[i] = new Clouds(Content.Load<Texture2D>("cloud1"), 
                    new Rectangle(randomNumGenerator.Next(0,screenWidth-100), randomNumGenerator.Next(0,screenHeight/3),100,100));
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
            if (scrolling1.rectangle.X + scrolling1.rectangle.Width <= 0)
                scrolling1.rectangle.X = scrolling2.rectangle.X + scrolling2.rectangle.Width;
            if (scrolling2.rectangle.X + scrolling2.rectangle.Width <= 0)
                scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling1.rectangle.Width;
            scrolling1.Update();
            scrolling2.Update();

            //update the zergling's movement
            zergling.Update();
            zergling.RestrictInScreen(screenHeight,screenWidth);
            base.Update(gameTime);

            //update the clouds movement
            spawnCloud += (float)gameTime.ElapsedGameTime.TotalSeconds;    
            foreach (Clouds cloud in clouds)
                cloud.Update();
            LoadClouds();

        }


        public void LoadClouds()
        {
            int randY = randomNumGenerator.Next(0, screenHeight-100);

            if (spawnCloud >= 1)    //respawn cloud every second
            {
                spawnCloud = 0;     //reset respawn timer
                if (clouds.Count() < 10)    //limit the number of total clouds to 10
                    clouds.Add(new Clouds(Content.Load<Texture2D>("cloud1"), new Rectangle(screenWidth+100, randY, 100, 100)));
            }

            for (int i = 0; i < clouds.Count; i ++)
                if (!clouds[i].isVisible)   //remove the cloud if it becomes invisible
                {
                    clouds.RemoveAt(i);
                    i--;
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
            scrolling1.Draw(spriteBatch);
            scrolling2.Draw(spriteBatch);
            foreach (Clouds cloud in clouds)
                cloud.Draw(spriteBatch);
            zergling.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
