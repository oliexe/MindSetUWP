using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FlappyBird.Screens
{
    class CursorScreen : Screen
    {
        private Texture2D _cursor_Texture;
        private Vector2 _cursor_Position;

        public Vector2 Position { get { return _cursor_Position; } }

        private Vector2 _previousThumbstickPosition;
        
        public CursorScreen()
        {
            _previousThumbstickPosition = Vector2.Zero;
        }

        public override void LoadContent()
        {
            _cursor_Texture = Statics.GAME_CONTENT.Load<Texture2D>("Textures/flappy_cursor");

            base.LoadContent();
        }

        public override void Update()
        {
            _cursor_Position = Statics.MANAGER_INPUT.GetCursorPosition();
            
            if (_previousThumbstickPosition != Statics.MANAGER_INPUT.CurrentGamePadState().ThumbSticks.Left)
            {
                _cursor_Position.X = (Statics.MANAGER_INPUT.CurrentGamePadState().ThumbSticks.Left.X + 1) * Statics.GAME_WIDTH / 2;
                _cursor_Position.Y = (Statics.MANAGER_INPUT.CurrentGamePadState().ThumbSticks.Left.Y + 1) * Statics.GAME_HEIGHT / 2;

                _previousThumbstickPosition = Statics.MANAGER_INPUT.CurrentGamePadState().ThumbSticks.Left;
            }
            
            base.Update();
        }

        public override void Draw()
        {
            Statics.GAME_SPRITEBATCH.Draw(_cursor_Texture, _cursor_Position, Color.White);
            
            base.Draw();
        }
    }
}