using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics; 

namespace FlappyBird
{
    class ParallaxBackground
    {
        // The image representing the parallaxing background
        private Texture2D _texture;

        // An array of positions of the parallaxing background
        private Vector2[] _positions;

        private int _bgWidth, _bgHeight;

        private bool _isVisible;

        // The speed which the background is moving
        private float _moveSpeed;
        public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

        private bool _isScrolling;
        public bool IsScrolling { get { return _isScrolling; } set { _isScrolling = value; } }
        
        public ParallaxBackground(ContentManager content, String texturePath, int screenWidth, int screenHeight, float moveSpeed, bool isVisible, bool isScrolling)
        {
            // Set the speed of the background
            _moveSpeed = moveSpeed;
            _isScrolling = isScrolling;
            _isVisible = isVisible;
            
            _bgHeight = screenHeight;
            _bgWidth = screenWidth;

            // Load the background texture we will be using
            _texture = content.Load<Texture2D>(texturePath);

            // If we divide the screen with the texture width then we can determine the number of tiles need.
            // We add 1 to it so that we won't have a gap in the tiling
            _positions = new Vector2[screenWidth / _texture.Width + 2];

            // Set the initial positions of the parallaxing background
            for (int i = 0; i < _positions.Length; i++)
            {
                // We need the tiles to be side by side to create a tiling effect
                _positions[i] = new Vector2(i * _texture.Width, screenHeight - _texture.Height);
            }
        }

        public void Initialize() 
        {
            
        }

        public void Update(GameTime gameTime)
        {
            if (!_isScrolling || _moveSpeed == 0)
                return;

            // Update the positions of the background
            for (int i = 0; i < _positions.Length; i++)
            {
                _positions[i].X += _moveSpeed;
            }

            for (int i = 0; i < _positions.Length; i++)
            {
                if (_moveSpeed <= 0)
                {
                    // Check if the texture is out of view and then put that texture at the end of the screen.
                    if (_positions[i].X <= -_texture.Width)
                    {
                        WrapTextureToLeft(i);
                    }
                }
                else
                {
                    if (_positions[i].X >= _texture.Width * (_positions.Length - 1))
                    {
                        WrapTextureToRight(i);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color filter)
        {
            if (!_isVisible)
                return;
            
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            Rectangle rectBg;

            for (int i = 0; i < _positions.Length; i++)
            {
                rectBg = new Rectangle((int)_positions[i].X, (int)_positions[i].Y, _texture.Width, _texture.Height);

                spriteBatch.Draw(_texture, rectBg, filter);
            }

            spriteBatch.End();
        }

        private void WrapTextureToLeft(int index)
        {
            // If the textures are scrolling to the left, when the tile wraps, it should be put at the
            // one pixel to the right of the tile before it.
            int prevTexture = index - 1;
            if (prevTexture < 0)
                prevTexture = _positions.Length - 1;

            _positions[index].X = _positions[prevTexture].X + _texture.Width;
        }

        private void WrapTextureToRight(int index)
        {
            // If the textures are scrolling to the right, when the tile wraps, it should be placed to the left
            // of the tile that comes after it.
            int nextTexture = index + 1;
            if (nextTexture == _positions.Length)
                nextTexture = 0;

            _positions[index].X = _positions[nextTexture].X - _texture.Width;
        }
    }
}
