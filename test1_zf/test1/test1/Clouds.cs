using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test1
{
    class Clouds : Sprite
    {
        public Random randomNumGenerator;
        public int stray;
        public bool isVisible;

        public Clouds(Texture2D newTexture, Rectangle newRectangle)
        {
            //get the cloud texture
            spriteTexture = newTexture;
            spriteRectangle = newRectangle;
            randomNumGenerator = new Random();
            stray = randomNumGenerator.Next(1, 4);
            isVisible = true;
        }

        public override void Update()
        {
            spriteRectangle.X -= stray;
            if (spriteRectangle.X < 0 - spriteRectangle.Width)
                isVisible = false;
        }

        //public void Draw(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Draw(texture,rectangle, Color.White);
        //}
    }

}
