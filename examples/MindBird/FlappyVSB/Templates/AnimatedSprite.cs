using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics; 

namespace FlappyBird
{
    class AnimatedSprite
    {
        // The image representing the collection of images used for animation
        Texture2D spriteStrip;

        // The time since we last updated the frame
        int elapsedTime;

        // The time we display a frame until the next one
        int frameTime;

        // The number of frames that the animation contains
        int frameCount;

        // The index of the current frame we are displaying
        int currentFrame;

        // The color of the frame we will be displaying
        Color color;

        // The area of the image strip we want to display
        Rectangle sourceRect = new Rectangle();

        // The area where we want to display the image strip in the game
        Rectangle destinationRect = new Rectangle();

        // Width of a given frame
        public int FrameWidth;

        // Height of a given frame
        public int FrameHeight;

        // The state of the Animation
        public bool IsActive;

        // Determines if the animation will keep playing or deactivate after one run
        public bool IsLooping;

        // Position of the image on screen
        public Vector2 Position;

        public float Rotation;

        // The scale used to display the sprite strip
        public float Scale;

        // The position where we want to rotate the image
        public Vector2 SourceRotate;

        public Rectangle Bounds { get { return this.destinationRect; } }

        public void Initialize(Texture2D texture, Vector2 position, float rotation, int frameWidth, int frameHeight, int frameCount, int frametime, Color color, float scale, bool isLooping) 
        {
            // Keep a local copy of the values passed in
            this.color = color;
            this.frameCount = frameCount;
            this.frameTime = frametime;
            this.spriteStrip = texture;

            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            this.IsLooping = isLooping;
            this.Position = position;
            this.Rotation = rotation;
            this.SourceRotate = new Vector2(frameWidth / 2, frameHeight / 2);
            this.Scale = scale;
            
            // Set the time to zero
            this.elapsedTime = 0;
            this.currentFrame = 0;

            // Set the Animation to active by default
            this.IsActive = true;
        }

        public void Update(GameTime gameTime)
        {
            // Do not update the game if we are not active
            if (this.IsActive == false) return;

            // Update the elapsed time
            this.elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            // If the elapsed time is larger than the frame time we need to switch frames
            if (this.elapsedTime > this.frameTime)
            {
                // Move to the next frame
                this.currentFrame++;

                // If the currentFrame is equal to frameCount reset currentFrame to zero
                if (this.currentFrame == this.frameCount)
                {

                    this.currentFrame = 0;

                    // If we are not looping deactivate the animation
                    if (this.IsLooping == false)
                        this.IsActive = false;
                }

                // Reset the elapsed time to zero
                this.elapsedTime = 0;

            }

            // Grab the correct frame in the image strip by multiplying the currentFrame index by the Frame width
            this.sourceRect = new Rectangle(this.currentFrame * this.FrameWidth, 0, this.FrameWidth, this.FrameHeight);

            // Grab the correct frame in the image strip by multiplying the currentFrame index by the frame width
            this.destinationRect = new Rectangle((int)this.Position.X - (int)(this.FrameWidth * this.Scale) / 2, (int)this.Position.Y - (int)(this.FrameHeight * this.Scale) / 2, (int)(this.FrameWidth * this.Scale), (int)(this.FrameHeight * this.Scale));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Only draw the animation when we are active
            if (this.IsActive) 
            {
                spriteBatch.Begin();

                spriteBatch.Draw(this.spriteStrip, this.destinationRect, this.sourceRect, this.color, this.Rotation, this.SourceRotate, SpriteEffects.None, 0);

                spriteBatch.End();
            } 
        }
    }
}
