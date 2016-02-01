using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace FlappyBird.Helpers
{
    class Collision
    {
        public static bool IsSloppyCollision(Rectangle sprite1Rectangle, Rectangle sprite2Rectangle)
        {
            return sprite1Rectangle.Intersects(sprite2Rectangle);
        }

        public static bool IsPixelCollision(Rectangle sprite1Rectangle, Rectangle sprite2Rectangle, Color[] colorData1, Color[] colorData2)
        {
            bool hasCollided = false;

            if (sprite1Rectangle.Intersects(sprite2Rectangle))
            {
                int _top = Math.Max(sprite1Rectangle.Top, sprite2Rectangle.Top);
                int _bottom = Math.Min(sprite1Rectangle.Bottom, sprite2Rectangle.Bottom);
                int _left = Math.Max(sprite1Rectangle.Left, sprite2Rectangle.Left);
                int _right = Math.Min(sprite1Rectangle.Right, sprite2Rectangle.Right);

                try
                {
                    for (int yA = _top; yA < _bottom; yA++)
                    {
                        // For each pixel in this row
                        for (int xA = _left; xA < _right; xA++)
                        {

                            Color colorA = colorData1[(xA - sprite1Rectangle.Left) + (yA - sprite1Rectangle.Top) * sprite1Rectangle.Width];
                            Color colorB = colorData2[(xA - sprite2Rectangle.Left) + (yA - sprite2Rectangle.Top) * sprite2Rectangle.Width];

                            // If both pixels are not completely transparent,
                            if (colorA.A > 0 && colorB.A > 0)
                            {
                                // then an intersection has been found
                                hasCollided = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception occured while checking for pixel collision. " + ex.InnerException);
                }
            }

            return hasCollided;
        }
    }
}
