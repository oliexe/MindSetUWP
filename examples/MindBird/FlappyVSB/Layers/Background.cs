using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlappyBird.Layers
{
    class Background
    {
        public Dictionary<string, ParallaxBackground> BackgroundLayer_Stack;

        public Background()
        {
            this.BackgroundLayer_Stack = new Dictionary<string, ParallaxBackground>();
        }

        public void LoadContent()
        {
            AddBackgrounds();
        }

        public void Update()
        {
            foreach (ParallaxBackground layer in this.BackgroundLayer_Stack.Values)
            {
                layer.Update(Statics.GAME_GAMETIME);
            }
        }

        public void Draw()
        {
            foreach (ParallaxBackground layer in this.BackgroundLayer_Stack.Values)
            {
                layer.Draw(Statics.GAME_SPRITEBATCH, Color.White);
            }
        }

        public void ResetBackgrounds()
        {
            this.BackgroundLayer_Stack.Clear();

            AddBackgrounds();
        }

        private void AddBackgrounds()
        {
            this.BackgroundLayer_Stack.Add("Clouds1", new ParallaxBackground(Statics.GAME_CONTENT, "Textures\\Background\\flappy_clouds_1", Statics.GAME_WIDTH, Statics.GAME_HEIGHT, 0f, true, true));
            this.BackgroundLayer_Stack.Add("Clouds2", new ParallaxBackground(Statics.GAME_CONTENT, "Textures\\Background\\flappy_clouds_2", Statics.GAME_WIDTH, Statics.GAME_HEIGHT, -0.5f, true, true));
            this.BackgroundLayer_Stack.Add("Clouds3", new ParallaxBackground(Statics.GAME_CONTENT, "Textures\\Background\\flappy_clouds_3", Statics.GAME_WIDTH, Statics.GAME_HEIGHT, -1.0f, true, true));

            this.BackgroundLayer_Stack.Add("Hills1", new ParallaxBackground(Statics.GAME_CONTENT, "Textures\\Background\\flappy_hills_1", Statics.GAME_WIDTH, Statics.GAME_HEIGHT, -1.5f, true, true));
            this.BackgroundLayer_Stack.Add("Hills2", new ParallaxBackground(Statics.GAME_CONTENT, "Textures\\Background\\flappy_hills_2", Statics.GAME_WIDTH, Statics.GAME_HEIGHT, -2.0f, true, true));

            this.BackgroundLayer_Stack.Add("Houses", new ParallaxBackground(Statics.GAME_CONTENT, "Textures\\Background\\flappy_houses", Statics.GAME_WIDTH, Statics.GAME_HEIGHT, -1.0f, true, true));
        }
    }
}
