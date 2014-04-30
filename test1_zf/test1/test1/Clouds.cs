using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test1
{
    class Clouds
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public Random randomNumGenerator;
        public int stray;

        public Clouds(Texture2D newTexture, Rectangle newRectangle)
        {
            //get the cloud texture
            texture = newTexture;
            rectangle = newRectangle;
            randomNumGenerator = new Random();
            stray = randomNumGenerator.Next(1, 4);
        }

        public void Update()
        {
            rectangle.X -= stray;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,rectangle, Color.White);
        }
    }

}
