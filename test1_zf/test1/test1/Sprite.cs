using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test1
{
    class Sprite
    {
        public Texture2D spriteTexture;
        public Rectangle spriteRectangle;
        public int velocity = 3;
        //for update
        public virtual void Update()
        {
        }

        //draw the sprite
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spriteRectangle, Color.White);
        }
    }
    class Character : Sprite
        {
            //constructor
            public Character(Texture2D newTexture, Rectangle newRectangle)
            {
                spriteTexture = newTexture;
                spriteRectangle = newRectangle;
            }

            //for update
            public override void Update()
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    spriteRectangle.X += velocity;
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    spriteRectangle.X -= velocity;
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    spriteRectangle.Y -= velocity;
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    spriteRectangle.Y += velocity;
            }
            public void RestrictInScreen(int screenHeight, int screenWidth)
            {
                if (spriteRectangle.X <= 0)
                    spriteRectangle.X = 0;

                if (spriteRectangle.X + spriteRectangle.Width >= screenWidth)
                    spriteRectangle.X = screenWidth - spriteRectangle.Width;

                if (spriteRectangle.Y <= 0)
                    spriteRectangle.Y = 0;

                if (spriteRectangle.Y + spriteRectangle.Height >= screenHeight)
                    spriteRectangle.Y = screenHeight - spriteRectangle.Height;
            }
        
    }
}
