using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlappyBird.Helpers
{
    class Debug
    {
        public static void DrawHitboxes(List<Rectangle> rectangles)
        {
            Statics.GAME_SPRITEBATCH.Begin();

            foreach (Rectangle rect in rectangles)
            {
                Statics.GAME_SPRITEBATCH.Draw(Statics.TEXTURE_PIXEL, rect, Statics.COLOR_HITBOX);
            }

            Statics.GAME_SPRITEBATCH.End();
        }

        public static void DrawText(Vector2 position, String text)
        {
            Statics.GAME_SPRITEBATCH.Begin();
            Statics.GAME_SPRITEBATCH.DrawString(Statics.MANAGER_FONT.Library["Small"], text, position, Color.White);
            Statics.GAME_SPRITEBATCH.End();
        }
    }
}
